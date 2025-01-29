using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Auth;

public class LoginRequest
{
    [Required]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}