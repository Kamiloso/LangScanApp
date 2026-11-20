import os
import re
import time
import logging

import httpx

from config import GEMMA_BATCH_SIZE, GEMMA_TIMEOUT_SECONDS, GEMMA_TOTAL_SECONDS

logger = logging.getLogger("flashcards")

GEMMA_URL = os.environ.get("GEMMA_URL", "http://gemma:11434/v1/chat/completions")
GEMMA_MODEL = os.environ.get("GEMMA_MODEL", "")

LANG_NAMES = {
    "de": "German",
    "en": "English",
    "it": "Italian",
    "es": "Spanish",
    "fr": "French",
    "pl": "Polish"
}

EXAMPLES = {
    "de": ("Hund", "Ein Haustier, das bellt"),
    "en": ("dog", "A pet animal that barks"),
    "it": ("cane", "Animale domestico che abbaia"),
    "es": ("perro", "Animal doméstico que ladra"),
    "fr": ("chien", "Animal domestique qui aboie"),
    "pl": ("pies", "Zwierzę domowe, które szczeka"),
}

NUMBER_PREFIX = re.compile(r"^(\d+)\s*[.):]?\s*(.*)$")


def parseLine(line):
    line = line.replace("**", "").strip().strip("|").strip().lstrip("-*• ").strip()
    match = NUMBER_PREFIX.match(line)
    if not match:
        return None

    rest = match.group(2).strip().strip("|").strip()
    parts = [p.strip() for p in rest.split("|")]
    if len(parts) < 2:
        return None

    return int(match.group(1)), parts[-2], parts[-1]


def requestBatch(pairs, learningLang, knownLang):
    learningName = LANG_NAMES[learningLang]
    knownName = LANG_NAMES[knownLang]
    exLearning = EXAMPLES[learningLang]
    exKnown = EXAMPLES[knownLang]

    lines = "".join(f"{i} | {w} | {t}\n" for i, (w, t) in enumerate(pairs, 1))

    prompt = (
        f"Write short dictionary definitions (max 8 words each) for flashcards. "
        f"Each input line is: number | {learningName} word | {knownName} translation. "
        f"For every line, define the {learningName} word in {learningName} and the {knownName} translation in {knownName}, "
        f"without repeating the words themselves or just translating them.\n"
        f"Output exactly one line per input line, formatted as:\n"
        f"number | definition in {learningName} | definition in {knownName}\n"
        f"No markdown, no bullets, no table, no extra text.\n\n"
        f"Example input:\n1 | {exLearning[0]} | {exKnown[0]}\n"
        f"Example output:\n1 | {exLearning[1]} | {exKnown[1]}\n\n"
        f"Input:\n{lines}"
    )

    try:
        response = httpx.post(
            GEMMA_URL,
            timeout=GEMMA_TIMEOUT_SECONDS,
            json={
                "model": GEMMA_MODEL,
                "messages": [{"role": "user", "content": prompt}],
                "temperature": 0.2,
                "reasoning_effort": "none",
                "max_tokens": 150 * len(pairs) + 300,
            },
        )
        response.raise_for_status()
        choice = response.json()["choices"][0]
        text = choice["message"].get("content") or ""

        if not text.strip():
            logger.warning(
                "Gemma zwróciła pusty content (finish_reason=%s, reasoning present=%s). "
                "Model prawdopodobnie 'myśli' i zużywa limit tokenów.",
                choice.get("finish_reason"),
                bool(choice["message"].get("reasoning")),
            )

    except Exception:
        logger.exception("Nie udało się połączyć z Ollamą")
        return None

    definitions = {}
    for line in text.splitlines():
        parsed = parseLine(line)
        if parsed is None:
            continue

        number, learningDef, knownDef = parsed
        idx = number - 1
        if not 0 <= idx < len(pairs):
            continue

        word = pairs[idx][0].lower()
        translation = pairs[idx][1].lower()

        if learningDef.lower() in (word, translation):
            learningDef = ""
        if knownDef.lower() in (word, translation):
            knownDef = ""

        definitions[word] = (learningDef or None, knownDef or None)

    if len(definitions) < len(pairs):
        logger.warning(
            "Gemma: sparsowano %d/%d linii. Surowa odpowiedź:\n%s",
            len(definitions), len(pairs), text,
        )

    return definitions


def runBatches(pairs, learningLang, knownLang, startTime, definitions):
    for start in range(0, len(pairs), GEMMA_BATCH_SIZE):
        if time.time() - startTime > GEMMA_TOTAL_SECONDS:
            logger.warning("TIME OUT")
            return False

        batch = pairs[start : start + GEMMA_BATCH_SIZE]
        batchResult = requestBatch(batch, learningLang, knownLang)

        if batchResult is None:
            return False

        definitions.update(batchResult)

    return True


def generateDefinitions(pairs, learningLang, knownLang):
    if not pairs:
        return {}

    definitions = {}
    startTime = time.time()

    finished = runBatches(pairs, learningLang, knownLang, startTime, definitions)

    if finished:
        missing = [pair for pair in pairs if pair[0].lower() not in definitions]
        if missing:
            logger.warning("Ponawiam %d brakujących definicji", len(missing))
            runBatches(missing, learningLang, knownLang, startTime, definitions)

    logger.warning(
        "Definicje: %d/%d w %.1f s",
        len(definitions), len(pairs), time.time() - startTime,
    )
    return definitions