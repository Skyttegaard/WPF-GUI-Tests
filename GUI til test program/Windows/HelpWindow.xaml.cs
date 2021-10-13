using System.Windows;

namespace GUI_til_test_program.Windows
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        /// <summary>
        /// Small help window with a message.
        /// </summary>
        /// <param name="message"></param>
        public HelpWindow(string message)
        {

            InitializeComponent();
            Message.Text = message;
        }
    }
}
