using CommunityToolkit.Maui.Views;
using CustomCalendar.CustomViews;
using CustomCalendar.ViewModels;
using System.Diagnostics.Eventing.Reader;

namespace CustomCalendar;

public partial class EmployeeManagementPage : ContentPage
{
	EmployeeManagementViewModel _model;
	public EmployeeManagementPage()
	{
		InitializeComponent();
		_model = new EmployeeManagementViewModel();
		BindingContext = _model;
	}

    void OnCheckBoxCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;
		if (checkBox.AutomationId == null) { }
		else
		{
			if(checkBox.IsChecked == true)
			{
				_model.AddToActive(checkBox.AutomationId.ToString());
			}
			else
			{
				_model.AddToInactive(checkBox.AutomationId.ToString());
			}
		}
		
    }

    private void GenNewEmployee(object sender, EventArgs e)
    {
		var Popup = new NewEmployeeView(_model);
		this.ShowPopup(Popup);
    }
}