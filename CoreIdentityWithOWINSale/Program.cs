using SalesCoreProjectWithIdentityViewCom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDBContext>(op =>
    op.UseSqlServer(builder.Configuration.GetConnectionString("con")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(op =>
{
    op.Password.RequiredLength = 5;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireDigit = false;
    op.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDBContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(op =>
{
    op.LoginPath = "/Account/Login";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Account", action = "Login" });

app.MapControllerRoute(
    name: "register",
    pattern: "Register",
    defaults: new { controller = "Account", action = "Register" });

app.MapControllerRoute(
    name: "logout",
    pattern: "Logout",
    defaults: new { controller = "Account", action = "Logout" });

app.MapControllerRoute(
    name: "dashboard",
    pattern: "Dashboard",
    defaults: new { controller = "Home", action = "Index" });
app.MapControllerRoute(
    name: "About",
    pattern: "About",
    defaults: new { controller = "Home", action = "Privacy" });

app.MapControllerRoute(
    name: "adminUsers",
    pattern: "Users",
    defaults: new { controller = "Admin", action = "Index" });

app.MapControllerRoute(
    name: "adminRoles",
    pattern: "Roles",
    defaults: new { controller = "Admin", action = "ListRoles" });

app.MapControllerRoute(
    name: "manageUserRoles",
    pattern: "users/{userId?}/roles",
    defaults: new { controller = "Admin", action = "ManageUserRoles" });

app.MapControllerRoute(
    name: "createRole",
    pattern: "roles/create",
    defaults: new { controller = "Admin", action = "CreateRole" });

app.MapControllerRoute(
    name: "deleteRole",
    pattern: "roles/delete/{id?}",
    defaults: new { controller = "Admin", action = "DeleteRole" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
