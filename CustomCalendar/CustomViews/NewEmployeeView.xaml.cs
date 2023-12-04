using CommunityToolkit.Maui.Views;
using CustomCalendar.ViewModels;

namespace CustomCalendar.CustomViews;

public partial class NewEmployeeView : Popup
{
	public NewEmployeeView(EmployeeManagementViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}