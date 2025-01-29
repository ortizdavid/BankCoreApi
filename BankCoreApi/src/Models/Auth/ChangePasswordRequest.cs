using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Auth;

public class ChangePasswordRequest
{
    [Required]
    public string? NewPassword { get; set; }
}