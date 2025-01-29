using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Customers;

public class CreateCustomerRequest
{
    [Required]
    public CustomerType CustomerType { get; set; }

    [Required]
    [StringLength(150, ErrorMessage = "Name must be between {2} and {1} characters.", MinimumLength = 10)]
    public string? CustomerName { get; set; }

    [Required]
    public string? Gender { get; set; }
    
    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [StringLength(30, ErrorMessage = "Identification must have {1} characters.")]
    public string? IdentificationNumber { get; set; }

    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(150, ErrorMessage = "Email must be between {2} and {1} characters.", MinimumLength = 10)]
    public string? Email { get; set; }
   
    [StringLength(20, ErrorMessage = "Phone must  have {1} characters.")]
    public string? Phone { get; set; }

    [StringLength(150, ErrorMessage = "Address must be between {2} and {1} characters.", MinimumLength = 10)]
    public string? Address { get; set; }
}