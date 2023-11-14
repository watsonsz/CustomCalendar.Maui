namespace CustomCalendar
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
            Routing.RegisterRoute(nameof(EmployeeManagementPage), typeof(EmployeeManagementPage));
        }
    }
}