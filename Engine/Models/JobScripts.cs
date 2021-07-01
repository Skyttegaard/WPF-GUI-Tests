using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// public strings som bliver brugt til bindings i xaml
/// Kategori & Forløb bliver brugt til at differentiere mellem deres kategorier & forløb så de bliver vist rigtigt i listen.
/// </summary>
namespace Engine.Models
{
    public class JobScripts : BaseNotificationClass
    {
        public string Description { get; }
        public string ScriptFailText { get; }
        public string Solution { get; }
        public string Hints { get; }
        public string ScriptFixText { get; }
        public string Title {get;}
        public string Kategori { get; }
        public string Fejl { get; set; }
        public string Løsning { get; }
        public string Forløb { get; }
        public JobScripts(string forløb, string kategori, string title, string description, string scriptFail, string solution, string hints, string scriptFix, string fejl, string løsning)
        {
            Forløb = forløb;
            Kategori = kategori;
            Title = title;
            Description = description;
            ScriptFailText = scriptFail;
            Solution = solution;
            Hints = hints;
            ScriptFixText = scriptFix;
            Fejl = fejl;
            Løsning = løsning;
        }
    }
}
