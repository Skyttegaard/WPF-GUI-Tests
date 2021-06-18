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
        private static List<string> _textFiles = new();
        private static List<string> _textFilesBeskrivelse = new();
        private static List<string> _textFilesHints = new();
        private static List<string> _textFilesLøsning = new();
        private static List<string> _textFilesLøsningScript = new();
        private static List<string> _textFilesFejlScript = new();
        private static List<JobScripts> _jobScripts = new();
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

  
        public static List<JobScripts> ReadJobScripts()
        {
            List<JobScripts> jobScripts = new();
            
            for(int i = 0; i < _textFilesBeskrivelse.Count; i++)
            {
                //Skal have flere lists til de andre textfiler.
                jobScripts.Add(new JobScripts(_textFiles[i], _textFilesBeskrivelse[i], _textFilesFejlScript[i], _textFilesLøsning[i], _textFilesHints[i], _textFilesLøsningScript[i]));

            }
            return jobScripts;
        }
        public static void ReadDirectoryFiles()
        {
            DirectoryInfo d = new DirectoryInfo(DATA_FOLDERNAME);
            string forløbFolder;
            string kategoriFolder;
            string opgaveNavn;
            string description;
            string hints;
            string solution;
            string solutionScript;
            string failScript;
            foreach(DirectoryInfo di in d.GetDirectories())
            {
                forløbFolder = di.Name;
                foreach(DirectoryInfo dinfo in di.GetDirectories())
                {
                    kategoriFolder = dinfo.Name;
                    foreach(DirectoryInfo opgaver in dinfo.GetDirectories())
                    {
                        foreach(FileInfo file in dinfo.GetFiles())
                        {
                            if(file.Name == "Opgave-?.Beskrivelse.txt")
                            {
                                description = File.ReadAllText(file.FullName);
                            }
                            if(file.Name == "Opgave-?.Fejl-Script.txt")
                            {
                                failScript = File.ReadAllText(file.FullName);
                            }
                            if(file.Name == "Opgave-?.Hints.txt")
                            {
                                hints = File.ReadAllText(file.FullName);
                            }
                            
                        }
                    }
                    //foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Beskrivelse.txt"))
                    //{
                    //    _textFiles.Add(file.Name.Substring(0,8));
                    //    if(file.Directory.Name == "Alle")
                    //    {
                    //        _textFilesBeskrivelse.Add(File.ReadAllText(file.FullName));   
                    //    }
                    //}
                    //foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Hints.txt"))
                    //{
                    //    if(file.Directory.Name == "Alle")
                    //    {
                    //        _textFilesHints.Add(File.ReadAllText(file.FullName));
                            
                    //    }
                    //}
                    //foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Løsning.txt"))
                    //{
                    //    if(file.Directory.Name == "Alle")
                    //    {
                    //        _textFilesLøsning.Add(File.ReadAllText(file.FullName));
                    //    }
                    //}
                    //foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Løsning-Script.txt"))
                    //{
                    //    if(file.Directory.Name == "Alle")
                    //    {
                    //        _textFilesLøsningScript.Add(File.ReadAllText(file.FullName));
                    //    }
                    //}
                    //foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Fejl-Script.txt"))
                    //{
                    //    if(file.Directory.Name == "Alle")
                    //    {
                    //        _textFilesFejlScript.Add(File.ReadAllText(file.FullName));
                    //    }
                    //}
                }
            }
            
        }
        public static List<string> Forløb()
        {
            DirectoryInfo d = new DirectoryInfo(DATA_FOLDERNAME);
            List<string> st = new();
            foreach(DirectoryInfo di in d.GetDirectories())
            {
                st.Add(di.Name);
            }
            return st;
        }

        public static List<JobScripts> FindOpgaver(this IEnumerable<JobScripts> scripts, JobScripts.JobForløb forløb, JobScripts.JobKategori kategori)
        {
            return scripts.Where(s => s.Kategori == kategori && s.Forløb == forløb).ToList();
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
                case "Basisnetværk":
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
