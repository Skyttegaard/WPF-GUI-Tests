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

            foreach(DirectoryInfo di in d.GetDirectories())
            {
                foreach(DirectoryInfo dinfo in di.GetDirectories())
                {
                    foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Beskrivelse.txt"))
                    {
                        _textFiles.Add(file.Name.Substring(0,8));
                        if(file.Directory.Name == "Alle")
                        {
                            _textFilesBeskrivelse.Add(File.ReadAllText(file.FullName));   
                        }
                    }
                    foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Hints.txt"))
                    {
                        if(file.Directory.Name == "Alle")
                        {
                            _textFilesHints.Add(File.ReadAllText(file.FullName));
                            
                        }
                    }
                    foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Løsning.txt"))
                    {
                        if(file.Directory.Name == "Alle")
                        {
                            _textFilesLøsning.Add(File.ReadAllText(file.FullName));
                        }
                    }
                    foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Løsning-Script.txt"))
                    {
                        if(file.Directory.Name == "Alle")
                        {
                            _textFilesLøsningScript.Add(File.ReadAllText(file.FullName));
                        }
                    }
                    foreach(FileInfo file in dinfo.GetFiles("Opgave-?.Fejl-Script.txt"))
                    {
                        if(file.Directory.Name == "Alle")
                        {
                            _textFilesFejlScript.Add(File.ReadAllText(file.FullName));
                        }
                    }
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
    }
}
