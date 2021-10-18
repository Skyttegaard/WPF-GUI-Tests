using Engine.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for IPClients.xaml
    /// </summary>
    public partial class IPClients : Window
    {
        private const string DATA_FILE = ".\\DATA\\Clients.json";
        /// <summary>
        /// Sets clientlist.
        /// </summary>
        public IPClients()
        {
            _clientsList = LoadJsonClients();
            DataContext = this;
            InitializeComponent();
            //if (_clientsList.Any())
            //{
            //    Thread.Sleep(2000);
            //    Finished();

            //}
        }

        private List<Clients> _clientsList { get; set; }
        public IReadOnlyList<Clients> ClientsList => _clientsList.AsReadOnly();
        /// <summary>
        /// Adds a new client to list on click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            _clientsList.Add(new Clients(PCName.Text, IPAdress.Text));
            IPAdressDataGrid.Items.Refresh();

        }
        /// <summary>
        /// Closes window and saves clients to json file.
        /// </summary>
        private void Finished()
        {
            File.WriteAllText(DATA_FILE, JsonConvert.SerializeObject(_clientsList, Formatting.Indented));

            MainWindow mw = new(_clientsList);
            mw.Show();
            Close();
        }
        /// <summary>
        /// Closes window and calls Finished method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Finished();
        }
        /// <summary>
        /// Removes client on click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            Clients client = IPAdressDataGrid.SelectedItem as Clients;
            _clientsList.Remove(client);
            IPAdressDataGrid.Items.Refresh();

        }
        /// <summary>
        /// Loads clients from json if it exists.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Removes start text if textbox is focused.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.Foreground = Brushes.Black;
            if (textBox.Text is "Input PC Name" or "Input IP Address")
            {
                textBox.Text = "";
            }
        }
        /// <summary>
        /// If textbox is out of focus and text is empty then fill again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text == "")
            {
                textBox.Foreground = Brushes.Gray;
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
