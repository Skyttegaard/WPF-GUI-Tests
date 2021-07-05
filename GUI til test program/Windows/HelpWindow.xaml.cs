using System.Windows;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow(string message)
        {

            InitializeComponent();
            Message.Text = message;
        }
    }
}
