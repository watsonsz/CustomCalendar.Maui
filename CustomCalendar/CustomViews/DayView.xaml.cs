using CustomCalendar.ViewModels;

namespace CustomCalendar.CustomViews;

public partial class DayView : ContentView
{
	public DayView()
	{
		InitializeComponent();
	}
    public DayView(DayViewModel date)
    {
        InitializeComponent();
        BindingContext = date;
    }

    public void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        //this gets the text being displayed, once an employee object is built employee names should be shown, but should SEND their GUID for checking.
        var check = picker.SelectedItem;
    }
}