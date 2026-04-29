namespace SalesCoreProjectWithIdentityViewCom.Models.ViewModels
{
    public class SalesSummaryViewModel
    {
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PaidSales { get; set; }
        public int UnpaidSales { get; set; }
        public decimal PaidRevenue { get; set; }
        public decimal UnpaidRevenue { get; set; }
        public int TotalProperties { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }
}
