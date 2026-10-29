using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace LangScan.Infrastructure.Socket.Other;

public class ThrowsAttribute(HttpStatusCode statusCode, string description = "")
    : SwaggerResponseAttribute((int)statusCode, description, typeof(ProblemDto));
