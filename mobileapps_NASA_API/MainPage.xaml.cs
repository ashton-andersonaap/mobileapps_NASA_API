//using Android.Runtime;
using mobileapps_NASA_API.Models;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Maui.Storage;
using mobileapps_NASA_API.Services;
using System.Globalization;

namespace mobileapps_NASA_API
{
    public partial class MainPage : ContentPage
    {
        private UserService userService = new UserService();


        public MainPage()
        {
            InitializeComponent();
            string lastImageSearch = Preferences.Get("LastImageSearched", "Nebula");
            SearchInput.Text = lastImageSearch;

            

            userService.LoadUser();

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

                if (!string.IsNullOrEmpty(img) && !string.IsNullOrEmpty(title))
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

                Console.WriteLine($"{title} - {img}");
            }

            displayList = displayList
                .OrderByDescending(x => x.date)
                .ToList();

            NASACollection.ItemsSource = displayList;
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

                if (!string.IsNullOrEmpty(img) && !string.IsNullOrEmpty(title))
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

                Console.WriteLine($"{title} - {img}");
            }

            NASACollection.ItemsSource = displayList;
        }

        private async void SearchInput_SearchButtonPressed(object sender, EventArgs e)
        {
            string query = SearchInput.Text;

            if (string.IsNullOrWhiteSpace(query))
            {
                return;
            }



            NASA_API_Service service = new();
            var items = await service.GetNASAData(query);

            List<NASAItemViewModel> displayList = new();

            foreach (var item in items)
            {
                var title = item.data?[0]?.title;
                var img = item.links?[0]?.href;
                var description = item.data?[0]?.description;
                var date = item.data?[0]?.date_created;

                if (!string.IsNullOrEmpty(img) && !string.IsNullOrEmpty(title))
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

                Console.WriteLine($"{title} - {img}");
            }

            displayList = displayList
             .OrderByDescending(x => x.date)
             .ToList();


            NASACollection.ItemsSource = displayList;
            Preferences.Set("LastImageSearched", query);

        }

        private async void OnImageTapped(object sender, ItemTappedEventArgs e)
        {
            var frame = sender as Border;
            var item = frame?.BindingContext as NASAItemViewModel;

            if (item == null)
            {
                return;
            }


        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var item = button?.BindingContext as NASAItemViewModel;
            string listName = "Favourites";

            if (item == null)
            {
                return;
            }

            userService.SaveItemToList(item, listName);

            await DisplayAlert("Saved", $"Added to {listName}", "OK");

        }
    }
}
