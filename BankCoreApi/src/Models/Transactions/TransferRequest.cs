using System.ComponentModel.DataAnnotations;

namespace BankCoreApi.Models.Transactions;

public class TransferByIbanRequest
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
}


public class TransferByNumberRequest
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
}

    