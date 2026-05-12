using mobileapps_NASA_API.Services;

namespace mobileapps_NASA_API;

public partial class UserPage : ContentPage
{

	private UserService _userService;
	public UserPage(UserService userService)
	{
		InitializeComponent();

		_userService = userService;

		BindingContext = _userService.CurrentUser;

	}
}