using System;

namespace LangScan.Domain.Model.Ai;

public class AiFailException(string response, Exception? innerException = null)
    : Exception($"AI has failed to format the response: {response}", innerException);
