using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Engine.Models;
using System.Collections.ObjectModel;


namespace Engine.Factories
{
    public static class TextFileReader
    {
        private const string DATA_FOLDERNAME = ".\\Forløb\\";
        private static List<JobScripts> _jobScripts = new();
        private static List<string> _forløb = new();
        private static List<string> _kategori = new();
        static TextFileReader()
        {
            
            if (Directory.Exists(DATA_FOLDERNAME))
            {
                ReadDirectoryFiles();
            }
            else
            {
                throw new FileNotFoundException($"Missing directory: {DATA_FOLDERNAME}");
            }
        }
        public static List<string> GetKategori()
        {
            return _kategori;
        }
        public static List<string> GetForløb()
        {
            return _forløb;
        }
  
        public static List<JobScripts> ReadJobScripts()
        {
            
            return _jobScripts;
        }
        public static void ReadDirectoryFiles()
        {
            DirectoryInfo d = new DirectoryInfo(DATA_FOLDERNAME);
            string forløbFolder;
            string kategoriFolder;
            string opgaveNavn;
            string description = "description";
            string hints = "hints";
            string solution = "solution";
            string solutionScript = "solutionscript";
            string failScript = "failscript";
            _kategori.Add("Alle");
            foreach(DirectoryInfo di in d.GetDirectories())
            {
                forløbFolder = di.Name;
                foreach(DirectoryInfo dinfo in di.GetDirectories())
                {
                    kategoriFolder = dinfo.Name;
                    foreach(DirectoryInfo opgaver in dinfo.GetDirectories())
                    {
                        opgaveNavn = opgaver.Name;
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Beskrivelse.txt"))
                        {
                             description = File.ReadAllText(file.FullName);
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Fejl-Script.txt"))
                        {
                            failScript = File.ReadAllText(file.FullName);
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Hints.txt"))
                        {
                            hints = File.ReadAllText(file.FullName);
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Løsning.txt"))
                        {
                            solution = File.ReadAllText(file.FullName);
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Løsning-Script.txt"))
                        {
                            solutionScript = File.ReadAllText(file.FullName);
                        }
                        _jobScripts.Add(new JobScripts(FindJobForløb(forløbFolder), FindJobKategori(kategoriFolder), opgaveNavn, description, failScript, solution, hints, solutionScript));
                    }
                    if (!_kategori.Contains(kategoriFolder))
                    {
                        _kategori.Add(kategoriFolder);
                    }
                }
                if (!_forløb.Contains(forløbFolder))
                {
                    _forløb.Add(forløbFolder);
                }
            }
            
        }
        

        public static List<JobScripts> FindOpgaver(this IEnumerable<JobScripts> scripts, JobScripts.JobForløb forløb, JobScripts.JobKategori kategori)
        {
            if (kategori == JobScripts.JobKategori.Alle)
            {
                return FindOpgaver(scripts, forløb);
            }
            else
            {
                return scripts.Where(s => s.Kategori == kategori && s.Forløb == forløb).ToList();
            }
        }
        public static List<JobScripts> FindOpgaver(this IEnumerable<JobScripts> scripts, JobScripts.JobForløb forløb)
        {
            return scripts.Where(s => s.Forløb == forløb).ToList();
        }
        public static JobScripts.JobForløb FindJobForløb(string forløb)
        {
            switch (forløb)
            {
                case "GF":
                    return JobScripts.JobForløb.GF;
                case "H1":
                    return JobScripts.JobForløb.H1;
                case "H2":
                    return JobScripts.JobForløb.H2;
                case "Ekstra":
                    return JobScripts.JobForløb.Ekstra;
                case "Svendeprøve":
                    return JobScripts.JobForløb.Svendeprøve;
                default:
                    return JobScripts.JobForløb.GF;
            }
        }
        public static JobScripts.JobKategori FindJobKategori(string kategori)
        {
            switch (kategori)
            {
                case "Alle":
                    return JobScripts.JobKategori.Alle;
                case "Basis netværk":
                    return JobScripts.JobKategori.Basisnetværk;
                case "EIGRP":
                    return JobScripts.JobKategori.EIGRP;
                case "Klienter":
                    return JobScripts.JobKategori.Klienter;
                case "OSPF":
                    return JobScripts.JobKategori.OSPF;
                case "Server":
                    return JobScripts.JobKategori.Server;
                default:
                    return JobScripts.JobKategori.Alle;
            }
        }
    }
}
