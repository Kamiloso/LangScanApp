import csv

from config import CEFR_FILES, CEFR_ORDER, CEFR_DEFAULT_LEVEL, CEFR_POS_MAP

levelsByPos = {}
levelsByWord = {}


def betterLevel(oldLevel, newLevel):
    if oldLevel is None:
        return newLevel
    if CEFR_ORDER.index(newLevel) < CEFR_ORDER.index(oldLevel):
        return newLevel
    return oldLevel


def loadCefrFile(lang, path):
    with open(path, encoding="utf-8-sig", newline="") as file:
        reader = csv.DictReader(file)
        for row in reader:
            level = row["CEFR"].strip().upper()
            if level not in CEFR_ORDER:
                continue

            pos = row["pos"].strip().lower()
            variants = row["headword"].split("/")

            for variant in variants:
                word = variant.strip().lower()
                if word == "":
                    continue

                posKey = (lang, word, pos)
                wordKey = (lang, word)
                levelsByPos[posKey] = betterLevel(levelsByPos.get(posKey), level)
                levelsByWord[wordKey] = betterLevel(levelsByWord.get(wordKey), level)


print("Ładowanie słowników CEFR...")
for lang in CEFR_FILES:
    for path in CEFR_FILES[lang]:
        loadCefrFile(lang, path)


def getCefrLevel(lang, lemma, upos):
    if lang not in CEFR_FILES:
        return None

    word = lemma.lower()
    pos = CEFR_POS_MAP.get(upos, "")

    if (lang, word, pos) in levelsByPos:
        return levelsByPos[(lang, word, pos)]
    if (lang, word) in levelsByWord:
        return levelsByWord[(lang, word)]
    return CEFR_DEFAULT_LEVEL