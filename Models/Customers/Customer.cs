using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BankCoreApi.Helpers;

namespace BankCoreApi.Models.Customers
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Name must be between {2} and {1} characters.", MinimumLength = 10)]
        [Column("customer_name")]
        public string? CustomerName { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Identification must have {1} characters.")]
        [Column("identification_number")]
        public string? IdentificationNumber { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(150, ErrorMessage = "Email must be between {2} and {1} characters.", MinimumLength = 10)]
        [Column("email")]
        public string? Email { get; set; }
       
        [StringLength(20, ErrorMessage = "Phone must  have {1} characters.")]
        [Column("phone")]
        public string? Phone { get; set; }

        [StringLength(150, ErrorMessage = "Address must be between {2} and {1} characters.", MinimumLength = 10)]
        [Column("address")]
        public string? Address { get; set; }

        [Column("unique_id")]
        public Guid UniqueId { get; } = Encryption.GenerateUUID();
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

