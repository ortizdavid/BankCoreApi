namespace BankCoreApi.Models.Reports
{
    public class DateRangeFilter
    {
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1).Date;
        public DateTime EndDate { get; set; } = DateTime.Now.Date;
    }
}