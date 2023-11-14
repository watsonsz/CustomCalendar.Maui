using CustomCalendar.ViewModels;

namespace CustomCalendar;

public partial class EmployeeManagementPage : ContentPage
{
	public EmployeeManagementPage()
	{
		InitializeComponent();
		BindingContext = new EmployeeManagementViewModel();
	}
}