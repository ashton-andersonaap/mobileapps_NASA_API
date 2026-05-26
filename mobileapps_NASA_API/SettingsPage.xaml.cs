namespace mobileapps_NASA_API;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    private void ToggleTheme_Clicked(object sender, EventArgs e)
    {
        if (Application.Current == null)
            return;

        Application.Current.UserAppTheme =
            Application.Current.RequestedTheme == AppTheme.Dark
                ? AppTheme.Light
                : AppTheme.Dark;
    }
}