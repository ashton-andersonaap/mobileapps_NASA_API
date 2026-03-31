using System.Threading.Tasks;

namespace mobileapps_NASA_API
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Recent_Button_Clicked(object sender, EventArgs e)
        {

            NASA_API_Service service = new();
            Rootobject result = await service.GetNASAData("Nebula");

            var items = result.collection.items;

            foreach (var item in items)
            {
                var title = item.data?[0]?.title;
                var img = item.links?[0]?.href;

                Console.WriteLine(title, img);
            }
        }

        private void Popular_button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
