using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Customers          
{
    public class ChangeCustomerStatusRequest
    {
        [Required]
        public string? IdentificationNumber { get; set; }

        [Required]
        public CustomerStatus NewStatus { get; set; }
    }

    public class ChangeCustomerTypeRequest
    {
        [Required]
        public string? IdentificationNumber { get; set; }

        [Required]
        public CustomerType NewType { get; set; }
    }

}