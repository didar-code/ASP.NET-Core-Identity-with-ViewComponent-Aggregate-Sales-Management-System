using SalesCoreProjectWithIdentityViewCom.Models;

namespace SalesCoreProjectWithIdentityViewCom.Models.ViewModels
{
    public class HomeDashboardViewModel
    {
        public bool IsAuthenticated { get; set; }
        public bool IsAdmin { get; set; }
        public int TotalSales { get; set; }
        public decimal TotalRevenue { get; set; }
        public int PaidSales { get; set; }
        public int UnpaidSales { get; set; }
        public int TotalUsers { get; set; }
        public int TotalRoles { get; set; }
        public int TotalProperties { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public List<Sale> RecentSales { get; set; } = new();
    }
}
