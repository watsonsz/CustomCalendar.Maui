using CustomCalendar.CustomViews;

namespace CustomCalendar;

public partial class ViewSchedulePage : ContentPage
{
	public ViewSchedulePage()
	{
		InitializeComponent();
        BindingContext = this;
        DatePicker.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    }

    public DateTime DatePicked { get; set; }

    //This needs to go to SchedulePage.xaml.cs
    private void CreateCalendar(Object sender, EventArgs e)
    {
        var checkTwo = DatePicked;
        MainPanel.Add(new MonthView(checkTwo));
    }
}