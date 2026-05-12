using mobileapps_NASA_API.Models;
using mobileapps_NASA_API.Services;

namespace mobileapps_NASA_API;

public partial class UserPage : ContentPage
{

	private readonly UserService _userService;

	public UserPage(UserService userService)
	{
		InitializeComponent();

		_userService = userService;

		_userService.LoadUser();

		BindingContext = _userService.CurrentUser;

	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		_userService.LoadUser();

		BindingContext = null;
		BindingContext = _userService.CurrentUser;
	}
}