using Microsoft.Maui.Storage;
using mobileapps_NASA_API.Models;
using mobileapps_NASA_API.Services;

namespace mobileapps_NASA_API;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();

        string lastImageSearch =
            Preferences.Get("LastImageSearched", "Nebula");

        SearchInput.Text = lastImageSearch;
    }

    private async void SearchInput_SearchButtonPressed(
        object sender,
        EventArgs e)
    {
        string query = SearchInput.Text;

        if (string.IsNullOrWhiteSpace(query))
            return;

        NASA_API_Service service = new();

        var items = await service.GetNASAData(query);

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

        Preferences.Set("LastImageSearched", query);
    }
}