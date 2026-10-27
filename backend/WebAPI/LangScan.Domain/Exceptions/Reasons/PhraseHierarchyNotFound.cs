namespace LangScan.Domain.Exceptions.Reasons;

public class PhraseHierarchyNotFound() : AbortException(
    status: HttpStatus.NOT_FOUND,
    code: "phrase_hierarchy_not_found",
    message: "Phrase hierarchy cannot be found"
);
