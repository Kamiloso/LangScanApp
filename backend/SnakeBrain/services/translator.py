import ctranslate2
from transformers import AutoTokenizer

from config import CT2_MODEL_DIR, NLLB_LANG_MAP

print("Ładowanie tokenizera NLLB...")
tokenizer = AutoTokenizer.from_pretrained(CT2_MODEL_DIR)

print("Ładowanie modelu NLLB...")
translator = ctranslate2.Translator(
    CT2_MODEL_DIR, device="cpu", compute_type="int8", inter_threads=1, intra_threads=2
)

def translateWords(words, sourceLang, targetLang):
    if not words:
        return []

    tokenizer.src_lang = NLLB_LANG_MAP[sourceLang]
    targetCode = NLLB_LANG_MAP[targetLang]

    sources = []
    targetPrefixes = []
    for word in words:
        sources.append(tokenizer.convert_ids_to_tokens(tokenizer.encode(word)))
        targetPrefixes.append([targetCode])

    results = translator.translate_batch(
        sources,
        target_prefix=targetPrefixes,
        beam_size=4,
        max_batch_size=8,
        max_decoding_length=32,
    )

    translations = []
    for result in results:
        hypothesis = result.hypotheses[0]
        ids = tokenizer.convert_tokens_to_ids(hypothesis[1:])
        translations.append(tokenizer.decode(ids, skip_special_tokens=True))
    return translations