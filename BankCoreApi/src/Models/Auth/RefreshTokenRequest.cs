using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Auth;

public class RefreshTokenRequest
{
    [Required]
    public string? RefreshToken { get; set; }
}