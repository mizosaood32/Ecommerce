# MVC Project - Bookstore Management System

## Overview
This project is a **Bookstore Management System** developed using **MVC architecture** and follows a **3-tier structure**. It provides separate dashboards for **Admin** and **User**, enabling efficient management of the bookstore.

## Features

### **1. User & Admin Registration**
- Secure authentication for both **users** and **admins**.
- Role-based access control to restrict functionalities.

---

### **2. Admin Dashboard**
Admin can:
- **Add Books to Store** → Upload books with details like title, author, price, and description.
- **Refill Stock** → Update book quantities when stock runs low.
- **Add Genre to Store** → Categorize books under different genres.
- **Top Sold Books** → View the best-selling books based on sales data.
- **Manage Profile** → Update admin profile details.
- **Manage Cart** → Monitor user carts and orders.

---

### **3. User Dashboard**
User can:
- **Manage Profile** → Edit personal details and preferences.
- **Manage Cart** → Add, remove, and update books in the shopping cart.
- **Manage Payment** → Process transactions securely for book purchases.

## **Architecture**
This project follows a **3-tier architecture**, ensuring modularity and maintainability:

1. **Presentation Layer (UI)** → Handles user interface with HTML, CSS, JavaScript, and Razor Views.
2. **Business Logic Layer (BLL)** → Contains core logic and validation.
3. **Data Access Layer (DAL)** → Manages database interactions using SQL Server.

## **Technology Stack**
- **Frontend**: HTML, CSS, JavaScript, Bootstrap
- **Backend**: C# (ASP.NET MVC)
- **Database**: SQL Server
- **Payment Integration**: Stripe (for user transactions)

---

## **Setup & Installation**
1. Clone the repository:  
   ```sh
   git clone https://github.com/your-repo/bookstore-mvc.git
   ```
2. Open the project in **Visual Studio**.
3. Set up the **SQL Server database** and update connection strings in `appsettings.json`.
4. Run the application and access it via `http://localhost:port`.

## **Contributing**
Feel free to submit issues and pull requests to enhance the project!

## **License**
This project is licensed under the MIT License.

---

### 📌 **Author**: Mohamed Abo El Saood  
Happy Coding! 🚀


