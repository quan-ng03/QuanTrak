# 🌍 QuanTrak - Internet Market Tracker

![QuanTrak Logo](https://img.shields.io/badge/QuanTrak-Internet%20Market%20Tracker-blue)  
![Docker](https://img.shields.io/badge/Docker-Enabled-blue?logo=docker)
![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-purple?logo=blazor)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red?logo=microsoftsqlserver)
![.NET 8](https://img.shields.io/badge/.NET%208-Backend-blueviolet?logo=dotnet)

---

## 🚀 Welcome to **QuanTrak**!
QuanTrak is a **full-stack web application** designed to **track and analyze the global internet market across different countries**. It fetches real-time data, displays rankings, and provides insights through interactive tables and charts.

---
## 🏆 **Key Features**
✅ View **Internet Usage Rankings** by country  
✅ Compare **World Bank & ITU** internet statistics  
✅ **Search and filter** by country, region, and internet penetration  
✅ **Dark/Light Mode** with **custom UI themes**  
✅ **Interactive Charts** & Tables for data visualization  
✅ **RESTful API** powered by **ASP.NET Core 8.0**  
✅ **Containerized Deployment** with **Docker**  

---

## 📌 **Project Structure**
📂 **backend/** - RESTful API built with **ASP.NET Core**  
📂 **frontend/** - Single Page Application (SPA) built with **Blazor WebAssembly**  
📂 **database/** - SQL Server setup (future roadmap)  
📄 **docker-compose.yml** - Runs both backend and frontend as separate **Docker containers**  

---

## 🎯 **Future Roadmap**
📌 **Phase 1:** Deploy the database container in **Docker**  
📌 **Phase 2:** Expand the dataset by **allowing historical data (yearly records)**  
📌 **Phase 3:** Build **ML models** to analyze regions with the **best internet market**  
📌 **Phase 4:** Create an **interactive world map** to visualize internet adoption trends  

---

## 🛠 **Technologies Used**
- **Backend**: `.NET 8.0`, `ASP.NET Core`, `EF Core`, `SQL Server 2022`
- **Frontend**: `Blazor WebAssembly`, `Bootstrap`, `MudBlazor`
- **Database**: `SQL Server 2022`
- **Deployment**: `Docker`, `Nginx`
- **Testing**: `xUnit`
- **CI/CD**: `GitHub Actions` (Planned)

---

## 🏗 **Getting Started**
### ✅ **1. Initialize Your Database**
Ensure **SQL Server 2022** is installed. If using Docker, run:
```docker run --name sqlserver -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourPassword123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest```
### ✅ **2. Run the Application**
At the project root directory, run:
```docker-compose up -d --build```
### ✅ **3. Access the App**
Frontend: http://localhost:5030
Backend API: http://localhost:5181/api
🔥 API Endpoints
📍 Country API
GET /api/countries → List of all countries
GET /api/countries/details → Fetches country details sorted by internet usage
GET /api/countries/top10 → Returns top 10 ranked countries
GET /api/countries/{name} → Get internet statistics for a specific country
📍 Statistics API
PUT /api/internet/{code} → Update World Bank internet rate
GET /api/countries/ranking/{code} → Get country ranking
🧪 Unit Testing
QuanTrak uses xUnit for backend testing. Run:
```dotnet test```
🤝 Contributing
Want to contribute? Fork this repository and submit a PR! 🚀

📬 Issues / Feature Requests? Open an issue in the GitHub repository.

📜 License
📌 MIT License - Feel free to modify and use it!

🚀 Developed with ❤️ by Quan Nguyen

---
### **Added Elements**
✅ **Badges/icons** for technologies  
✅ **Organized structure** for easy understanding  
✅ **Command snippets** for easy setup  
✅ **API documentation**  
✅ **Future roadmap details**  

Would you like any more details or customization? 🚀
