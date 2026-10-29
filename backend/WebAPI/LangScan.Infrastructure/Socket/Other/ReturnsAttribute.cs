using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace LangScan.Infrastructure.Socket.Other;

public class ReturnsAttribute(HttpStatusCode statusCode, string description = "")
    : SwaggerResponseAttribute((int)statusCode, description);

public class ReturnsAttribute<T>(HttpStatusCode statusCode, string description = "")
    : SwaggerResponseAttribute((int)statusCode, description, typeof(T));
