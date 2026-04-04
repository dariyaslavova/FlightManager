# Flight Manager – ASP.NET MVC Project
- **by Dariya Slavova**

Flight Manager is a complete airline management system built with ASP.NET Core MVC, Entity Framework Core, and SQL Server.  
The project provides full functionality for managing flights, reservations, passengers, and user roles.

## Technologies Used

### Backend
- ASP.NET Core MVC
- C#
- Entity Framework Core
- LINQ
- Dependency Injection
- Session Authentication

### Authentication & Role Management
- Secure login system using sessions
- Two user roles:
  - **Admin** – full access to user management and flight management
  - **Employee** – access to flight operations and reservation handling

### Flight Management
- Create, edit, delete, and view flights
- Filtering by origin and destination
- Automatic duration calculation
- Pagination for large datasets

### Reservation System
- Multi-step reservation process:
  1. Select flight & enter email
  2. Add passengers
  3. Confirm reservation
- Passenger management
- Reservation overview and details

### User Management (Admin Only)
- Create, edit, delete users
- Automatic role assignment (first user = Admin)
- Filtering and pagination

## Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server / LocalDB
- X.PagedList
- Bootstrap 5
- Dependency Injection Architecture

## How to Run
1. Clone the repository  
2. Update database connection string  
3. Run migrations (if needed)  
4. Start the project  
5. First registered user becomes **Admin**

## Project Purpose
This project demonstrates:
- MVC architecture
- Clean separation of concerns
- Secure role-based access
- Real-world airline management logic
- Professional UI and UX structure
