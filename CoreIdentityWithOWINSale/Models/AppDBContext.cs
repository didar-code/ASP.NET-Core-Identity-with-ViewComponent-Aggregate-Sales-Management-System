using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SalesCoreProjectWithIdentityViewCom.Models
{
    public class AppDBContext:IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> op) : base(op)
        {
        }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
    }
}
