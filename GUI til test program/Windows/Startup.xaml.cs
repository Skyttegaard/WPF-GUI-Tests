using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;


namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for Startup.xaml
    /// </summary>
    public partial class Startup : Window
    {
        
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
                MainWindow mw = new();
                mw.Show();
                Close();
            }
        }
        private void SetFilePath_OnClick(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
            {
                 FilePathTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            if(FilePathTextBox.Text == string.Empty)
            {
                return;
            }
            File.WriteAllText(".\\FilePath.txt", FilePathTextBox.Text);
            File.WriteAllText("C:\\Users\\Wowar\\Source\\Repos\\GUI-til-test-program6\\Engine\\FilePath.txt", FilePathTextBox.Text);
            MainWindow mw = new();
            mw.Show();
            Close();
        }
    }
}
