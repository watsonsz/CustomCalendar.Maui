using CustomCalendar.ViewModels;
using CustomCalendar.BusinessEntity;

namespace CustomCalendar.CustomViews;

public partial class ViewDayView : ContentView
{
	public ViewDayView(DayViewModel date)
	{
		InitializeComponent();
		BindingContext = date;
		currentDate = date;
	}
	private DayViewModel currentDate;

  //  private void RemoveEmployee(object sender, EventArgs e)
  //  {
		//var button = sender as Button;
  //  }
}