using CustomCalendar.CustomViews;

namespace CustomCalendar
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            DatePicker.Date = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day);
        }

        public DateTime DatePicked { get; set; }

        //This needs to go to SchedulePage.xaml.cs
        private void CreateCalendar(Object sender, EventArgs e)
        {
            MainPanel.Clear();
            var check = DatePicked;
            var checkTwo = DatePicked.Month;
            MainPanel.Add(new MonthView(checkTwo, DatePicked.Year));
        }


    }
}