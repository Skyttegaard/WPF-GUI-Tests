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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Timers;
using Engine.Models;
using Engine.ViewModels;

namespace GUI_til_test_program
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        Viewmodels ViewModel = new Viewmodels();

        

        public MainWindow()
        {
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0,  0,  0,  0,  1);
            DataContext = ViewModel;
            InitializeComponent();
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid dataGrid = sender as DataGrid;
            JobScripts jobScripts = dataGrid.SelectedItem as JobScripts;
            if (jobScripts != null)
            {
                ViewModel.ChangeDescriptions(jobScripts);
                
            }
        }
        private void LavFejl_Click(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
            dispatcherTimer.Start();
            DelayButton(sender,e);
            ViewModel.SetStartTid();
        }
        private void RetFejl_Click(object sender, RoutedEventArgs e)
        {
            DelayButton(sender,  e);
            ViewModel.SetSlutTid();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                ViewModel.CurrentTime = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
            }
        }
        private async void DelayButton(object sender, EventArgs e)
        {
            UIElement element = (UIElement)sender;

            element.IsEnabled = false;
            await Task.Delay(5000);
            element.IsEnabled = true;
        } 
        private void StopTid_OnClick(object sender, RoutedEventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                stopWatch.Reset();
                ViewModel.CurrentTime = "00:00";
                ViewModel.SetSlutTid();
            }
        }
    }
}
