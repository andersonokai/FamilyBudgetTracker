# Family Budget Tracker

## Overview
Family Budget Tracker is a .NET Blazor web application designed to help Ghanaian families manage their household budgets. It allows users to securely log, categorize, and analyze expenses, generate monthly reports, and plan finances. The app supports GHS currency and is optimized for mobile and desktop use.

## Features
- User authentication (register, login, logout)
- Add, edit, delete, and view expenses
- Search/filter expenses by category or year
- Monthly budget reports with interactive charts
- Responsive UI for mobile and desktop
- GHS currency support
- Azure cloud deployment
- Footer with author and copyright

## Technologies
- .NET 9.0 Blazor
- Entity Framework Core (SQLite)
- ASP.NET Core Identity
- Bootstrap & Bootstrap Icons
- Plotly.Blazor for charts

## Getting Started
1. Clone the repository:
   ```
   git clone https://github.com/andersonokai/FamilyBudgetTracker.git
   ```
2. Navigate to the project folder:
   ```
   cd FamilyBudgetTracker
   ```
3. Restore dependencies and apply migrations:
   ```
   dotnet restore
   dotnet ef database update
   ```
4. Run the application:
   ```
   dotnet run
   ```
5. Open your browser to the address shown in the terminal (e.g., http://localhost:5234).

## Usage
- Register a new account and log in.
- Add expenses with amount, category, and date.
- Edit or delete expenses as needed.
- Filter expenses by category or year.
- View monthly reports on the Reports page.

## Project Structure
- `Components/Pages/` - Main Blazor pages (Home, Expenses, Reports, Auth)
- `Components/Account/Pages/` - Authentication and account management
- `Data/` - Entity Framework models, context, migrations
- `Services/` - Business logic (ExpenseService)
- `wwwroot/` - Static assets (CSS, Bootstrap)

## Error Handling
- Form validation for required fields and valid amounts
- User feedback on successful actions
- Robust error handling in backend services

## Accessibility & Design
- Responsive design for all devices
- Accessible forms and navigation
- Consistent branding and footer

## Deployment
- The app is ready for Azure deployment. See Azure DevOps board for deployment steps.

## Links
- [GitHub Repository](https://github.com/andersonokai/FamilyBudgetTracker.git)
- [Trello Board](https://trello.com/b/68cc7133d43f30ad9d62eeb2/anderson-okai-blazor-project)
- [Azure DevOps Board](https://okai-anderson-devops.visualstudio.com/Family%20Budget%20Tracker/_boards/board/t/Family%20Budget%20Tracker%20Team/Epics)

## Author
App by Anderson Okai © 2025

---

## Code Comments & Documentation
- All major classes and methods are commented for clarity.
- See inline comments in `ExpenseService`, `ApplicationDbContext`, and Blazor pages for details.

## Troubleshooting
- If expenses do not save, ensure you are logged in and the database is writable.
- If you see errors, check the terminal and browser console for details.
- For deployment, ensure your Azure environment supports SQLite or switch to Azure SQL.

## License
MIT
