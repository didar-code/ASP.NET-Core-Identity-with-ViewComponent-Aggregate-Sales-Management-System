# 🚀 ASP.NET Core Identity Sales Management System
<img width="482" height="701" alt="Dasboard" src="https://github.com/user-attachments/assets/e9b69f07-0ccf-44c1-8377-489541c1dc5f" />


![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-SQL%20Server-green)
![Identity](https://img.shields.io/badge/Identity-Authentication-purple)
![CSharp](https://img.shields.io/badge/C%23-.NET-red)
![Pagination](https://img.shields.io/badge/X.PagedList-Enabled-orange)

A professional **ASP.NET Core 8 MVC web application** developed for complete **sales management with secure authentication and role-based access control**.
This project integrates **ASP.NET Core Identity, Entity Framework Core, SQL Server, X.PagedList pagination, search filtering, payment method management, and property relationship handling** in a clean and scalable architecture.

---

# ✨ Key Features
<img width="500" height="739" alt="ManagesSales" src="https://github.com/user-attachments/assets/e7bb8f51-628c-48b3-91af-be0b04c77c43" />
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
<img width="941" height="667" alt="Login" src="https://github.com/user-attachments/assets/e5e67426-8618-49a0-9f15-03a335a5dbe7" />
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
<img width="1004" height="785" alt="Admin" src="https://github.com/user-attachments/assets/82f7891c-9bbe-4f24-b55a-337d887bf265" />
<img width="872" height="768" alt="Create" src="https://github.com/user-attachments/assets/706d26c0-bff8-4e1d-b798-69290b9f1038" />
<img width="947" height="801" alt="Edit" src="https://github.com/user-attachments/assets/da58eaf7-d64f-46c8-aae5-9f9a283a491a" />

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
git clone https://github.com/didar-code/ASP.NET-Core-Identity-with-ViewComponent-Aggregate-Sales-Management-System.git
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
---

# ▶️ How to Run This Project (ZIP Method)

If you are not using Git, you can run this project by downloading the ZIP file.

## 1️⃣ Download ZIP

- Go to the GitHub repository  
- Click on the **Code** button  
- Select **Download ZIP**

## 2️⃣ Extract Files

- Right-click the downloaded ZIP file  
- Click **Extract Here** or **Extract All**  
- Open the extracted project folder  

## 3️⃣ Open in Visual Studio

- Locate the solution file (`.sln`)  
  Example:
  ```text
  SalesCoreProjectWithIdentityViewCom.sln

  Double-click to open in Visual Studio
4️⃣ Configure Database

Open:

appsettings.json

Update your SQL Server connection string:

"ConnectionStrings": {
  "DefaultConnection": "your-sql-server-connection"
}
5️⃣ Run Migration

Open Package Manager Console and run:

Add-Migration InitialCreate
Update-Database
6️⃣ Run the Project
Press F5 or click Run
The application will start in your browser
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
