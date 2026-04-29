namespace SalesCoreProjectWithIdentityViewCom.Models.ViewModels
{
    public class ManageUserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<RoleSelection> Roles { get; set; } = new();
    }
}
