using CustomCalendar.ViewModels;

namespace CustomCalendar.CustomViews;

public partial class ViewDayView : ContentPage
{
	public ViewDayView(DayViewModel date)
	{
		InitializeComponent();
		BindingContext = date;
	}
}