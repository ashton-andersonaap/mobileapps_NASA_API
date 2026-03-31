//using Android.Runtime;
using System.Threading.Tasks;

namespace mobileapps_NASA_API
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Recent_Button_Clicked(object sender, EventArgs e)
        {

            NASA_API_Service service = new();
            var items = await service.GetNASAData("Nebula");

            List<NASAItemViewModel> displayList = new();

            foreach (var item in items)
            {
                var title = item.data?[0]?.title;
                var img = item.links?[0]?.href;
                var description = item.data?[0]?.description;

                if (!string.IsNullOrEmpty(img) && !string.IsNullOrEmpty(title))
                {
                    displayList.Add(new NASAItemViewModel
                    {
                        Title = title,
                        ImageUrl = img,
                        Description = description ?? "No Description"
                    });
                }

                Console.WriteLine($"{title} - {img}");
            }

            NASACollection.ItemsSource = displayList;
        }

        private void Popular_button_Clicked(object sender, EventArgs e)
        {

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

                if (!string.IsNullOrEmpty(img) && !string.IsNullOrEmpty(title))
                {
                    displayList.Add(new NASAItemViewModel
                    {
                        Title = title,
                        ImageUrl = img,
                        Description = description ?? "No Description"
                    });
                }

                Console.WriteLine($"{title} - {img}");
            }

            NASACollection.ItemsSource = displayList;

        }
    }
}
