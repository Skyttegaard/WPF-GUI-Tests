using Engine.Factories;
using Engine.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private readonly CultureInfo dk = new("da-dk");
        public bool CloseWindows { get; set; }
        public bool ErrorButtonVisibility { get; set; }
        private string _jobKategori = "Alle";
        private string _jobForløb = "GF";
        private List<JobScripts> _jobs { get; set; }
        private List<string> _forløb { get; set; }
        private List<string> _kategori { get; set; }

        public IReadOnlyList<JobScripts> Jobs => _jobs.FindOpgaver(_jobForløb, _jobKategori);
        public IReadOnlyList<string> Forløb => _forløb.AsReadOnly();
        public IReadOnlyList<string> Kategori => _kategori.AsReadOnly();
        public List<TextBoxesText> TextBoxes { get; private set; }
            = new() { new(), new(), new(), new(), new() };

        public void SetStartTid(int tabIndex)
        {
            TextBoxes[tabIndex].StartTimer = DateTime.Now.ToString("HH:mm", dk);
        }
        public void SetSlutTid(int tabIndex)
        {
            TextBoxes[tabIndex].EndTimer = DateTime.Now.ToString("HH:mm", dk);
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

        public Viewmodels()
        {
            TextFileReader.Initialize();
            _kategori = TextFileReader.GetKategori("GF");
            _forløb = TextFileReader.GetForløb();
            _jobs = TextFileReader.ReadJobScripts();
        }
        private void OnForløbChanged()
        {
            _kategori = TextFileReader.GetKategori(JobForløb);
            OnPropertyChanged(nameof(Kategori));
            if (JobKategori == null)
            {
                JobKategori = _kategori.First();
            }
        }

        public void ChangeDescriptions(JobScripts jobScripts, int tabIndex)
        {
            TextBoxes[tabIndex].Description = jobScripts.Description;
            TextBoxes[tabIndex].ScriptFail = jobScripts.ScriptFailText;
            TextBoxes[tabIndex].Solution = jobScripts.Solution;
            TextBoxes[tabIndex].Hints = jobScripts.Hints;
            TextBoxes[tabIndex].ScriptFix = jobScripts.ScriptFixText;
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
        public void RestartProgram()
        {
            if (CloseWindows)
            {
                File.WriteAllText(".\\FilePath.txt", string.Empty);
            }
        }
    }
}

