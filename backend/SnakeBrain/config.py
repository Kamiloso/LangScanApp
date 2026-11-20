CT2_MODEL_DIR = "/app/nllb-600M-ct2"

NLLB_LANG_MAP = {
    "it": "ita_Latn",
    "de": "deu_Latn",
    "en": "eng_Latn",
    "es": "spa_Latn",
    "fr": "fra_Latn",
    "pl": "pol_Latn",
}

CONTENT = {"NOUN", "VERB", "ADJ", "ADV"}

STOP_LEMMAS = {
    "de": {"sein", "haben", "werden", "können", "müssen", "wollen", "sollen", "dürfen", "mögen"},
    "en": {"be", "have", "do"},
    "it": {"essere", "avere"},
    "es": {"ser", "estar", "haber"},
    "fr": {"être", "avoir"},
}

MAX_DEFINITIONS_PER_REQUEST = 1000
GEMMA_BATCH_SIZE = 5
GEMMA_TIMEOUT_SECONDS = 180
GEMMA_TOTAL_SECONDS = 600

CEFR_ORDER = ["A1", "A2", "B1", "B2", "C1", "C2"]
CEFR_DEFAULT_LEVEL = "C2"
CEFR_POS_MAP = {"NOUN": "noun", "VERB": "verb", "ADJ": "adjective", "ADV": "adverb"}

CEFR_FILES = {
    "en": [
        "/app/data/cefrj-vocabulary-profile-1.5.csv",
        "/app/data/octanove-vocabulary-profile-c1c2-1.0.csv",
    ],
}
