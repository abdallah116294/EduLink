# 🎓 EduLink - Educational Management System

A comprehensive educational management platform built with **ASP.NET Core** using **Clean Architecture** principles, designed to streamline school administration and enhance educational operations.

![.NET](https://img.shields.io/badge/.NET-8.0-blue)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-Core_8.0-green)
![SQL Server](https://img.shields.io/badge/SQL_Server-2019+-orange)
![XUnit](https://img.shields.io/badge/XUnit-Testing-red)
![License](https://img.shields.io/badge/License-MIT-yellow)

## 📋 Table of Contents
- [Features](#-features)
- [Architecture](#-architecture)
- [Technology Stack](#-technology-stack)
- [Project Structure](#-project-structure)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Testing](#-testing)
- [Database Schema](#-database-schema)
- [Contributing](#-contributing)
- [License](#-license)

## ✨ Features

### 👥 **User Management**
- Multi-role authentication (Students, Teachers, Parents, Admin, Staff)
- Secure login with ASP.NET Core Identity
- Role-based access control
- User profile management

### 🎒 **Student Management**
- Student enrollment and registration
- Academic records tracking
- Attendance monitoring
- Grade and performance analytics
- Parent-student relationship management

### 👨‍🏫 **Staff Management**
- Academic and non-academic staff registration
- Department assignment and management
- Specialization tracking
- Staff performance monitoring

### 📚 **Academic Management**
- Class creation and management
- Subject assignment to teachers
- Academic year management
- Exam scheduling and management
- Grade recording and reporting

### 💰 **Financial Management**
- Fee structure management
- Payment tracking and monitoring
- Outstanding dues reporting
- Financial analytics

### 📊 **Reporting & Analytics**
- Student performance reports
- Attendance analytics
- Financial summaries
- Custom report generation

## 🏗️ Architecture

This project follows **Clean Architecture** principles with clear separation of concerns:

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│   Presentation  │    │    Business     │    │      Data       │
│      Layer      │────│     Layer       │────│     Layer       │
│   (API/UI)      │    │   (Services)    │    │  (Repository)   │
└─────────────────┘    └─────────────────┘    └─────────────────┘
         │                       │                       │
         └───────────────────────┼───────────────────────┘
                                 │
                    ┌─────────────────┐
                    │      Core       │
                    │   (Entities)    │
                    └─────────────────┘
```

### Design Patterns Implemented:
- **Repository Pattern** - Data access abstraction
- **Unit of Work Pattern** - Transaction management
- **Specification Pattern** - Query object pattern
- **Dependency Injection** - Loose coupling
- **CQRS Pattern** - Command Query Responsibility Segregation

## 🛠️ Technology Stack

- **Framework:** ASP.NET Core 8.0
- **Database:** SQL Server 2019+
- **ORM:** Entity Framework Core 8.0
- **Authentication:** ASP.NET Core Identity
- **Testing:** XUnit, Moq, FluentAssertions
- **Documentation:** Swagger/OpenAPI
- **Logging:** Serilog
- **Validation:** FluentValidation

## 📁 Project Structure

```
EduLink/
├── 📁 EduLink.API/                 # Presentation Layer
│   ├── Controllers/                # API Controllers
│   ├── Extensions/                 # Service registrations
│   └── Program.cs                  # Application entry point
│
├── 📁 EduLink.Core/               # Domain Layer
│   ├── Entities/                   # Domain entities
│   ├── Interfaces/
│   │   ├── IServices/             # Service contracts
│   │   └── IRepositories/         # Repository contracts
│   ├── Specifications/            # Query specifications
│   └── Enums/                     # Domain enumerations
│
├── 📁 EduLink.Repository/         # Data Access Layer
│   ├── Data/
│   │   ├── ApplicationDbContext.cs
│   │   ├── Configurations/        # Entity configurations
│   │   └── Migrations/            # EF migrations
│   ├── Repositories/              # Repository implementations
│   └── UnitOfWork/                # Unit of work implementation
│
├── 📁 EduLink.Services/           # Business Logic Layer
│   ├── Services/                  # Service implementations
│   └── Mappings/                  # AutoMapper profiles
│
├── 📁 EduLink.Utilities/          # Shared Utilities
│   ├── DTOs/                      # Data Transfer Objects
│   ├── Helpers/                   # Helper classes
│   └── Extensions/                # Extension methods
│
└── 📁 EduLink.Tests/              # Test Layer
    ├── UnitTests/                 # Unit tests
    ├── IntegrationTests/          # Integration tests
    └── TestHelpers/               # Test utilities
```

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone [https://github.com/yourusername/EduLink.git](https://github.com/abdallah116294/EduLink)
   cd EduLink
   ```

2. **Configure the database connection**
   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=EduLinkDb;Trusted_Connection=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update --project EduLink.Repository --startup-project EduLink.API
   ```

4. **Build and run the application**
   ```bash
   dotnet build
   dotnet run --project EduLink.API
   ```

5. **Access the application**
   - API: `http://edulink.runasp.net/swagger/index.html`
   - Swagger UI: `[https://localhost:7001/swagge](http://edulink.runasp.net/swagger/index.html)r`

### Docker Support

```bash
# Build and run with Docker
docker build -t edulink-api .
docker run -p 8080:80 edulink-api
```

## 📖 API Documentation

The API is documented using Swagger/OpenAPI. Once the application is running, visit:
- **Swagger UI:** `(http://edulink.runasp.net/swagger/index.html)`
- **OpenAPI JSON:** `(http://edulink.runasp.net/swagger/v1/swagger.json)`

### Key Endpoints

| Endpoint | Method | Description |
|----------|--------|-------------|
| `/api/students` | GET | Get all students |
| `/api/students/{id}` | GET | Get student by ID |
| `/api/students` | POST | Create new student |
| `/api/teachers` | GET | Get all teachers |
| `/api/classes` | GET | Get all classes |
| `/api/grades` | POST | Record grades |
| `/api/attendance` | POST | Record attendance |

## 🧪 Testing

The project includes comprehensive unit tests using XUnit framework.

```bash
# Run all tests
dotnet test

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test EduLink.Tests/
```

### Test Structure
- **Unit Tests:** Test individual components in isolation
- **Integration Tests:** Test component interactions
- **Repository Tests:** Test data access layer
- **Service Tests:** Test business logic layer

### Test Coverage
- Services: 95%+
- Repositories: 90%+
- Controllers: 85%+

## 🗄️ Database Schema

The system manages the following core entities:

- **Users** - Base user information with Identity integration
- **Students** - Student-specific data and academic records
- **AcademicStaff** - Teachers and academic personnel
- **NonAcademicStaff** - Administrative and support staff
- **Parents** - Parent/guardian information
- **Classes** - Class/grade level management
- **Subjects** - Course/subject definitions
- **Grades** - Student performance records
- **Attendance** - Daily attendance tracking
- **Fees** - Financial management
- **Departments** - Organizational structure
- **AcademicYears** - Academic calendar management

### Entity Relationships
```
Users (1) ←→ (1) Students ←→ (∞) Grades
Users (1) ←→ (1) Parents ←→ (∞) Students
Users (1) ←→ (1) AcademicStaff ←→ (∞) Subjects
Classes (1) ←→ (∞) Students
Classes (1) ←→ (∞) Subjects
Students (1) ←→ (∞) Attendance
Students (1) ←→ (∞) Fees
```

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

### Development Workflow
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Coding Standards
- Follow C# naming conventions
- Write unit tests for new features
- Update documentation as needed
- Use meaningful commit messages

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**[Your Name]**
- GitHub: [@yourusername]((https://github.com/abdallah116294))
- LinkedIn: [Your LinkedIn](www.linkedin.com/in/abdallha-mohamed-b66926209)
- Email: abdallhamohamed116@gmail.com

## 🙏 Acknowledgments

- ASP.NET Core team for the excellent framework
- Entity Framework team for the powerful ORM
- XUnit team for the testing framework
- All contributors who helped shape this project

---

⭐ **If you found this project helpful, please give it a star!** ⭐

---

*Built with ❤️ using Clean Architecture principles*
