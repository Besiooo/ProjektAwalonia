using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using System.Net.Http;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string apiKey = "c7d93130a8bb0fa9d8b4401cb61250a9"; 
            string city = "Warsaw"; 

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            var button = (Button)sender;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic weatherData = JsonConvert.DeserializeObject(responseBody);
                    string weatherDescription = weatherData.weather[0].description;
                    double temperature = weatherData.main.temp;                   
                    string weatherInfo = $"Pogoda w {city}: {weatherDescription}, temperatura: {temperature}°C";
                    
                    button.Content = weatherInfo;
                }
                catch (HttpRequestException ex)
                {
                    button.Content = $"B³¹d pobierania danych: {ex.Message}";
                }
            }
        }

    }
}