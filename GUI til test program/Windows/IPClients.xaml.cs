using Engine.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for IPClients.xaml
    /// </summary>
    public partial class IPClients : Window
    {
        private const string DATA_FILE = ".\\DATA\\Clients.json";
        public IPClients()
        {
            _clientsList = LoadJsonClients();
            DataContext = this;
            InitializeComponent();
        }

        private List<Clients> _clientsList { get; set; }
        public IReadOnlyList<Clients> ClientsList => _clientsList.AsReadOnly();
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            _clientsList.Add(new Clients(PCName.Text, IPAdress.Text));
            IPAdressDataGrid.Items.Refresh();

        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            File.WriteAllText(DATA_FILE, JsonConvert.SerializeObject(_clientsList, Formatting.Indented));

            MainWindow mw = new(_clientsList);
            mw.Show();
            Close();
        }
        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            Clients client = IPAdressDataGrid.SelectedItem as Clients;
            _clientsList.Remove(client);
            IPAdressDataGrid.Items.Refresh();

        }
        private static List<Clients> LoadJsonClients()
        {
            if (!File.Exists(DATA_FILE))
            {
                return new();
            }
            else
            {
                List<Clients> clients = JsonConvert.DeserializeObject<List<Clients>>(File.ReadAllText(DATA_FILE));
                return !clients.Any() ? (new()) : clients;
            }

        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "Input PC Name" || textBox.Text == "Input IP Address")
            {
                textBox.Text = "";
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                if (textBox == IPAdress)
                {
                    textBox.Text = "Input IP Address";
                }
                if (textBox == PCName)
                {
                    textBox.Text = "Input PC Name";
                }
            }
        }
    }
}
