namespace BankCoreApi.Models.Reports
{
    public class CustomerReport
    {
        public string? CustomerName { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? CustomerType { get; set; }
        public string? CustomerStatus { get; set; }
    }
}