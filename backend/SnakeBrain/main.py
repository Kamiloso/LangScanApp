import uvicorn
from fastapi import FastAPI

app = FastAPI()

@app.get("/hello")
async def hello(message: str = ""):
    return {"message": message}

if __name__ == "__main__":
    uvicorn.run(app, host="0.0.0.0", port=8081)