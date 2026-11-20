from typing import List, Literal, Optional
from pydantic import BaseModel, Field

LearningLanguage = Literal["de", "en", "it", "es", "fr"]
KnownLanguage = Literal["pl", "en"]
ProficiencyLevel = Literal["A1", "A2", "B1", "B2", "C1", "C2"]

class ProcessRequest(BaseModel):
    text: str = Field(..., max_length=5000)
    learningLang: LearningLanguage
    knownLang: KnownLanguage
    proficiencyLevel: Optional[ProficiencyLevel] = None

class TokenPair(BaseModel):
    knownWord: str
    learningWord: str
    knownDefinition: Optional[str] = None
    learningDefinition: Optional[str] = None
    proficiencyLevel: Optional[str] = None

class ProcessResponse(BaseModel):
    tokens: List[TokenPair]