using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class JobScripts : BaseNotificationClass
    {
        public string Description { get; }
        public string ScriptFail { get; }
        public string Solution { get; }
        public string Hints { get; }
        public string ScriptFix { get; }
        public string Title {get;}
        public enum JobForløb
        {
            GF,
            H1,
            H2,
            Ekstra,
            Svendeprøve
        }
        public enum JobKategori
        {
            Alle,
            Basisnetværk,
            EIGRP,
            Klienter,
            OSPF,
            Server
        }
        
        public JobKategori Kategori { get ;  }
        public JobForløb Forløb { get; }

        public JobScripts(JobForløb forløb, JobKategori kategori, string title, string description, string scriptFail, string solution, string hints, string scriptFix)
        {
            Forløb = forløb;
            Kategori = kategori;
            Title = title;
            Description = description;
            ScriptFail = scriptFail;
            Solution = solution;
            Hints = hints;
            ScriptFix = scriptFix;

        }
    }
}
