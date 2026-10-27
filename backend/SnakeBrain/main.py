import random
import uvicorn
from fastapi import FastAPI
from pydantic import BaseModel
from typing import List

app = FastAPI(title="SnakeBrain Mock API")

class SnakeRequest(BaseModel):
    langKnownCode: str
    langLearningCode: str
    proficiencyLevel: str
    textBlock: str

class SnakeResponse(BaseModel):
    wordKnown: str
    wordLearning: str
    definitionKnown: str
    definitionLearning: str

MOCK_DATA = [
    SnakeResponse(
        wordKnown="jabłko",
        wordLearning="apple",
        definitionKnown="Okrągły owoc rosnący na drzewie",
        definitionLearning="A round fruit with red or green skin"
    ),
    SnakeResponse(
        wordKnown="kot",
        wordLearning="cat",
        definitionKnown="Małe zwierzę domowe",
        definitionLearning="A small domesticated carnivorous mammal"
    ),
    SnakeResponse(
        wordKnown="samochód",
        wordLearning="car",
        definitionKnown="Pojazd silnikowy na czterech kołach",
        definitionLearning="A four-wheeled road vehicle"
    ),
    SnakeResponse(
        wordKnown="rzeka",
        wordLearning="river",
        definitionKnown="Duży naturalny strumień wody",
        definitionLearning="A large natural stream of water flowing in a channel"
    ),
    SnakeResponse(
        wordKnown="drzewo",
        wordLearning="tree",
        definitionKnown="Wieloletnia roślina z drewnianym pniem",
        definitionLearning="A woody perennial plant with a trunk"
    )
]

@app.post("/api/nlp/process", response_model=List[SnakeResponse])
async def process_text_block(request: SnakeRequest):
    result = MOCK_DATA.copy()
    random.shuffle(result)
    
    count = random.randint(2, len(result))
    return result[:count]

if __name__ == "__main__":
    uvicorn.run("main:app", host="127.0.0.1", port=8081, reload=True)