SMART CAMPUS SERVICE PORTAL - ASP.NET CORE MVC

REQUIREMENTS
- Visual Studio 2022 with ASP.NET and web development workload
- .NET 10 SDK
- Internet connection for first NuGet restore and Bootstrap CDN

OPEN AND RUN
1. Extract SmartCampusPortal.zip.
2. Open SmartCampusPortal.csproj in Visual Studio.
3. Allow NuGet packages to restore.
4. Press F5 or click the green Run button.
5. The SQLite database file smartcampus.db is created automatically.

DEMO ACCOUNTS
Student:
  Email: student@campus.com
  Password: Student@123

Administrator:
  Email: admin@campus.com
  Password: Admin@123

FEATURES
- Student registration and login
- Password hashing
- Student dashboard
- Submit campus service requests
- View and track own requests
- Admin dashboard and request status updates
- Admin remarks
- Campus notices and admin notice publishing
- SQLite database persistence

DATABASE
SQLite database: smartcampus.db
It is created and seeded automatically on first run.

NOTE
This is an educational practical starter project. For real deployment, add
production-grade authentication/authorization, HTTPS configuration, CSRF
protection review, rate limiting, audit logs, and database migrations.
