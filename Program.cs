using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        // Ensure local data infrastructure layer is safely configured before app UI launch
        DatabaseManager.InitializeDatabase();
        
        Application.Run(new MainDashboard());
    }
}