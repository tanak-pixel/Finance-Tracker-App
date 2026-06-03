using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

public class DatabaseManager
{
    private const string ConnectionString = "Data Source=finance.db";

    public static void InitializeDatabase()
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            
            // Re-structured schema optimized for SQLite compatibility
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Transactions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Amount DECIMAL(10,2) NOT NULL,
                    Category TEXT NOT NULL,
                    TransactionDate TEXT NOT NULL,
                    Type TEXT NOT NULL
                );";
            command.ExecuteNonQuery();
        }
    }

    public static void AddTransaction(decimal amount, string category, string type, DateTime date)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Transactions (Amount, Category, Type, TransactionDate) 
                VALUES ($amount, $category, $type, $date);";
            
            command.Parameters.AddWithValue("$amount", amount);
            command.Parameters.AddWithValue("$category", category);
            command.Parameters.AddWithValue("$type", type);
            command.Parameters.AddWithValue("$date", date.ToString("yyyy-MM-dd"));
            
            command.ExecuteNonQuery();
        }
    }

    public static Dictionary<string, double> GetExpenseCategorySummary()
    {
        var summary = new Dictionary<string, double>();
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            
            // Your Dashboard Query running over data persistence layer
            command.CommandText = @"
                SELECT Category, SUM(Amount) as Total 
                FROM Transactions 
                WHERE Type = 'Expense' 
                GROUP BY Category;";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string category = reader.GetString(0);
                    double total = reader.GetDouble(1);
                    summary[category] = total;
                }
            }
        }
        return summary;
    }

    public static decimal GetTotalByType(string type)
    {
        using (var connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT TOTAL(Amount) FROM Transactions WHERE Type = $type;";
            command.Parameters.AddWithValue("$type", type);
            return Convert.ToDecimal(command.ExecuteScalar() ?? 0);
        }
    }
}