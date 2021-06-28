using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using GUI_til_test_program.Windows;
using Engine.Models;
using Engine.ViewModels;
/// <summary>
/// DispatcherTimer & StopWatch bliver brugt til at tjekke hvor lang tid man bruger på en opgave
/// DataGrid_SelectionChanged bliver kaldt hver gang man vælger et nyt element fra datagrid(job liste) Her ændre den descriptions og gemmer valgte element som jobScripts
/// DelayButton bliver brugt til at slå knapper fra i 5 sekunder når man trykker på dem for at man ikke kan spamme knapperne og starte for mange scripts på en gang
/// LavFejl_Click starter scriptet og timeren. 
/// RetFejl_Click retter scriptet. 
/// StopTid_OnClick stopper tiden. 
/// </summary>
namespace GUI_til_test_program
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer dispatcherTimer = new();
        private readonly Stopwatch stopWatch = new();
        private readonly Viewmodels viewModel = new();
        private JobScripts jobScripts;
        private TabControl SetTabs;
        private Timers timers;
        public MainWindow()
        {
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            DataContext = viewModel;
            InitializeComponent();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            JobScripts job = dataGrid.SelectedItem as JobScripts;
            if (job != null)
            {
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
                jobScripts = job;
            }
        }
        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            JobScripts job = dataGrid.SelectedItem as JobScripts;
            if (job != null)
            {
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
                jobScripts = job;
            }
        }
        private void DataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            JobScripts job = dataGrid.SelectedItem as JobScripts;
            if (job != null)
            {
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
                jobScripts = job;
            }
        }
        private void DataGrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            JobScripts job = dataGrid.SelectedItem as JobScripts;
            if (job != null)
            {
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
                jobScripts = job;
            }
        }
        private void DataGrid5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            JobScripts job = dataGrid.SelectedItem as JobScripts;
            if (job != null)
            {
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
                jobScripts = job;
            }
        }
        private void LavFejl_Click(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            if(jobScripts == null)
            {
                ErrorWindow message = new("Vælg script", "Du har ikke valgt et script");
                message.Owner = GetWindow(this);
                message.ShowDialog();
            }
            else
            {
                stopWatch.Reset();
                SetTabs = SetDataTabControl;
                timers = viewModel.SetCurrentTimer(SetTabs.SelectedIndex);
                stopWatch.Start();
                dispatcherTimer.Start();
                DelayButton(sender, e);
                viewModel.SetStartTid(SetTabs.SelectedIndex);
                viewModel.RunScript(jobScripts.Fejl);
            }
        }
        private void RetFejl_Click(object sender, RoutedEventArgs e)
        {
            DelayButton(sender, e);
            viewModel.RunScript(jobScripts.Løsning);
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                timers.CurrentTimer = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
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
            SetTabs = SetDataTabControl;
            if (stopWatch.IsRunning)
            {
                stopWatch.Stop();
                viewModel.SetSlutTid(SetTabs.SelectedIndex);
                DelayButton(sender, e);
            }
        }
        private void TilfældigJob_OnClick(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            Random random = new();
            jobScripts = viewModel.Jobs[random.Next(0, viewModel.Jobs.Count)];
            viewModel.ChangeDescriptions(jobScripts, SetTabs.SelectedIndex);

            
        }
        private void Test_OnClick(object sender, RoutedEventArgs e)
        {
            TabItem ti = SetDataTabControl.SelectedItem as TabItem;
            TabControl tab = SetDataTabControl;
            
            ErrorWindow message = new ErrorWindow("test", $"Selected tab is {tab.SelectedIndex}");
            
            message.Owner = GetWindow(this);
            message.ShowDialog();
        }
    }
}
