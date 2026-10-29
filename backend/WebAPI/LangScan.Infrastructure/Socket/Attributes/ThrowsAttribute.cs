using LangScan.Domain.Model;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace LangScan.Infrastructure.Socket.Attributes;

public class ThrowsAttribute(HttpStatusCode statusCode, string description = "")
    : SwaggerResponseAttribute((int)statusCode, description, typeof(ProblemInfo));
