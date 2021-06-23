using System;
using System.Collections.Generic;
using Engine.Models;
using Engine.Factories;
using System.Diagnostics;
/// <summary>
/// public strings bliver brugt til bindings samt OnPropertyChanged for at de bliver opdateret i view. private strings bliver brugt som backing variable.
/// StartTid & SlutTid sætter tidspunktet scriptet er startet og slutted.
/// ChangeDescriptions bliver brugt i MainWindow.xaml.cs til at ændre alle tekstbokse når man vælger en ny ting fra listen.
/// OnForløbChanged opdatere kategori listen for at den passer til det forløb man har valgt.
/// IfKategoriNull tjekker om der er valgt en kategori. Hvis ikke bliver den sat til "Alle"
/// 2 ToolTips til knappende til mouseover så man kan se hvad de gør
/// </summary>
namespace Engine.ViewModels
{
    public class Viewmodels : BaseNotificationClass
    {
        private string _startTid = "00:00";
        private string _slutTid = "00:00";
        private string _currentTime = "00:00";
        private string _description;
        private string _scriptFail;
        private string _solution;
        private string _hints;
        private string _scriptFix;
        private string _jobForløb = "GF";
        private List<JobScripts> _jobs { get; set; }
        private List<string> _forløb { get; set; }
        private List<string> _kategori { get; set; }
        public IReadOnlyList<JobScripts> Jobs => _jobs.FindOpgaver(_jobForløb, _jobKategori);
        public IReadOnlyList<string> Forløb => _forløb.AsReadOnly();
        public IReadOnlyList<string> Kategori => _kategori.AsReadOnly();
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string ScriptFail
        {
            get => _scriptFail;
            set
            {
                _scriptFail = value;
                OnPropertyChanged();
            }
        }
        public string Solution
        {
            get => _solution;
            set
            {
                _solution = value;
                OnPropertyChanged();
            }
        }
        public string Hints
        {
            get => _hints;
            set
            {
                _hints = value;
                OnPropertyChanged();
            }
        }
        public string ScriptFix
        {
            get => _scriptFix;
            set
            {
                _scriptFix = value;
                OnPropertyChanged();
            }
        }
        public string StartTid
        {
            get => _startTid;
            set
            {
                _startTid = value;
                OnPropertyChanged();
            }
        }
        public string SlutTid
        {
            get => _slutTid;
            set
            {
                _slutTid = value;
                OnPropertyChanged();
            }
        }
        public void SetStartTid() => StartTid = DateTime.Now.ToString("HH:mm");
        public void SetSlutTid() => SlutTid = DateTime.Now.ToString("HH:mm");

        
        public string JobForløb
        {
            get => _jobForløb;
            set
            {
                _jobForløb = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Jobs));
                OnForløbChanged();
                IfKategoriNull();
            }
        }
        private string _jobKategori = "Alle";
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
        private void IfKategoriNull()
        {
            if(JobKategori == null)
            {
                JobKategori = "Alle";
            }
        }

        public Viewmodels() 
        {
            _kategori = TextFileReader.GetKategori("GF");
            _forløb = TextFileReader.GetForløb();
            _jobs = TextFileReader.ReadJobScripts();
            ButtonFixToolTip = "Fix script fejl med denne knap";
            ButtonFailToolTip = "Start script med fejl med denne knap";
        }
        private void OnForløbChanged()
        {
            _kategori = TextFileReader.GetKategori(JobForløb);
            OnPropertyChanged(nameof(Kategori));
        }
        
        public void ChangeDescriptions(JobScripts jobScripts)
        {
            Description = jobScripts.Description;
            ScriptFail = jobScripts.ScriptFailText;
            Solution = jobScripts.Solution;
            Hints = jobScripts.Hints;
            ScriptFix = jobScripts.ScriptFixText;
        }
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }
        
        public void RunScript(string script)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
            
        }
        public string ButtonFixToolTip { get; }
        public string ButtonFailToolTip { get; }
        
            



    }
    
}

