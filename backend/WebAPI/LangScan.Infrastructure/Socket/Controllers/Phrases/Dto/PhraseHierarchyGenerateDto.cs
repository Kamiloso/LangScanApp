using System.ComponentModel.DataAnnotations;
using LangScan.Domain.Config;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseHierarchyGenerateDto(
    [MaxLength(Const.LEN_255)]
    string Name,

    [MaxLength(Const.LEN_65535)]
    string Description,

    long IdLangKnown,
    long IdLangLearning,

    [MaxLength(Const.LEN_255)]
    [RegularExpression("^(A1|A2|B1|B2|C1|C2)$")]
    string ProficiencyLevel,

    [MaxLength(Const.LEN_1048575)]
    string TextBlock);
