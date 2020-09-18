using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.Json;
using System.Configuration;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetWeather();
        }
        private void GetWeather()
        {
            using (var client = new WebClient())
            {
                try
                {
                    var jsonString = client.DownloadString($"{ConfigurationManager.AppSettings["BaseUrl"]}/weatherforecast");

                    var weatherForecast = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(jsonString);

                    GridView1.ItemsSource = weatherForecast;
                }
                catch (Exception)
                {

                }
            }
        }
    }

    public class WeatherForecast
    {
        public DateTime date { get; set; }
        public int temperatureC { get; set; }
        public int temperatureF { get; set; }
        public string summary { get; set; }
    }
}
