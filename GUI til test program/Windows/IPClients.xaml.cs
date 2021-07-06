using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using Engine.Models;
using Engine.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            Viewmodels viewModel = new(_clientsList);
            MainWindow mw = new(viewModel);
            mw.Show();
            Close();
        }
        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            Clients client = IPAdressDataGrid.SelectedItem as Clients;
            _clientsList.Remove(client);
            IPAdressDataGrid.Items.Refresh();
            
        }
        private List<Clients> LoadJsonClients()
        {
            if (!File.Exists(DATA_FILE) || File.ReadAllText(DATA_FILE) != "[]")
            {
                return new();
            }
            else
            {

                try
                {
                    List<Clients> clients = JsonConvert.DeserializeObject<List<Clients>>(DATA_FILE);
                    return clients;
                }
                catch
                {
                    return new();
                }
            }

        }
    }
}
