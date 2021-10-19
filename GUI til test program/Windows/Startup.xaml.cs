using System.IO;
using System.Windows;
using System.Windows.Forms;
using Engine.StaticClasses;
using System.Threading;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        /// <summary>
        /// Creates or reads FilePath.txt.
        /// </summary>
        public Startup()
        {
            if (!Directory.Exists(".\\DATA"))
            {
                Directory.CreateDirectory(".\\DATA");
            }
            InitializeComponent();
            if (File.Exists(".\\DATA\\FilePath.txt"))
            {
                FilePathTextBox.Text = File.ReadAllText(".\\DATA\\FilePath.txt");
            }
            
            if (FilePathTextBox.Text != string.Empty)
            {
                IPClients icw = new();
                icw.Show();
                //MainWindow mw = new(icw._clientsList);
                //mw.Show();
                Close();
            }
        }
        /// <summary>
        /// Opens browser dialog on click and allows folder to be selected and saved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFilePath_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                FilePathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
        /// <summary>
        /// Closes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Saves filepath and opens program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if (FilePathTextBox.Text == string.Empty)
            {
                return;
            }
            File.WriteAllText(".\\DATA\\FilePath.txt", FilePathTextBox.Text);
            IPClients icw = new();
            icw.Show();
            TextFileReader.Initialize();
            Close();
        }
    }
}
