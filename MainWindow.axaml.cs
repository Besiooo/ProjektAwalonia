using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherApp
{
    public partial class MainWindow : Window
    {
        private const string API_KEY = "55addf5d7930c9dc259c27d61d951539";
        private const string CITY = "Rajcza";
        private TextBlock _weatherTextBlock;

        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
            _weatherTextBlock = this.FindControl<TextBlock>("WeatherTextBlock");
            GetWeatherData();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void GetWeatherData()
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={CITY}&appid={API_KEY}";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    _weatherTextBlock.Text = data;
                }
            }
        }
    }
}