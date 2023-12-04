using CommunityToolkit.Maui.Views;
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
        //TODO Check Exists then update

        _repo.SaveMonth(monthEntity);
        _viewModel.SaveDays();
    }

    private void AssignEmployee(object sender, EventArgs e)
    {
        var label = this.FindByName<Label>("ErrorLabel");
        var button = this.FindByName<Button>("Error Button");
        
        SideBar.Children.Remove(label);
        SideBar.Children.Remove(button);

        if (_viewModel.AddEmployee(false))
        {
            var errorLabel = new Label()
            {
                Text = _viewModel.ErrorMessage,
                AutomationId = "ErrorLabel"
            };
            SideBar.Children.Add(errorLabel);

            var Button = new Button()
            {
                Text = "Assign Anyway",
                Command = AddAnywayCommand,
            };
            Button.AutomationId = "ErrorButton";
            SideBar.Children.Add(Button);
            
        }
            
    }

    [RelayCommand]
    void AddAnyway()
    {
        var label = this.FindByName<Label>("ErrorLabel");
        var button = this.FindByName("ErrorButton");
        SideBar.Children.Remove(label);
        SideBar.Children.Remove(button);

        _viewModel.AddEmployee(true);
    }
}