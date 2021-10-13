using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;


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
            if (!File.Exists(".\\FilePath.txt"))
            {
                File.Create(".\\FilePath.txt");
            }
            InitializeComponent();
            FilePathTextBox.Text = File.ReadAllText(".\\FilePath.txt");
            if (FilePathTextBox.Text != string.Empty)
            {
                IPClients icw = new();
                MainWindow mw = new(icw._clientsList);
                mw.Show();
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
            File.WriteAllText(".\\FilePath.txt", FilePathTextBox.Text);
            IPClients icw = new();
            icw.Show();
            Close();
        }
    }
}
