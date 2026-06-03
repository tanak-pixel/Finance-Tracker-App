using System;
using System.Windows.Forms;
using System.Drawing;

public class MainDashboard : Form
{
    private TextBox txtAmount = null!;
    private ComboBox cmbCategory = null!;
    private ComboBox cmbType = null!;
    private Label lblSummary = null!;

    public MainDashboard()
    {
        TitleConfig();
        InitializeComponents();
        RefreshDashboard();
    }

    private void TitleConfig()
    {
        Text = "Personal Finance Tracker & Budget Dashboard";
        Size = new Size(850, 500);
        StartPosition = FormStartPosition.CenterScreen;
    }

    private void InitializeComponents()
    {
        // --- Input Panel Configuration ---
        Panel pnlInput = new Panel { Dock = DockStyle.Left, Width = 280, Padding = new Padding(15), BackColor = Color.FromArgb(245, 245, 245) };
        
        Label lblAmount = new Label { Text = "Amount ($):", Top = 20, Left = 15, Width = 240 };
        txtAmount = new TextBox { Top = 45, Left = 15, Width = 240 };

        Label lblCategory = new Label { Text = "Category:", Top = 85, Left = 15, Width = 240 };
        cmbCategory = new ComboBox { Top = 110, Left = 15, Width = 240 };
        cmbCategory.Items.AddRange(new object[] { "Food", "Rent", "Salary", "Utilities", "Entertainment", "Freelance" });
        cmbCategory.SelectedIndex = 0;

        Label lblType = new Label { Text = "Transaction Type:", Top = 150, Left = 15, Width = 240 };
        cmbType = new ComboBox { Top = 175, Left = 15, Width = 240 };
        cmbType.Items.AddRange(new object[] { "Income", "Expense" });
        cmbType.SelectedIndex = 1;

        Button btnSave = new Button { Text = "Log Transaction", Top = 230, Left = 15, Width = 240, Height = 35, BackColor = Color.DarkSlateGray, ForeColor = Color.White };
        btnSave.Click += LogTransaction_Click;

        pnlInput.Controls.AddRange(new Control[] { lblAmount, txtAmount, lblCategory, cmbCategory, lblType, cmbType, btnSave });
        Controls.Add(pnlInput);

        // --- Dashboard Summary Metrics Panel ---
        Panel pnlMetrics = new Panel { Dock = DockStyle.Top, Height = 80, BackColor = Color.DarkSlateGray };
        lblSummary = new Label { Dock = DockStyle.Fill, ForeColor = Color.White, Font = new Font("Arial", 14, FontStyle.Bold), TextAlign = ContentAlignment.MiddleCenter };
        pnlMetrics.Controls.Add(lblSummary);
        Controls.Add(pnlMetrics);
    }

    private void LogTransaction_Click(object? sender, EventArgs e)
    {
        if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
        {
            MessageBox.Show("Please enter a valid numeric transaction amount.", "Input Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string category = cmbCategory.SelectedItem?.ToString() ?? "Misc";
        string type = cmbType.SelectedItem?.ToString() ?? "Expense";

        DatabaseManager.AddTransaction(amount, category, type, DateTime.Now);
        txtAmount.Clear();
        
        RefreshDashboard();
    }

    private void RefreshDashboard()
    {
        // Re-calculate Financial Summary Totals
        decimal totalIncome = DatabaseManager.GetTotalByType("Income");
        decimal totalExpenses = DatabaseManager.GetTotalByType("Expense");
        decimal netSavings = totalIncome - totalExpenses;

        lblSummary.Text = $"Total Inflow: ${totalIncome:N2}   |   Total Outflow: ${totalExpenses:N2}   |   Net Savings: ${netSavings:N2}";
    }
}