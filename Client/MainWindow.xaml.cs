using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FlightConnection;


namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var client = new TcpClient("127.0.0.1", 22222);
            var stream = client.GetStream();
            StreamReader sr = new StreamReader(stream);
            string test = sr.ReadToEnd();
            MessageBox.Show(test);
            Airport ob = JsonConvert.DeserializeObject<Airport>(test);
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            GmapFlight.MapProvider = GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            GmapFlight.SetPositionByKeywords("USA");
        }
    }
}
