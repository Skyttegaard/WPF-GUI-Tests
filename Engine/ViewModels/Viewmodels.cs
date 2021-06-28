using System;
using System.Collections.Generic;
using Engine.Models;
using Engine.Factories;
using System.Linq;
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
        private string _jobKategori = "Alle";
        private string _jobForløb = "GF";
        private List<JobScripts> _jobs { get; set; }
        private List<string> _forløb { get; set; }
        private List<string> _kategori { get; set; }

        public IReadOnlyList<JobScripts> Jobs => _jobs.FindOpgaver(_jobForløb, _jobKategori);
        public IReadOnlyList<string> Forløb => _forløb.AsReadOnly();
        public IReadOnlyList<string> Kategori => _kategori.AsReadOnly();
        public Timers Set1Timers { get; private set; }
        public Timers Set2Timers { get; private set; }
        public Timers Set3Timers { get; private set; }
        public Timers Set4Timers { get; private set; }
        public Timers Set5Timers { get; private set; }
        public TextBoxesText Set1Texts { get; private set; }
        public TextBoxesText Set2Texts { get; private set; }
        public TextBoxesText Set3Texts { get; private set; }
        public TextBoxesText Set4Texts { get; private set; }
        public TextBoxesText Set5Texts { get; private set; }
        public void InitializeObjects()
        {
            
            Set1Timers = new();
            Set2Timers = new();
            Set3Timers = new();
            Set4Timers = new();
            Set5Timers = new();
            Set1Texts = new();
            Set2Texts = new();
            Set3Texts = new();
            Set4Texts = new();
            Set5Texts = new();
        }
        public void SetStartTid(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    Set1Timers.StartTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 1:
                    Set2Timers.StartTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 2:
                    Set3Timers.StartTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 3:
                    Set4Timers.StartTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 4:
                    Set5Timers.StartTimer = DateTime.Now.ToString("HH:mm");
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public void SetSlutTid(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    Set1Timers.EndTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 1:
                    Set2Timers.EndTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 2:
                    Set3Timers.EndTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 3:
                    Set4Timers.EndTimer = DateTime.Now.ToString("HH:mm");
                    break;
                case 4:
                    Set5Timers.EndTimer = DateTime.Now.ToString("HH:mm");
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public Timers SetCurrentTimer(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    return Set1Timers;
                case 1:
                    return Set2Timers;
                case 2:
                    return Set3Timers;
                case 3:
                    return Set4Timers;
                case 4:
                    return Set5Timers;
                default:
                    throw new IndexOutOfRangeException();
                    
            }
        }
        
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
            InitializeObjects();
            _kategori = TextFileReader.GetKategori("GF");
            _forløb = TextFileReader.GetForløb();
            _jobs = TextFileReader.ReadJobScripts();
        }
        private void OnForløbChanged()
        {
            _kategori = TextFileReader.GetKategori(JobForløb);
            OnPropertyChanged(nameof(Kategori));
        }
        private TextBoxesText ChooseSetTextBoxes(int setIndex)
        {
            switch (setIndex)
            {
                case 0:
                    return Set1Texts;
                case 1:
                    return Set2Texts;
                case 2:
                    return Set3Texts;
                case 3:
                    return Set4Texts;
                case 4:
                    return Set5Texts;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
        public void ChangeDescriptions(JobScripts jobScripts, int setIndex)
        {
            TextBoxesText text = ChooseSetTextBoxes(setIndex);
            text.Description = jobScripts.Description;
            text.ScriptFail = jobScripts.ScriptFailText;
            text.Solution = jobScripts.Solution;
            text.Hints = jobScripts.Hints;
            text.ScriptFix = jobScripts.ScriptFixText;
        }
        
        public void RunScript(string script)
        {
            ProcessStartInfo startInfo = new()
            {

                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -file  \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
            
        }
        public string ButtonFixToolTip => "Fix script fejl med denne knap";
        public string ButtonFailToolTip => "Start script med fejl med denne knap";
    }
    
}

