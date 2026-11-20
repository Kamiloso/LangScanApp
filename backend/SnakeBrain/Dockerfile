FROM python:3.10-slim

WORKDIR /app

ENV PYTHONUNBUFFERED=1 \
    MALLOC_ARENA_MAX=2 \
    MALLOC_TRIM_THRESHOLD_=100000 \
    OMP_NUM_THREADS=2 \
    TOKENIZERS_PARALLELISM=false

COPY requirements.txt .

RUN pip install --no-cache-dir -r requirements.txt

RUN ct2-transformers-converter \
        --model facebook/nllb-200-distilled-600M \
        --quantization int8 \
        --output_dir /app/nllb-600M-ct2 \
    && python -c "from transformers import AutoTokenizer; AutoTokenizer.from_pretrained('facebook/nllb-200-distilled-600M').save_pretrained('/app/nllb-600M-ct2')" \
    && rm -rf /root/.cache/huggingface/hub/models--facebook--nllb-200-distilled-600M

RUN python -c "import stanza; [stanza.download(l, processors='tokenize,mwt,pos,lemma') for l in ['de','it','en','es','fr']]"

ENV HF_HUB_OFFLINE=1

COPY main.py schemas.py config.py ./
COPY data/ ./data/
COPY services/ ./services/

EXPOSE 8000

CMD ["uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000", "--workers", "1"]