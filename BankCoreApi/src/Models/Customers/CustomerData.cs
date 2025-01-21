namespace BankCoreApi.Models.Customers
{
    public class CustomerData
    {
        public int CustomerId { get; }
        public Guid UniqueId { get; }
        public string? CustomerName { get; }
        public string? IdentificationNumber { get; }
        public string? Gender { get; }
        public DateTime BirthDate { get; }
        public string? Email { get; }
        public string? Phone { get; }
        public string? Address { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public int TypeId { get; }
        public string? TypeName { get; }
        public int StatusId { get; }
        public string? StatusName { get; }
    }
}
