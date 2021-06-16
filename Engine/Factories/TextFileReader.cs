using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Engine.Models;


namespace Engine.Factories
{
    public static class TextFileReader
    {
        private const string DATA_FOLDERNAME = ".\\Tekstfiler\\";
        private static List<string> _textFiles = new List<string>();
        private static List<string> _textFilesDescription = new List<string>();


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
            List<JobScripts> jobScripts = new List<JobScripts>();
            for(int i = 0; i < _textFiles.Count; i++)
            {
                //Skal have flere lists til de andre textfiler.
                jobScripts.Add(new JobScripts(_textFiles[i], _textFilesDescription[i], _textFiles[i], _textFiles[i], _textFiles[i], _textFiles[i]));

            }
            return jobScripts;
        }
        public static void ReadDirectoryFiles()
        {
            DirectoryInfo d = new DirectoryInfo(DATA_FOLDERNAME);
            foreach (FileInfo file in d.GetFiles("TestText*.txt"))
            {
                _textFiles.Add(File.ReadAllText(file.FullName)); 
            }
            foreach(FileInfo file in d.GetFiles("TestDescription?.txt"))
            {
                _textFilesDescription.Add(File.ReadAllText(file.FullName));
            }
        }
    }
}
