# 📌 Recipe Management Web Application (Backend)

## 🔗 Live API  
👉 https://recipe-ducwabaegshtc8f7.westeurope-01.azurewebsites.net  

## 🔗 Frontend Repository  
👉 https://github.com/ShameerNavas/RECIPE-Management-Frontend  

---

## 📖 Overview  
This is the backend of a full-stack Recipe Management Web Application built using ASP.NET Core Web API.  
It handles user authentication, recipe management, and database operations using RESTful services.

---

## 🚀 Features  

- 🔐 User Authentication with JWT Token  
- 👤 User Registration & Login  
- 🚫 Block / Unblock Users (Admin control)  
- 🍽️ Add, Edit, Delete Recipes  
- 📄 View Recipes and Recipe Details  
- 📊 Fetch user-specific recipes  
- 🌐 RESTful API architecture  

---

## 🛠️ Tech Stack  

- Backend: ASP.NET Core Web API  
- Language: C#  
- ORM: Entity Framework Core  
- Database: Azure SQL Database  
- Authentication: JWT (JSON Web Token)  
- API Testing: Swagger  
- Deployment: Azure App Service  
- CI/CD: GitHub Actions  

---

## 📂 Project Structure  

- Controllers/
  - UserController.cs

- Models/
  - User.cs
  - Recipe.cs

- DBContext/
  - MyDbContext.cs

- Program.cs  
- appsettings.json
  
---

## 🔐 Authentication (JWT)  

- JWT token is generated on successful login  
- Token contains:
  - UserId  
  - Name  
  - IsAdmin  

- Used for secure API access  

---

## 📡 API Endpoints  

### 🔐 Authentication  

- POST /api/User/signup  
- POST /api/User/login  

---

### 👤 User  

- GET /api/User/UserProfile/{userId}  
- PUT /api/User/UpdatePassword/{userId}  

---

### 🍽️ Recipes  

- GET /api/User/Feed  
- GET /api/User/RecipeProfile/{userId}  
- GET /api/User/RecipeDetail/{recipeId}  

- POST /api/User/add-recipe  
- PUT /api/User/EditRecipe/{recipeId}  
- DELETE /api/User/DeleteRecipe/{recipeId}
---

## ⚙️ Setup & Installation  

# Clone repository
git clone https://github.com/ShameerNavas/RECIPE-Management-Backend.git

# Open in Visual Studio

# Restore packages

dotnet restore

# Run project

dotnet run

---

## 🗄️ Database Configuration  

Connection string (Azure SQL):

"ConnectionStrings": {
  "DefaultConnection": "Server=tcp:recipeproject.database.windows.net,1433;Initial Catalog=recipe-db;Persist Security Info=False;User ID=recipeadmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
}

---

## 🔄 Entity Framework Migrations  

### Add Migration  

dotnet ef migrations add InitialCreate
### Update Database  

dotnet ef database update

---

## 🌍 Deployment  

- Hosted on Azure App Service  
- Connected with GitHub for CI/CD  
- Automatic deployment on code push  

---

## 🧠 Architecture  

Client (React Frontend)
        ↓
ASP.NET Core Web API
        ↓
Entity Framework Core
        ↓
Azure SQL Database

---

## 🎯 Key Highlights  

- Secure authentication using JWT  
- Clean REST API design  
- Scalable architecture  
- Cloud deployment with CI/CD  
- Integrated with frontend (React)

---

## 👨‍💻 Author  

Shameer Navas  
📧 shameernavas10@gmail.com
