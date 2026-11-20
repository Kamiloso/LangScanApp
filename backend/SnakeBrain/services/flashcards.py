import logging
import re
import threading
import torch
from fastapi import HTTPException
from schemas import ProcessRequest, ProcessResponse, TokenPair
from config import CONTENT, STOP_LEMMAS, MAX_DEFINITIONS_PER_REQUEST
from services.stanza_service import getStanzaPipeline
from services.translator import translateWords
from services.gemma_service import generateDefinitions
from services.cefr import getCefrLevel

logger = logging.getLogger("flashcards")
PROCESS_LOCK = threading.Lock()

DEFINITION_UNAVAILABLE = {
    "pl": "definicja niedostępna",
    "en": "definition not available",
}

def unavailableText(knownLang):
    return DEFINITION_UNAVAILABLE.get(knownLang, DEFINITION_UNAVAILABLE["en"])

def levelMatches(wordLevel, requestedLevel):
    if requestedLevel is None:
        return True
    if wordLevel is None:
        return True
    return wordLevel == requestedLevel

def processFlashcardsText(request: ProcessRequest) -> ProcessResponse:
    if request.learningLang == request.knownLang:
        raise HTTPException(status_code=400, detail="Języki muszą być różne.")

    try:
        with PROCESS_LOCK, torch.inference_mode():
            cleanedText = re.sub(r"\s+", " ", request.text).strip()
            nlpSource = getStanzaPipeline(request.learningLang)

            sentences = nlpSource(cleanedText).sentences
            if not sentences:
                return ProcessResponse(tokens=[])

            stopLemmas = STOP_LEMMAS.get(request.learningLang, set())

            uniqueWords = []
            uniquePos = []
            levelsMap = {}
            seenWords = {}

            for sentence in sentences:
                for word in sentence.words:
                    if word.upos not in CONTENT:
                        continue

                    learningWord = lemmaOf(word, request.learningLang)
                    if learningWord == "":
                        continue
                    if learningWord.lower() in stopLemmas:
                        continue
                    if learningWord in seenWords:
                        continue

                    seenWords[learningWord] = True

                    level = getCefrLevel(request.learningLang, learningWord, word.upos)
                    if not levelMatches(level, request.proficiencyLevel):
                        continue

                    levelsMap[learningWord] = level
                    uniqueWords.append(learningWord)
                    uniquePos.append(word.upos)

            if not uniqueWords:
                return ProcessResponse(tokens=[])

            translations = translateWords(
                uniqueWords, request.learningLang, request.knownLang
            )

            cardsMap = {}
            for i in range(len(uniqueWords)):
                learningWord = uniqueWords[i]
                knownWord = cleanTranslation(translations[i], uniquePos[i], request.knownLang)

                if knownWord == "":
                    continue
                if learningWord.lower() == knownWord.lower():
                    continue

                cardsMap[learningWord] = knownWord

        definitionPairs = []
        for learningWord in cardsMap:
            if len(definitionPairs) >= MAX_DEFINITIONS_PER_REQUEST:
                break
            definitionPairs.append((learningWord, cardsMap[learningWord]))

        definitionsDict = generateDefinitions(
            definitionPairs, request.learningLang, request.knownLang
        )

        tokensList = []
        for learningWord in cardsMap:
            knownWord = cardsMap[learningWord]
            learningDef = None
            knownDef = None

            wordKey = learningWord.lower()
            if wordKey in definitionsDict:
                learningDef = definitionsDict[wordKey][0]
                knownDef = definitionsDict[wordKey][1]

            if not learningDef:
                learningDef = unavailableText(request.knownLang)
            if not knownDef:
                knownDef = unavailableText(request.knownLang)

            tokensList.append(TokenPair(
                learningWord=learningWord,
                knownWord=knownWord,
                learningDefinition=learningDef,
                knownDefinition=knownDef,
                proficiencyLevel=levelsMap[learningWord],
            ))

        return ProcessResponse(tokens=tokensList)

    except HTTPException:
        raise
    except Exception:
        logger.exception("processFlashcardsText failed")
        raise HTTPException(status_code=500, detail="Błąd przetwarzania tekstu.")

def cleanTranslation(text, upos, lang):
    cleaned = text.strip().strip(".,;:!?\"'")
    lowered = cleaned.lower()

    if lang == "en":
        prefixes = []
        if upos == "NOUN":
            prefixes = ["the ", "a ", "an "]
        if upos == "VERB":
            prefixes = ["to "]

        for prefix in prefixes:
            if lowered.startswith(prefix) and len(lowered) > len(prefix):
                cleaned = cleaned[len(prefix):]
                break

    if lang == "de" and upos == "NOUN":
        return cleaned
    return cleaned.lower()

def lemmaOf(word, lang):
    text = word.lemma or word.text
    if lang == "de" and word.upos == "NOUN":
        return text
    else:
        return text.lower()