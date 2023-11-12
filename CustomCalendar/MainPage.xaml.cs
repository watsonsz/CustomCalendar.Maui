using CommunityToolkit.Mvvm.Input;
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
            
        }

        [RelayCommand]
        async Task Click(string s)
        {
            await Shell.Current.GoToAsync(s);
        }
        


    }
}