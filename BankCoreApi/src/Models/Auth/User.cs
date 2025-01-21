using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Auth
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public UserRole UserRole { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public bool? IsActive { get; set; }

        public string? Image { get; set; }

        public Guid UniqueId { get; set; } = Encryption.GenerateUUID();

        public string? Token { get; set; } = Encryption.GenerateRandomToken(32);

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}