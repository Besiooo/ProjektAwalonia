using ReactiveUI;
using System.Reactive;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;
using System.Net.Http;
using AvaloniaApplication2.MyReferences;
using System;

namespace AvaloniaApplication2.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public string Greeting => "Welcome to Avalonia!";
        public ICommand DoTheThing { get; set; }
        public void Test() {
            try
            {
                var miejscowosc = References.MainWindow.FindControl<TextBox>("miejscowosc").Text;
                GetWeatherButton_Click(miejscowosc);
            } 
            catch (Exception ex)
            {
                throw;
            }

        }
        [ObservableProperty]
        private DateTime _currentDate = DateTime.Now;
        [ObservableProperty]
        private string _weatherInfo;
        [ObservableProperty]
        private double _temperature;
        [ObservableProperty]
        private double _cisnienie;
        [ObservableProperty]
        private double _wilgotnosc;
        [ObservableProperty]
        private string _dzis;
        public MainWindowViewModel()
        {
            DoTheThing = new RelayCommand(Test);
        }

        private async void GetWeatherButton_Click(string city)
        {
            string apiKey = "c7d93130a8bb0fa9d8b4401cb61250a9";
            

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    dynamic weatherData = JsonConvert.DeserializeObject(responseBody);
                    string weatherDescription = weatherData.weather[0].description;
                    Temperature = weatherData.main.temp;
                    Cisnienie = weatherData.main.pressure;
                    WeatherInfo = $"Pogoda w {city}: {weatherDescription}";
                    Wilgotnosc = weatherData.main.humidity;
                    Dzis = CurrentDate.ToString("dd MMMM yyyy");


                }
                catch (HttpRequestException ex)
                {

                }
            }
        }
    }
}