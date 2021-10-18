using Engine.StaticClasses;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Timers;
/// <summary>
/// Dette er viewmodel som bliver brugt som hoveddelen af koden til programmet. Her bliver der bindet til i MainWindow.xaml
/// Public strings bliver brugt til bindings samt OnPropertyChanged for at de bliver opdateret i view. private strings bliver brugt som backing variable.
/// ChangeDescriptions bliver brugt i MainWindow.xaml.cs til at ændre alle tekstbokse når man vælger en ny ting fra listen. Her bliver der brugt indekset af sæt tabbet man er på
/// StartTid & SlutTid sætter tidspunktet scriptet er startet og sluttet. Her bliver sæt tab indeks også brugt til at differentiere mellem hvad der skal ændres.
/// OnForløbChanged opdatere kategori listen for at den passer til det forløb man har valgt
/// IfKategoriNull tjekker om der er valgt en kategori. Hvis ikke bliver den sat til "Alle"
/// 2 ToolTips til knapperne til mouseover så man kan se hvad de gør
/// TextBoxes bliver brugt til at opdele strings til alle sættene så de ikke er det samme.
/// RunScript tager filepath til det script man gerne vil køre som er gemt i jobscriptet. Her bliver der lavet en processinfo så man kan køre det i powershell.
/// </summary>
namespace Engine.ViewModels
{
    public class Viewmodels : BaseNotificationClass
    {
        private readonly CultureInfo dk = new("da-DK");
        private int CurrentTimerSet;
        public Clients Client { get; set; }
        public bool CloseWindows { get; set; }
        public bool ErrorButtonVisibility { get; set; }
        private string _jobKategori = "Alle";
        private string _jobForløb = "GF";
        private readonly System.Timers.Timer timer = new();
        private List<JobScripts> _jobs { get; set; }
        private List<string> _forløb { get; set; }
        private List<string> _kategori { get; set; }
        private List<Clients> ClientList { get; set; }
        public IReadOnlyList<Clients> ClientNames => ClientList.AsReadOnly();
        public IReadOnlyList<JobScripts> Jobs => _jobs.FindOpgaver(_jobForløb, _jobKategori);
        public IReadOnlyList<string> Forløb => _forløb.AsReadOnly();
        public IReadOnlyList<string> Kategori => _kategori.AsReadOnly();
        public List<TextBoxesText> TextBoxes { get; set; } = new() { new(), new(), new(), new(), new() };
        public SelectedClient Selected { get; set; }
        public Stopwatch StopWatch = new();

        /// <summary>
        /// Restarts timer/ starts timer.
        /// </summary>
        /// <param name="tabIndex"></param>
        /// <param name="currentTimerSet"></param>
        public void SetStartTid(int tabIndex, int currentTimerSet)
        {
            
            
            TextBoxes[tabIndex].StartTimer = DateTime.Now.ToString("HH:mm", dk);
            StopWatch.Restart();
            StopWatch.Start();
            CurrentTimerSet = currentTimerSet;
            timer.Start();
        }
        /// <summary>
        /// Stops timer.
        /// </summary>
        /// <param name="tabIndex"></param>
        public void SetSlutTid(int tabIndex)
        {
            StopWatch.Stop();
            TextBoxes[tabIndex].EndTimer = DateTime.Now.ToString("HH:mm", dk);
        }
        /// <summary>
        /// Updates current time on timer per tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = StopWatch.Elapsed;
            TextBoxes[CurrentTimerSet].CurrentTimer = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        }
        /// <summary>
        /// Gets jobforløb string and calls OnPropertyChanged and OnForløbChanged method.
        /// </summary>
        public string JobForløb
        {
            get => _jobForløb;
            set
            {
                _jobForløb = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Jobs));
                OnForløbChanged();
            }
        }
        /// <summary>
        /// Gets JobKategori string and calls OnPropertyChanged.
        /// </summary>
        public string JobKategori
        {
            get => _jobKategori;
            set
            {
                _jobKategori = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Jobs));
            }
        }
        /// <summary>
        /// Constructor sets all needed values.
        /// </summary>
        /// <param name="client"></param>
        public Viewmodels(Clients client)
        {
            Client = client;
            _kategori = TextFileReader.GetKategori("GF");
            _forløb = TextFileReader.GetForløb().ToList();
            _jobs = TextFileReader.ReadJobScripts().ToList();
            Selected = SelectedClient.Instance;
            timer.Elapsed += new ElapsedEventHandler(Timer_Tick);
            timer.Interval = 500;
            timer.Enabled = true;
        }
        /// <summary>
        /// Sets Jobkategori
        /// </summary>
        private void OnForløbChanged()
        {
            _kategori = TextFileReader.GetKategori(JobForløb);
            OnPropertyChanged(nameof(Kategori));
            if (JobKategori == null)
            {
                JobKategori = _kategori.First();
            }
        }
        /// <summary>
        /// Changes text for textboxes.
        /// </summary>
        /// <param name="jobScripts"></param>
        /// <param name="tabIndex"></param>
        public void ChangeDescriptions(JobScripts jobScripts, int tabIndex)
        {
            TextBoxes[tabIndex].Description = jobScripts.Description;
            TextBoxes[tabIndex].ScriptFail = jobScripts.ScriptFailText;
            TextBoxes[tabIndex].Solution = jobScripts.Solution;
            TextBoxes[tabIndex].Hints = jobScripts.Hints;
            TextBoxes[tabIndex].ScriptFix = jobScripts.ScriptFixText;

        }
        /// <summary>
        /// Writes FilePath.txt if CloseWindows is true
        /// </summary>
        public void RestartProgram()
        {
            if (CloseWindows)
            {
                File.WriteAllText(".\\DATA\\FilePath.txt", string.Empty);
            }
        }
        /// <summary>
        /// String for tooltip hover.
        /// </summary>
        public string ButtonFixToolTip => "Fix script fejl med denne knap";
        /// <summary>
        /// String for tooltip hover.
        /// </summary>
        public string ButtonFailToolTip => "Start script med fejl med denne knap";
    }
}

