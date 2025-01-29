using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Auth;

public class CreateUserRequest
{
    [Required]
    public UserRole UserRole { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Password { get; set;}
}