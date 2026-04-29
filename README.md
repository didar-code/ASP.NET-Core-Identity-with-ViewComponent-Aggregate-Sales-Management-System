# 🚀 ASP.NET Core Identity Sales Management System

![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-SQL%20Server-green)
![Identity](https://img.shields.io/badge/Identity-Authentication-purple)
![CSharp](https://img.shields.io/badge/C%23-.NET-red)
![Pagination](https://img.shields.io/badge/X.PagedList-Enabled-orange)

A professional **ASP.NET Core 8 MVC web application** developed for complete **sales management with secure authentication and role-based access control**.
This project integrates **ASP.NET Core Identity, Entity Framework Core, SQL Server, X.PagedList pagination, search filtering, payment method management, and property relationship handling** in a clean and scalable architecture.

---

# ✨ Key Features

✅ ASP.NET Core Identity Authentication
✅ User Registration and Login System
✅ Role Based Authorization
✅ Admin Panel Management
✅ Sales CRUD Operations
✅ Property Management Module
✅ Payment Method Integration
✅ Search, Filter and Sorting
✅ Pagination using X.PagedList
✅ Entity Framework Core with SQL Server
✅ Responsive MVC Interface

---

# 🛠️ Technologies Used

* ⚙️ ASP.NET Core 8 MVC
* 🔐 ASP.NET Core Identity
* 🗄️ SQL Server
* 🔗 Entity Framework Core
* 💻 C#
* 📦 X.PagedList.Mvc.Core
* 🎨 Bootstrap
* 🧩 Razor View Engine

---

# 📂 Project Structure

```text
Controllers/
 ├── AccountController
 ├── AdminController
 ├── HomeController
 ├── SalesController

Models/
 ├── Sale
 ├── Property
 ├── PaymentMethod
 ├── AppDBContext

ViewModels/
 ├── LoginViewModel
 ├── RegisterViewModel
 ├── ManageUserRoleViewModel
 ├── HomeDashboardViewModel
```

---

# 🔐 Authentication & Authorization

This project uses **ASP.NET Core Identity** for secure user authentication.

### Included Security Features:

✅ Register
✅ Login
✅ Logout
✅ Role Assignment
✅ Protected Controller Access

Example:

```csharp
[Authorize]
public class SalesController : Controller
```

---

# 📊 Sales Module Functionalities

### Sales Controller Includes

✅ Create Sale
✅ Edit Sale
✅ Delete Sale
✅ Search by Client Name
✅ Search by Mobile Number
✅ Date Filter
✅ Payment Type Filter
✅ Paid / Unpaid Filter
✅ Sorting
✅ Pagination
✅ ViewComponent Implementation  
✅ Aggregate Functions for Dashboard Summary  
✅ Total Sales Calculation  
✅ Paid and Due Amount Summary  
✅ Reusable Dashboard Components  

---
# ✨ Key Features

✅ ASP.NET Core Identity Authentication  
✅ User Registration and Login System  
✅ Role Based Authorization  
✅ Admin Panel Management  
✅ Sales CRUD Operations  
✅ Property Management Module  
✅ Payment Method Integration  
✅ ViewComponent Implementation  
✅ Aggregate Functions for Sales Summary  
✅ Dashboard Summary with Total Sales, Paid Amount and Due Amount  
✅ Search, Filter and Sorting  
✅ Pagination using X.PagedList  
✅ Entity Framework Core with SQL Server  
✅ Responsive MVC Interface  

# 🧩 ViewComponent & Aggregate Summary

This project includes **ViewComponent** implementation to display reusable dashboard summary data in a clean and organized way.

The system also uses **aggregate functions** to calculate important business values such as:

- Total Sales Amount
- Total Paid Amount
- Total Due Amount
- Sales Count
- Payment Summary

This makes the dashboard more dynamic, informative, and useful for real business reporting.

# 🏢 Related Business Modules

### Property Module

* Property linked with sales
* Dynamic relationship handling

### Payment Method Module

* Cash
* Bank
* Other payment methods

---

# ▶️ How to Run This Project

## 1️⃣ Clone Repository

```bash
git clone https://github.com/your-username/your-repository-name.git
```

## 2️⃣ Open Project

Open solution file in Visual Studio:

```text
SalesCoreProjectWithIdentityViewCom.sln
```

## 3️⃣ Configure Database

Open:

```text
appsettings.json
```

Update connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "your-sql-server-connection"
}
```

## 4️⃣ Run Migration

```bash
Add-Migration InitialCreate
Update-Database
```

## 5️⃣ Run Application

```bash
F5
```
# 👨‍💻 Project Purpose

This project demonstrates practical implementation of:

✅ Real Authentication System
✅ Real Business CRUD Logic
✅ Entity Relationships
✅ Production Style MVC Architecture

# 🤝 Contribution

Pull requests are welcome.
For major improvements, open an issue first.

---
