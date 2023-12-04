using CommunityToolkit.Maui.Views;

namespace CustomCalendar.CustomViews;

public partial class ErrorView : Popup
{
	public ErrorView(string message)
	{
		InitializeComponent();
		BindingContext = this;
		Message = message;
		Acknowledged = false;
	}

	public bool Acknowledged;

	public string Message { get; set; }

    private void OK_Button_Clicked(object sender, EventArgs e)
    {
		this.Close();
    }

    private void Assign_Anyway(object sender, EventArgs e)
    {
		Acknowledged = true;
    }
}