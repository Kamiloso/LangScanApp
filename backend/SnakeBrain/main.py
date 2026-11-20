from typing import List, Optional

from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, ValidationError

from config import CEFR_ORDER
from schemas import ProcessRequest, ProcessResponse
from services.flashcards import processFlashcardsText

app = FastAPI(title="SnakeBrain NLP API")


class SnakeRequest(BaseModel):
    langKnownCode: str
    langLearningCode: str
    proficiencyLevel: Optional[str] = None
    textBlock: str


class SnakeResponse(BaseModel):
    wordKnown: str
    wordLearning: str
    definitionKnown: Optional[str] = None
    definitionLearning: Optional[str] = None


def cleanLevel(level):
    if level is None:
        return None

    cleaned = level.strip().upper()
    if cleaned in CEFR_ORDER:
        return cleaned
    return None


@app.get("/health")
def healthCheck():
    return {"status": "ok"}


@app.post("/api/nlp/process", response_model=List[SnakeResponse])
def snakeProcessEndpoint(snakeRequest: SnakeRequest):
    try:
        request = ProcessRequest(
            text=snakeRequest.textBlock,
            learningLang=snakeRequest.langLearningCode.strip().lower(),
            knownLang=snakeRequest.langKnownCode.strip().lower(),
            proficiencyLevel=cleanLevel(snakeRequest.proficiencyLevel),
        )
    except ValidationError:
        raise HTTPException(status_code=400, detail="Nieobsługiwany język albo zbyt długi tekst.")

    response = processFlashcardsText(request)

    result = []
    for token in response.tokens:
        result.append(SnakeResponse(
            wordKnown=token.knownWord,
            wordLearning=token.learningWord,
            definitionKnown=token.knownDefinition,
            definitionLearning=token.learningDefinition,
        ))
    return result


@app.post("/process-text", response_model=ProcessResponse)
def processTextEndpoint(request: ProcessRequest):
    return processFlashcardsText(request)