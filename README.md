# 📄 Orders API (.NET + Redis) 

![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet?logo=dotnet)
![Redis](https://img.shields.io/badge/Redis-Cache-red?logo=redis)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-grey?logo=microsoftsqlserver)
![Swagger](https://img.shields.io/badge/Swagger-API%20Docs-brightgreen?logo=swagger)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-Pattern-blue)
![Status](https://img.shields.io/badge/Status-Completed-success)

This repository implements an **Orders API** using **ASP.NET Core**, **SQL Server**, and **Redis caching**, following clean architecture principles.

---

## 🚀 Tech Stack

- ASP.NET Core 8 Web API  
- Entity Framework Core  
- SQL Server  
- Redis (Docker)  
- StackExchange.Redis  
- Swagger / OpenAPI  
- Clean Architecture (API / Application / DAL)

---

# 📌 Features

### 🟢 Orders API

#### **POST /orders**
Creates a new order.

#### **GET /orders/{id}**
Fetches a single order with **Redis caching (5-minute TTL)**.

#### **GET /orders**
Retrieves all orders.

#### **DELETE /orders/{id}**
Deletes the order from SQL Server and Redis.

---

# 🧱 Order Model

```csharp
public class Order
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string Product { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

# 🗂 Project Structure

```
API/
 ├── Controllers/
 ├── Mapping/
 ├── Program.cs
 └── appsettings.json

Application/
 ├── DTOs/
 ├── Exceptions/
 ├── Interfaces/
 ├── Services/
 └── Settings/

DAL/
 ├── Migrations/
 ├── Models/
 ├── Repositories/
 └── AppDbContext.cs

```

---

# 🔴 Redis Setup

Start Redis in Docker:

```bash
docker run -d --name redis-dev -p 6379:6379 redis
```

Test connection:

```bash
docker exec -it my-redis redis-cli
```

Expected:
```bash
127.0.0.1:6379>
```

# 🛢 SQL Server Configuration

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=OrdersDb;Trusted_Connection=True;TrustServerCertificate=True;",
  "Redis": "localhost:6379"
}
```

---

# 🛠 Run EF Core Migrations

```powershell
Add-Migration Init
Update-Database
```

---

# 📘 Swagger UI

Run the API:

```bash
dotnet run
```

Open:

```
https://localhost:5105/
```

---

# 🧪 Example API Usage

### **Create Order**
```json
POST /orders
{
  "customerName": "Nancy",
  "product": "Laptop",
  "amount": 1200
}
```

### **Get Order**
```
GET /orders/{id}
```

### **List Orders**
```
GET /orders
```

### **Delete Order**
```
DELETE /orders/{id}
```

---


---

