using System.ComponentModel.DataAnnotations;

namespace SalesCoreProjectWithIdentityViewCom.Models
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int PaymentTypeId { get; set; }

        public string PaymentType { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
