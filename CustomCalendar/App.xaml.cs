using Microsoft.Maui.Controls.Handlers.Items;

namespace CustomCalendar
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            CollectionViewHandler.Mapper.AppendToMapping("Header", (handler, _) => _.AddLogicalChild(handler.PlatformView.Header as Element));
            MainPage = new AppShell();
        }
    }
}