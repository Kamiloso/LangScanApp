namespace LangScan.Domain.Exceptions.Reasons;

public class PhraseHierarchyNotFound() : AbortException(
    status: HttpStatus.NOT_FOUND,
    detail: "Phrase hierarchy cannot be found");
