using Engine.ViewModels;
using System.Windows;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        /// <summary>
        /// Used to check if yes was clicked while window was open.
        /// </summary>
        public bool ClickedYes;
        /// <summary>
        /// Opens errorwindow with a message and title. Use ClickedYes bool to check if it was pressed.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="viewModel"></param>
        public ErrorWindow(string title, string message, Viewmodels viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            Title = title;
            Message.Content = message;
        }
        /// <summary>
        /// Sets ClickedYes to true and closes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ja_OnClick(object sender, RoutedEventArgs e)
        {
            ClickedYes = true;
            Close();
        }
        /// <summary>
        /// closes window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nej_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
