# EComCart - ASP.NET Core MVC E-Commerce Application

## Project Overview

EComCart is a full-stack ASP.NET Core MVC web application that demonstrates complete CRUD (Create, Read, Update, Delete) operations across multiple related entities. This project follows modern MVC architecture, uses Entity Framework Core with Code-First migrations, and implements proper database relationships including One-to-Many and Many-to-Many using a junction table.

This project was developed as part of the Group Project requirement for ASP.NET Core MVC coursework.

---

## Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Razor Views
- C#
- Visual Studio 2022
- Git & GitHub

---

## Database Design

### Entities

The system contains 5 main entities:

1. Category  
2. Product  
3. Customer  
4. Order  
5. OrderItem (Junction Table)  

---

### Relationships

- Category → Product (One-to-Many)
- Customer → Order (One-to-Many)
- Order → OrderItem (One-to-Many)
- Product → OrderItem (One-to-Many)

Many-to-Many relationship:

- Order ↔ Product (implemented using OrderItem junction table)

---

## Features

- Full CRUD operations for all entities
- Proper MVC architecture
- Entity Framework Core Code-First migrations
- Navigation properties and foreign keys
- ViewModels for dropdown lists and complex views
- Bootstrap responsive UI
- SQL Server database integration

---

## Project Structure
EComCart/
│
├── Controllers/
│ ├── CategoryController.cs
│ ├── ProductController.cs
│ ├── CustomerController.cs
│ ├── OrderController.cs
│ └── OrderItemController.cs
│
├── Models/
│ ├── Category.cs
│ ├── Product.cs
│ ├── Customer.cs
│ ├── Order.cs
│ └── OrderItem.cs
│
├── ViewModels/
├── Views/
├── Data/
│ └── AppDbContext.cs
│
├── appsettings.json
└── Program.cs

## CRUD Operations

The application supports full CRUD operations for:

- Categories
- Products
- Customers
- Orders
- OrderItems

## Team Members

- Hirenkumar Patel  
- Aum Patel  
- Fenilkumar Chaudhari  
- Gopichand Bollipalli 

## GitHub Repository
(https://github.com/hpatel9986/EComCart.git)
