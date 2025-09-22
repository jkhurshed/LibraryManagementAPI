📚 Library Management API

  The Library Management API is an ASP.NET Core Web API project designed to manage a digital library system. It allows administrators and users to handle books, authors, categories, inventories, loans, and reviews efficiently. The system is powered by Entity Framework Core and comes with preloaded JSON seed data, making it easy to set up and test.

✨ Features

  This project provides:
  
  A two-level category system, where books can be organized into main categories and subcategories.
  
  Comprehensive book management, including metadata such as title, ISBN, and description.
  
  Author management, with support for many-to-many relationships between books and authors.
  
  User management, where members and administrators have distinct roles.
  
  An inventory system that tracks available and reserved copies of each book.
  
  A loan system that manages borrowing and returning workflows, with due dates and statuses.
  
  A review system that allows users to leave feedback on books.

🛠️ Tech Stack

  This project is built using:
  
  ASP.NET Core 8 – for building the Web API.
  
  Entity Framework Core – for ORM and database management.
  
  SQL Server – as the primary relational database (can be adapted for PostgreSQL or SQLite).
  
  JSON seeding – for initial data population.

📂 Project Structure

  Models/ → Entity classes like Book, Author, Category, User, etc.
  
  Data/ → EF Core DbContext (LibDbContext) and seeding logic.
  
  Migrations/ → Database migrations.
  
  Controllers/ → API endpoints for managing entities.

🔮 Future Improvements

  The following enhancements are planned:
  
  Add authentication and role-based authorization for librarians and members.
  
  Implement loan rules, including due dates, penalties, and notifications.
  
  Provide a recommendation system using reviews and borrowing history.
  
  Add REST endpoints ready for frontend or mobile integration.

📜 License

  This project is open-source and available under the MIT License.
