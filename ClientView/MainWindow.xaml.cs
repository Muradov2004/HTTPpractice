using ClientView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace ClientView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new();
        public ObservableCollection<User> users { get; set; } = new();


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void GetButton_Click(object sender, RoutedEventArgs e)
        {
            var msg = new HttpRequestMessage();
            msg.RequestUri = new Uri(@"http://localhost:27001/");
            msg.Method = HttpMethod.Get;
            var response = await client.SendAsync(msg);
            var text = await response.Content.ReadAsStringAsync();
            users.Clear();
            users = JsonSerializer.Deserialize<ObservableCollection<User>>(text)!;
        }


    }
}
