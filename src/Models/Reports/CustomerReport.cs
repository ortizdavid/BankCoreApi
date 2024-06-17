namespace BankCoreApi.Models.Reports
{
    public class CustomerReport
    {
        public string? CustomerName { get; }
        public string? IdentificationNumber { get; }
        public string? Gender { get; }
        public DateTime BirthDate { get; }
        public string? Email { get; }
        public string? Phone { get; }
        public string? Address { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public string? CustomerType { get; }
        public string? CustomerStatus { get; }
    }
}