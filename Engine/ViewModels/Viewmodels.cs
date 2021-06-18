using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using Engine.Models;
using Engine.Factories;
namespace Engine.ViewModels
{
    public class Viewmodels : BaseNotificationClass
    {
        private string _startTid = "00:00";
        private string _slutTid = "00:00";
        private string _currentTime = "00:00";
        private string _title;
        private string _description;
        private string _scriptFail;
        private string _solution;
        private string _hints;
        private string _scriptFix;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
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
        public void SetStartTid()
        {
            StartTid = DateTime.Now.ToString("HH:mm");
        }
        public void SetSlutTid()
        {
            SlutTid = DateTime.Now.ToString("HH:mm");
        }
        public string JobForløb { get; set; }
        public string JobKategori { get; set; }
        private List<JobScripts> _jobs { get; set; }
        public IReadOnlyList<JobScripts> Jobs => _jobs.FindOpgaver(TextFileReader.FindJobForløb(JobForløb), TextFileReader.FindJobKategori(JobKategori));
        private List<string> _forløb { get; set; }
        public IReadOnlyList<string> Forløb => _forløb.AsReadOnly();
        private List<string> _kategori { get; set; }
        public IReadOnlyList<string> Kategori => _kategori.AsReadOnly();
        public Viewmodels() 
        {
            _kategori = TextFileReader.GetKategori();
            _forløb = TextFileReader.GetForløb();
            _jobs = TextFileReader.ReadJobScripts();
            
            
        }
        
        public void ChangeDescriptions(JobScripts jobScripts)
        {
            Description = jobScripts.Description;
            ScriptFail = jobScripts.ScriptFail;
            Solution = jobScripts.Solution;
            Hints = jobScripts.Hints;
            ScriptFix = jobScripts.ScriptFix;
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
        

    }
    
}

