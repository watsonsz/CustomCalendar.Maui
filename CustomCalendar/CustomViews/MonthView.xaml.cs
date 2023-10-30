using CustomCalendar.ViewModels;

namespace CustomCalendar.CustomViews;

public partial class MonthView : ContentView
{
    private MonthViewViewModel _viewModel;
    private static int ROW_Start = 2;

	public MonthView()
	{
		InitializeComponent();
        _viewModel = new MonthViewViewModel();
        BindingContext = _viewModel;
        CreateDayBlocks();
	}


    public MonthView(int month)
    {
        InitializeComponent();
        _viewModel = new MonthViewViewModel(month);
        BindingContext = _viewModel;
        CreateDayBlocks();

    }
    public MonthView(int month, int year)
    {
        InitializeComponent();
        _viewModel = new MonthViewViewModel(month);
        BindingContext = _viewModel;
        CreateDayViewBlocks();

    }

    private void CreateDayViewBlocks()
    {
        int row = ROW_Start;
        foreach (var day in _viewModel.Days)
        {
            int column = (int)day.Date.DayOfWeek;
            MonthLayout.Add(new ViewDayView(day), column, row);
            if (column == 6)
            {
                row++;
            }
        }
    }

    public void CreateDayBlocks()
    {
        int row = ROW_Start;
        foreach(var day in _viewModel.Days)
        {
            int column = (int)day.Date.DayOfWeek;
            MonthLayout.Add(new DayView(day), column, row);
            if(column == 6)
            {
                row++;
            }
        }
    }
   
}