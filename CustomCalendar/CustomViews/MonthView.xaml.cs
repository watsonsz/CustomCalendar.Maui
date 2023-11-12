using CommunityToolkit.Mvvm.Input;
using CustomCalendar.BusinessEntity;
using CustomCalendar.DataAccess.Repositories;
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

    public MonthView(DateTime date)
    {
        InitializeComponent();
        MonthRepository _repo = new MonthRepository();
        var newMonthEntity = _repo.GetMonth(date.ToString("MMMM"), date.Year.ToString());

        var newMonth = new MonthViewViewModel(newMonthEntity.MonthID, date, newMonthEntity.MonthName);

        _viewModel = newMonth;
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
            MonthLayout.Add(new ViewDayView(day), column, row);
            if(column == 6)
            {
                row++;
            }
        }
    }

    
    public void SaveMonth(Object sender, EventArgs e)
    {
        MonthEntity monthEntity = new MonthEntity
        {
            Year = _viewModel.MonthDateTime.Year.ToString(),
            MonthName = _viewModel.CurrentMonth,
            MonthID = _viewModel.Id
        };
        MonthRepository _repo = new MonthRepository();
        _repo.SaveMonth(monthEntity);
        _viewModel.SaveDays();
    }
}