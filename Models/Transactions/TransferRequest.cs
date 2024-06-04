using System.ComponentModel.DataAnnotations;

namespace Name
{
    public class TransferIbanRequest
    {
        [Required]
        [StringLength(31, ErrorMessage = "Source IBAN must have {1} characters.")]
        public string? SourceIban { get; set; }
        
        [Required]
        [StringLength(31, ErrorMessage = "Destination IBAN must have {1} characters.")]
        public string? DestinationIban { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Currency must have {1} characters.")]
        public string? Currency { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Description must have {1} characters.")]
        public string? Description { get; set; }
    }


    public class TransferNumberRequest
    {
        [Required]
        [StringLength(18, ErrorMessage = "Source Number must have {1} characters.")]
        public string? SourceNumber { get; set; }
        
        [Required]
        [StringLength(18, ErrorMessage = "Destination Number must have {1} characters.")]
        public string? DestinationNumber { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Currency must have {1} characters.")]
        public string? Currency { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Description must have {1} characters.")]
        public string? Description { get; set; }
    }
}

        