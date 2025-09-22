📚 Library Management API

A medium-complexity ASP.NET Core Web API project for managing books, authors, categories, users, loans, and reviews in a digital library system. Built with Entity Framework Core and seeded with JSON data for easy setup.

🚀 Features

Categories – two-level hierarchy with subcategories.

Books – store metadata (title, ISBN, description) and assign to categories.

Authors – manage author info and link to multiple books.

Users – library members and admins with roles.

Inventory – track available/reserved copies of books.

Loans – borrow/return workflow with dates and status.

Reviews – users can leave reviews for books.

🗄️ Tech Stack

ASP.NET Core 8 Web API

Entity Framework Core

SQL Server (or PostgreSQL/SQLite)

JSON-based data seeding

🔮 Future Improvements

Authentication & role-based access (librarian vs. member).

Loan rules with due dates and late fees.

Recommendation system based on reviews & history.

REST endpoints for frontend/mobile integration.
