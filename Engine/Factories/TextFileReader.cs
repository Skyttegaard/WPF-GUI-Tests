using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Engine.Factories
{
    public static class TextFileReader
    {
        private const string TEXT_DATA_FILENAME = ".\\Tekstfiler\\TestText.txt";
        private static List<string> _textFiles = new List<string>();
        private const string datating = ".\\Tekstfiler\\";


        static TextFileReader()
        {
            TestTing();
            if (File.Exists(TEXT_DATA_FILENAME))
            {
                //_textFiles = File.ReadAllText(TEXT_DATA_FILENAME);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {TEXT_DATA_FILENAME}");
            }
        }
        public static string GetTextFile(int input)
        {
            return _textFiles[input];
        }

        public static void TestTing()
        {
            DirectoryInfo d = new DirectoryInfo(datating);
            foreach (var file in d.GetFiles("*.txt"))
            {
                _textFiles.Add(File.ReadAllText(file.FullName)); 
            }
        }
    }
}
