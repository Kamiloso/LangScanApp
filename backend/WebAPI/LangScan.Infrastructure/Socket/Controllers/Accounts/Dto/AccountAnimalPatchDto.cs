using System.ComponentModel.DataAnnotations;

namespace LangScan.Infrastructure.Socket.Controllers.Accounts.Dto;

public record AccountAnimalPatchDto(
    int AnimalHealth,

    [Required]
    string AnimalFood);
