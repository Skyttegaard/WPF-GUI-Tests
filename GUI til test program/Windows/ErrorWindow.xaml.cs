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
using Engine.ViewModels;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public bool ClickedYes = false;
        public ErrorWindow(string title, string message, Viewmodels viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            Title = title;
            Message.Content = message;
        }
        private void Ja_OnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = true;
            Close();
        }
        private void Nej_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
