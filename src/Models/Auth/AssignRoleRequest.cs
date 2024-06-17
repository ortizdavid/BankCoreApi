using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Auth
{
    public class AssignRoleRequest
    {
        [Required]
        public UserRole NewRole { get; set; }
    }
}