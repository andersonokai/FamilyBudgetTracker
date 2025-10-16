# Family Budget Tracker - User Documentation

## Table of Contents
- Getting Started
  - System requirements
  - How to run locally
  - Creating an account
- Using the App
  - Adding an Expense
  - Editing an Expense
  - Deleting an Expense
  - Searching and Filtering
  - Generating Reports
- Troubleshooting
  - Expenses not appearing
  - Cannot register or login
  - Reports empty after adding expenses
- FAQs
- Contact & Support

---

## Getting Started

### System requirements
- .NET 9.0 SDK
- Windows, macOS, or Linux
- Modern browser (Chrome, Edge, Firefox)

### How to run locally
1. Clone the repository and open the folder.
2. Restore packages and apply migrations:
   ```powershell
   dotnet restore
   dotnet ef database update
   ```
3. Run the project:
   ```powershell
   dotnet run
   ```
4. Open the URL shown in the terminal (e.g., `http://localhost:5234`).

### Creating an account
1. Click "Sign Up" or "Register" on the home page.
2. Fill in your email and password, then submit the form.
3. You will be redirected to a confirmation page; since this project uses a no-op email sender in development, you can log in immediately.

---

## Using the App

### Adding an Expense
1. Login to the application.
2. Navigate to the "Expenses" page.
3. Fill in the Date, Category, and Amount (in GHS). Amount must be greater than 0.
4. Click "Save". You should see a success notification and the expense will appear in the list.

Notes:
- Expenses are stored per-user. You must be logged in to add or view your expenses.

### Editing an Expense
1. From the expense list, click "Edit" next to the entry you want to modify.
2. Change the values and click "Save".

### Deleting an Expense
1. From the expense list, click "Delete" next to the entry.
2. The item will be removed after confirmation.

### Searching and Filtering
- Use the category filter or year input at the top of the "Expenses" page to filter results.

### Generating Reports
1. Navigate to the "Reports" page.
2. Choose a Year and Month, then click "Generate Report".
3. If there are expenses for that user in the chosen month, you'll see a chart breakdown by category.

---

## Troubleshooting

### Expenses not appearing
- Ensure you are logged in. The app stores expenses per user.
- Check the terminal output for any EF Core errors. Look for `INSERT` statements when adding an expense.
- Ensure `Data/app.db` exists and is writable.

### Cannot register or login
- Confirm migrations were applied (`dotnet ef database update`).
- Check `appsettings.json` for the correct connection string.

### Reports empty after adding expenses
- Verify the expenses were created for your user by checking the `Expenses` table in `Data/app.db` (use a SQLite viewer).
- Ensure the report month/year matches the expense date.

---

## FAQs
Q: Why can't I see other users' expenses?
A: Expenses are private and tied to your account. Each user sees only their own entries.

Q: Is data backed up?
A: Locally it's stored in SQLite. For production, deploy to Azure and use Azure SQL for reliability and backups.

---

## Contact & Support
For questions or help, open an issue in the GitHub repository: https://github.com/andersonokai/CSE325-Anderson-BlazorProject/issues

---

App by Anderson Okai © 2025
