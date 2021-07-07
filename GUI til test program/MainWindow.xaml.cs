using Engine.Models;
using GUI_til_test_program.Windows;
using Singleton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private readonly Stopwatch stopWatch = new();
        private int SelectedComboBoxIndex;
        private TabControl SetTabs;
        private readonly List<JobScripts> jobScriptsList = new() { null, null, null, null, null };
        private int CurrentTimerSet;
        private ViewModelHolder viewModelHolder;
        private SelectedClient selectedClient;
        public MainWindow(List<Clients> clientList)
        {
            selectedClient = SelectedClient.Instance;
            viewModelHolder = ViewModelHolder.Instance;
            selectedClient.ClientList = clientList;
            selectedClient.Client = selectedClient.ClientList.First();
            LoadViewModels();
            ChooseViewModel();
            InitializeComponent();

        }
        private void LoadViewModels()
        {
            foreach (Clients client in selectedClient.ClientList)
            {
                viewModelHolder.AddViewModelToList(client);
            }
        }
        private void ChooseViewModel()
        {
            DataContext = viewModelHolder.ViewModels[SelectedComboBoxIndex];
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.IsDropDownOpen)
            {
                comboBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (e.AddedItems.Count > 0)
                    {
                        SelectedComboBoxIndex = comboBox.SelectedIndex;
                        ChooseViewModel();
                    }

                    comboBox.IsDropDownOpen = false;
                }));
            }

        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedItem is JobScripts job)
            {
                jobScriptsList[SetDataTabControl.SelectedIndex] = job;
                viewModelHolder.ViewModels[SelectedComboBoxIndex].ChangeDescriptions(job, SetTabs.SelectedIndex);
            }
        }
        private void LavFejl_Click(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            if (jobScriptsList[SetTabs.SelectedIndex] == null)
            {
                viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = false;
                ErrorWindow message = new("Vælg script", "Du har ikke valgt et script", viewModelHolder.ViewModels[SelectedComboBoxIndex]);
                message.Owner = GetWindow(this);
                message.ShowDialog();
            }
            else if (viewModelHolder.ViewModels[SelectedComboBoxIndex].StopWatch.IsRunning && CurrentTimerSet != SetTabs.SelectedIndex)
            {
                viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = false;
                ErrorWindow message = new("Error", "Stop timer på det script du er igang med først", viewModelHolder.ViewModels[SelectedComboBoxIndex]);
                message.Owner = GetWindow(this);
                message.ShowDialog();
            }
            else
            {
                CurrentTimerSet = SetTabs.SelectedIndex;
                SetTabs = SetDataTabControl;
                DelayButton(sender, e);
                RunScript("fail");
                viewModelHolder.ViewModels[SelectedComboBoxIndex].SetStartTid(SetTabs.SelectedIndex, CurrentTimerSet);
            }
        }
        private void RetFejl_Click(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            DelayButton(sender, e);
            RunScript("fix");
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
            if (viewModelHolder.ViewModels[SelectedComboBoxIndex].StopWatch.IsRunning)
            {
                stopWatch.Stop();
                viewModelHolder.ViewModels[SelectedComboBoxIndex].SetSlutTid(CurrentTimerSet);
                DelayButton(sender, e);
            }
        }
        private void TilfældigJob_OnClick(object sender, RoutedEventArgs e)
        {
            SetTabs = SetDataTabControl;
            Random random = new();
            jobScriptsList[SetTabs.SelectedIndex] = viewModelHolder.ViewModels[SelectedComboBoxIndex].Jobs[random.Next(0, viewModelHolder.ViewModels[SelectedComboBoxIndex].Jobs.Count)];
            viewModelHolder.ViewModels[SelectedComboBoxIndex].ChangeDescriptions(jobScriptsList[SetTabs.SelectedIndex], SetTabs.SelectedIndex);
        }


        private void ChangeFolderLocation_OnClick(object sender, RoutedEventArgs e)
        {
            viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = true;
            ErrorWindow message = new("Folder change", "Vil du vælge en ny mappe? (lukker programmet ned)", viewModelHolder.ViewModels[SelectedComboBoxIndex]);
            message.Owner = GetWindow(this);
            message.ShowDialog();
            if (message.ClickedYes)
            {
                viewModelHolder.ViewModels[SelectedComboBoxIndex].CloseWindows = true;
                viewModelHolder.ViewModels[SelectedComboBoxIndex].RestartProgram();
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
        private void RunScript(string FixOrFail)
        {
            SetTabs = SetDataTabControl;
            ProcessStartInfo startInfo = new();
            if (FixOrFail == "fix")
            {
                startInfo = new()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{jobScriptsList[SetTabs.SelectedIndex].Løsning}\"",
                    UseShellExecute = false
                };
                Process.Start(startInfo);
                return;
            }
            if (viewModelHolder.ViewModels[SelectedComboBoxIndex].StopWatch.IsRunning && SetTabs.SelectedIndex == CurrentTimerSet)
            {
                viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = true;
                ErrorWindow message = new("Restart Timer?", $"Er du sikker på at du vil genstarte timeren på sæt {SetTabs.SelectedIndex + 1} og starte et nyt script på denne pc?", viewModelHolder.ViewModels[SelectedComboBoxIndex]);
                message.Owner = GetWindow(this);
                message.ShowDialog();
                if (message.ClickedYes == false)
                {
                    viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = false;
                    return;
                }
                viewModelHolder.ViewModels[SelectedComboBoxIndex].ErrorButtonVisibility = false;
            }
            if (FixOrFail == "fail")
            {
                startInfo = new()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{jobScriptsList[SetTabs.SelectedIndex].Fejl}\"",
                    UseShellExecute = false
                };
            }
            Process.Start(startInfo);
        }
        //private void RunScript2(string fixOrFail)
        //{
        //    SetTabs = SetDataTabControl;
        //    UriBuilder builder = new(viewModelHolder.ViewModels[SelectedComboBoxIndex].Client.IPAddress);
        //    Uri uri = builder.Uri;
        //    WSManConnectionInfo connectionInfo = new(uri);
        //    connectionInfo.AuthenticationMechanism = AuthenticationMechanism.Negotiate;
        //    connectionInfo.EnableNetworkAccess = true;
        //    string script = jobScriptsList[SetTabs.SelectedIndex].Fejl;
        //    Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo);
        //    runspace.Open();

        //    using (PowerShell ps = PowerShell.Create())
        //    {
        //        ps.Runspace = runspace;
        //        ps.AddScript(script);
        //        ps.Invoke();
        //    }
        //    Pipeline pipeline = runspace.CreatePipeline();
        //    pipeline.Commands.AddScript(script);
        //    runspace.Close();
        //}


    }
}
