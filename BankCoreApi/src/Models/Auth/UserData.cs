namespace BankCoreApi.Models.Auth;

public class UserData
{
    public int UserId { get; }
    public Guid UniqueId { get; }
    public string? UserName { get; }
    public string? Password { get; }
    public string? Image { get; }
    public bool IsActive { get; }
    public string? Token { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public int RoleId { get; }
    public string? RoleName { get; }
}
