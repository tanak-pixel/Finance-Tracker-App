# Personal Finance Tracker & Budget Dashboard

A simple Windows desktop application for tracking personal income and expenses with real-time financial summaries.

## Features

- 💰 **Transaction Logging** - Add income and expense transactions with categories
- 📊 **Real-time Summary** - View total inflow, outflow, and net savings at a glance
- 📂 **Category Management** - Pre-defined categories: Food, Rent, Salary, Utilities, Entertainment, Freelance
- 💾 **SQLite Database** - All transactions are persisted locally
- 🎨 **Clean UI** - Intuitive Windows Forms interface

## Requirements

- **.NET 8 SDK** - [Download here](https://dotnet.microsoft.com/download)
- **Windows OS** - Windows 7 or later

## Installation

1. Clone or extract the project directory
2. Navigate to the project folder:
   ```powershell
   cd "D:\Programs\Finance Tracker App"
   ```

3. Restore dependencies:
   ```powershell
   dotnet restore
   ```

## Running the Application

### Option 1: Run Directly
```powershell
dotnet run
```

### Option 2: Build and Run
```powershell
dotnet build
dotnet bin\Debug\net8.0-windows\FinanceTrackerApp.exe
```

## Project Structure

```
Finance Tracker App/
├── Program.cs              # Application entry point
├── MainDashboard.cs        # Main UI form
├── DatabaseManager.cs      # Database operations & SQLite management
├── FinanceTrackerApp.csproj # Project configuration
├── finance.db             # SQLite database (auto-created on first run)
└── README.md              # This file
```

## How to Use

1. **Launch the app** - Run the application using one of the methods above
2. **Enter an amount** - Type a transaction amount in the "Amount ($)" field
3. **Select a category** - Choose from the predefined categories
4. **Select transaction type** - Choose "Income" or "Expense"
5. **Log transaction** - Click "Log Transaction" button
6. **View summary** - The top panel updates automatically with your totals

## Database

The application uses **SQLite** for local data storage. The database file (`finance.db`) is created automatically in the application directory on first run.

### Database Schema

**Transactions Table:**
| Column | Type | Description |
|--------|------|-------------|
| Id | INTEGER | Primary key |
| Amount | DECIMAL | Transaction amount |
| Category | TEXT | Transaction category |
| Type | TEXT | "Income" or "Expense" |
| TransactionDate | TEXT | Date of transaction (YYYY-MM-DD) |

## Future Enhancements

- 📈 Interactive pie charts for expense breakdown by category
- 📅 Date range filtering and reports
- 🏷️ Custom category creation
- 📤 Export transactions to CSV
- 📱 Budget goals and alerts

## Troubleshooting

**Build fails with missing dependencies:**
```powershell
dotnet restore
```

**Database errors:**
- Delete `finance.db` and restart the app to recreate the database

**Port already in use:**
- Ensure no other instances of the application are running

## License

This project is provided as-is for personal use.

## Support

For issues or questions, please review the source code or check the error messages displayed in the application.
