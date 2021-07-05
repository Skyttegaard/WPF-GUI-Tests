using Engine.ViewModels;
using System.Windows;

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
