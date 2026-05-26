using mobileapps_NASA_API.Models;
using Microsoft.Maui.Storage;
using mobileapps_NASA_API.Services;

namespace mobileapps_NASA_API;

public partial class MainPage : ContentPage
{
    private readonly UserService _userService;

    public MainPage(UserService userService)
    {
        InitializeComponent();

        _userService = userService;

        userService.LoadUser();

        Recent_Button_Clicked(this, EventArgs.Empty);
    }

    private async void Recent_Button_Clicked(object sender, EventArgs e)
    {
        NASA_API_Service service = new();

        var items = await service.GetNASAData("");

        items = items
            .Where(item => item.data != null && item.data.Length > 0)
            .OrderByDescending(item => item.data?[0].date_created)
            .ToList();

        List<NASAItemViewModel> displayList = new();

        foreach (var item in items)
        {
            var title = item.data?[0]?.title;
            var img = item.links?[0]?.href;
            var description = item.data?[0]?.description;
            var date = item.data?[0]?.date_created;

            if (!string.IsNullOrEmpty(img) &&
                !string.IsNullOrEmpty(title))
            {
                displayList.Add(new NASAItemViewModel
                {
                    Id = item.data?[0]?.nasa_id,
                    Title = title,
                    ImageUrl = img,
                    Description = description ?? "No Description",
                    date = date ?? DateTime.MinValue
                });
            }
        }

        NASACollection.ItemsSource =
            displayList.OrderByDescending(x => x.date);
    }

    private async void Popular_button_Clicked(object sender, EventArgs e)
    {
        NASA_API_Service service = new();

        var items = await service.GetNASAData("");

        List<NASAItemViewModel> displayList = new();

        foreach (var item in items)
        {
            var title = item.data?[0]?.title;
            var img = item.links?[0]?.href;
            var description = item.data?[0]?.description;
            var date = item.data?[0]?.date_created;

            if (!string.IsNullOrEmpty(img) &&
                !string.IsNullOrEmpty(title))
            {
                displayList.Add(new NASAItemViewModel
                {
                    Id = item.data?[0]?.nasa_id,
                    Title = title,
                    ImageUrl = img,
                    Description = description ?? "No Description",
                    date = date ?? DateTime.MinValue
                });
            }
        }

        NASACollection.ItemsSource = displayList;
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;

        var item = button?.BindingContext as NASAItemViewModel;

        if (item == null)
            return;

        _userService.SaveItemToList(item, "Favourites");

        await DisplayAlert("Saved", "Added to favourites", "OK");
    }
}
