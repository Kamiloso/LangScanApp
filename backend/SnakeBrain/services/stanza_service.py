import gc
import stanza
import torch

MAX_LOADED_PIPELINES = 3
PINNED_LANGS = []  

stanzaPipelines = {}
stanzaUsageOrder = []  


def getStanzaPipeline(lang):
    if lang in stanzaPipelines:
        stanzaUsageOrder.remove(lang)
        stanzaUsageOrder.append(lang)
        return stanzaPipelines[lang]

    while len(stanzaPipelines) >= MAX_LOADED_PIPELINES:
        oldestLang = None
        for candidate in stanzaUsageOrder:
            if candidate not in PINNED_LANGS:
                oldestLang = candidate
                break

        if oldestLang is None:
            break

        stanzaUsageOrder.remove(oldestLang)
        del stanzaPipelines[oldestLang]
        gc.collect()
        if torch.cuda.is_available():
            torch.cuda.empty_cache()

    stanzaPipelines[lang] = stanza.Pipeline(
        lang,
        processors="tokenize,mwt,pos,lemma",
        use_gpu=torch.cuda.is_available(),
        logging_level="WARN",
        download_method=None,
    )
    stanzaUsageOrder.append(lang)
    return stanzaPipelines[lang]