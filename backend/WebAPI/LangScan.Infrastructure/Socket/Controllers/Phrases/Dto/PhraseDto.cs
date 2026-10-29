using System.ComponentModel.DataAnnotations;
using LangScan.Domain.Config;

namespace LangScan.Infrastructure.Socket.Controllers.Phrases.Dto;

public record PhraseDto(
    long Id,
    long IdPhraseGroup,

    [MaxLength(Const.LEN_4096)]
    string WordKnown,

    [MaxLength(Const.LEN_4096)]
    string WordLearning,

    [MaxLength(Const.LEN_4096)]
    string DefinitionKnown,

    [MaxLength(Const.LEN_4096)]
    string DefinitionLearning);
