namespace LangScan.Domain.Exceptions.Reasons;

public class InvalidCrudRequest() : AbortException(
    status: HttpStatus.BAD_REQUEST,
    code: "invalid_crud_request",
    message: "Provided CRUD request was invalid"
);
