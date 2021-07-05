using Engine.Models;
using Engine.ViewModels;
using GUI_til_test_program.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
/// <summary>
/// DispatcherTimer & StopWatch bliver brugt til at tjekke hvor lang tid man bruger på en opgave
/// DataGrid_SelectionChanged bliver kaldt hver gang man vælger et nyt element fra datagrid(job liste) Her ændre den descriptions og gemmer valgte element som jobScripts
/// DelayButton bliver brugt til at slå knapper fra i 5 sekunder når man trykker på dem for at man ikke kan spamme knapperne og starte for mange scripts på en gang
/// LavFejl_Click starter scriptet og timeren. 
/// RetFejl_Click retter scriptet. 
/// StopTid_OnClick stopper tiden. 
/// ChangeFolder åbner et nyt vindue som spørger om man vil ændre startfolder hvor alle tekstfiler og scripts er gemt i. Hvis man trykker ja starter den programmet forfra or sletter alt i FilePath.txt så man kan vælge en ny path.
/// </summary>
namespace GUI_til_test_program
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer dispatcherTimer = new();
        private readonly Stopwatch stopWatch = new();
        private readonly Viewmodels viewModel = new();
        private TabControl SetTabs;
        private readonly List<JobScripts> jobScriptsList = new() { null, null, null, null, null };
        private int CurrentTimerSet;

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
                jobScriptsList[SetDataTabControl.SelectedIndex] = job;
                viewModel.ChangeDescriptions(job, SetTabs.SelectedIndex);
            }
        }
        private void LavFejl_Click(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            if (stopWatch.IsRunning && CurrentTimerSet != SetTabs.SelectedIndex)
            {
                ErrorWindow message = new("Error", "Stop timer på det script du er igang med først", viewModel);
                message.Owner = GetWindow(this);
                message.ShowDialog();
                return;
            }
            CurrentTimerSet = SetTabs.SelectedIndex;
            if (jobScriptsList[SetTabs.SelectedIndex] == null)
            {
                viewModel.ErrorButtonVisibility = false;
                ErrorWindow message = new("Vælg script", "Du har ikke valgt et script", viewModel);
                message.Owner = GetWindow(this);
                message.ShowDialog();
            }
            else
            {
                stopWatch.Reset();
                SetTabs = SetDataTabControl;
                stopWatch.Start();
                dispatcherTimer.Start();
                DelayButton(sender, e);
                viewModel.SetStartTid(SetTabs.SelectedIndex);
                viewModel.RunScript(jobScriptsList[SetTabs.SelectedIndex].Fejl);
            }
        }
        private void RetFejl_Click(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DelayButton(sender, e);
            viewModel.RunScript(jobScriptsList[SetTabs.SelectedIndex].Løsning);
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            SetTabs = SetDataTabControl;
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                viewModel.TextBoxes[CurrentTimerSet].CurrentTimer = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
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
                viewModel.SetSlutTid(CurrentTimerSet);
                DelayButton(sender, e);
            }
        }
        private void TilfældigJob_OnClick(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            Random random = new();
            jobScriptsList[SetTabs.SelectedIndex] = viewModel.Jobs[random.Next(0, viewModel.Jobs.Count)];
            viewModel.ChangeDescriptions(jobScriptsList[SetTabs.SelectedIndex], SetTabs.SelectedIndex);


        }


        private void ChangeFolderLocation_OnClick(object sender, RoutedEventArgs e)
        {
            viewModel.ErrorButtonVisibility = true;
            ErrorWindow message = new("Folder change", "Vil du vælge en ny mappe? (lukker programmet ned)", viewModel);
            message.Owner = GetWindow(this);
            message.ShowDialog();
            if (message.ClickedYes)
            {
                viewModel.CloseWindows = true;
                viewModel.RestartProgram();
                Startup st = new();
                st.Show();
                Close();

            }
        }
        private void Help_OnClick(object sender, RoutedEventArgs e)
        {
            HelpWindow message = new("Hvis du ikke ser nogle opgaver kan du prøve at ændre folderpath. Path skal være ValgtFolder -> Forløb -> Kategori -> Opgave -> Filer.\nFor at ændre folderplacering, tryk 'File'");
            message.Owner = GetWindow(this);
            message.ShowDialog();
        }
    }
}
