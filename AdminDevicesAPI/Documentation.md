# AdminDevicesAPI Documentation

---
## Introduction

The **AdminDevicesAPI** is a RESTful API designed for managing devices within an administrative context. It provides endpoints to create, retrieve, update, and delete devices. This is a short documentation, so we won’t overwhelm you with too many details — just the essentials to get you started. Enjoy!

---
### Technology Stack
- **ASP.NET Core**: For cross-platform development, performance, and modularity.
- **Entity Framework Core**: Simplifies database access and supports LINQ for querying.
- **SQL Server**: Reliable, scalable, and integrates well with .NET.
- **In-Memory Database for Testing**: For isolated, fast, and configuration-free testing.

### Design Decisions
- **Controllers**: Manages HTTP requests and responses, keeping the logic separate.
- **Repository Pattern**: Encapsulates database access logic, making it easier to maintain and test.
- **Service Layer**: Handles business logic, promoting separation of concerns.
- **DTOs (Data Transfer Objects)**: Decouples internal models from client-facing data, ensuring security and validation.
- **Enums**: Provides type safety, readability, and limits input to predefined values.
- **ModelState Validation**: Enforces data integrity, reducing risk of invalid input handling.
- **Async/Await**: For non-blocking, scalable code during I/O-bound operations.
- **Task-Based Methods**: Returns `Task` objects, allowing for parallel and efficient execution.
- **Interfaces**: Provides abstraction, promoting flexibility and easier testing.
- **Logging**: Uses structured logging for debugging, monitoring, and better issue tracking.

### Folder Structure
- **Controllers**: Manages API endpoints and HTTP request handling.
- **Models**: Represents database entities and application data.
- **DTOs**: Defines data structures for client-server communication.
- **Repositories**: Encapsulates data access logic and database operations.
- **Services**: Contains business logic and abstracts complexities from controllers.
- **Interfaces**: Defines contracts for various layers, enhancing testability and flexibility.
- **Responses**: Holds models for structured API responses.
- **Data**: Contains `DbContext` and SeedData classes to initialize and manage database state.
- **AdminDevicesAPI.Tests**: Separate folder for unit tests to ensure application stability.

This concise documentation provides clarity on each component and its purpose in the API architecture.

---
## Getting Started

To run the project locally:

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/AdminDevicesAPI.git
   ```

2. **Navigate to the Project Directory:**

   ```bash
   cd AdminDevicesAPI
   ```

3. **Restore Dependencies:**

   ```bash
   dotnet restore
   ```

4. **Configure the Database Connection:**

   - Update the `appsettings.json` file with your SQL Server connection string:

     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=your_server;Database=AdminDevicesDB;Trusted_Connection=True;"
       }
     }
     ```

5. **Apply Migrations:**

   ```bash
   dotnet ef database update
   ```

6. **Run the Application:**

   ```bash
   dotnet run
   ```

7. **Run Tests:**

   ```bash
   dotnet test
   ```

---

## Testing Strategy 

### Using In-Memory Database for Unit Tests

- **Purpose:** Simulate the database for quick and isolated testing.
- **Speed:** Tests run faster compared to using an actual database.
- **Simplicity:** No need for complex database setup or configurations.

### Scenarios Covered

- **Create Device:**
  - Validates device creation with correct data.
  - Ensures `ModelState` catches missing or invalid fields.
  
- **Retrieve Device:**
  - Confirms fetching of existing devices.
  - Handles cases where devices are not found.

- **Update Device:**
  - Checks updates with valid input.
  - Ensures `ModelState` prevents invalid updates.

- **Delete Device:**
  - Verifies successful deletions.
  - Handles non-existent device errors.

