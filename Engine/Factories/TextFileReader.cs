using Engine.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// DATA_FOLDERNAME er startfolder. Der hvor forløbene skal ligge indenunder
/// ReadDirectoryFiles går igennem alle folders og gemmer navnene samt textfiler der ligger deri som så bliver gemt som JobScript i listen _jobScripts
/// GetKategori sortere efter alle kategorier som ligger under forløbet. Den retunere så en liste med de kategorier som skal bruges og sendes til viewmodel
/// GetForløb & ReadJobScripts retunere listerne _forløb & _jobScripts som indeholder navne på forløb & alle scripts med txt filer & scripts
/// FindOpgaver retunere en liste hvor den sammenligner valgte forløb & kategori fra combobox
/// </summary>
namespace Engine.Factories
{
    public static class TextFileReader
    {
        private static readonly string DATA_FOLDERNAME;
        private static List<JobScripts> _jobScripts = new();
        private static List<string> _forløb = new();
        private static List<string> _kategori = new();
        static TextFileReader()
        {
            DATA_FOLDERNAME = File.ReadAllText(".\\FilePath.txt");
            if (Directory.Exists(DATA_FOLDERNAME))
            {
                ReadDirectoryFiles();
            }
            else
            {
                throw new FileNotFoundException($"Missing directory: {DATA_FOLDERNAME}");
            }
        }
        public static List<string> GetKategori(string forløb)
        {
            _kategori.Clear();
            _kategori.Add("Alle");
            foreach (JobScripts scripts in _jobScripts)
            {
                if (scripts.Forløb == forløb)
                {
                    
                    if (!_kategori.Contains(scripts.Kategori))
                    {
                        _kategori.Add(scripts.Kategori);
                    }
                }
            }
            return _kategori;
        }
        public static List<string> GetForløb() => _forløb;

        public static List<JobScripts> ReadJobScripts() => _jobScripts;
        
        public static void ReadDirectoryFiles()
        {
            DirectoryInfo d = new(DATA_FOLDERNAME);
            string forløbFolder;
            string kategoriFolder;
            string opgaveNavn;
            string description = "description";
            string hints = "hints";
            string solution = "solution";
            string solutionScript = "solutionscript";
            string failScript = "failscript";
            string fejl = "error";
            string løsning = "error";
            foreach (DirectoryInfo di in d.GetDirectories())
            {
                forløbFolder = di.Name;
                foreach (DirectoryInfo dinfo in di.GetDirectories())
                {
                    kategoriFolder = dinfo.Name;
                    foreach (DirectoryInfo opgaver in dinfo.GetDirectories())
                    {
                        opgaveNavn = opgaver.Name;
                        foreach (FileInfo file in opgaver.GetFiles("Opgave-?.Beskrivelse.txt"))
                        {
                            description = File.ReadAllText(file.FullName);
                        }
                        foreach (FileInfo file in opgaver.GetFiles("Opgave-?.Fejl-Script.txt"))
                        {
                            failScript = File.ReadAllText(file.FullName);
                        }
                        foreach (FileInfo file in opgaver.GetFiles("Opgave-?.Hints.txt"))
                        {
                            hints = File.ReadAllText(file.FullName);
                        }
                        foreach (FileInfo file in opgaver.GetFiles("Opgave-?.Løsning.txt"))
                        {
                            solution = File.ReadAllText(file.FullName);
                        }
                        foreach (FileInfo file in opgaver.GetFiles("Opgave-?.Løsning-Script.txt"))
                        {
                            solutionScript = File.ReadAllText(file.FullName);
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Fejl.ps?"))
                        {
                            fejl = file.FullName;
                        }
                        foreach(FileInfo file in opgaver.GetFiles("Opgave-?.Løsning.ps?"))
                        {
                            løsning = file.FullName;
                        }
                        _jobScripts.Add(new JobScripts(forløbFolder, kategoriFolder, opgaveNavn, description, failScript, solution, hints, solutionScript, fejl, løsning));
                    }
                }
                if (!_forløb.Contains(forløbFolder))
                {
                    _forløb.Add(forløbFolder);
                }
            }
            _jobScripts = _jobScripts.OrderBy(o => o.Title).ToList();
        }
        public static List<JobScripts> FindOpgaver(this IEnumerable<JobScripts> scripts, string forløb, string kategori) => kategori == "Alle"
                ? scripts.Where(s => s.Forløb == forløb).ToList()
                : scripts.Where(s => s.Kategori == kategori && s.Forløb == forløb).ToList();


    }
}
