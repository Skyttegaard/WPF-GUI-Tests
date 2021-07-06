using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
namespace Singleton
{
    public class SingletonTest
    {
        private readonly CultureInfo dk = new("da-DK");
        private List<StringHolder> textBoxes { get; set; } = new() { new(), new(), new(), new(), new() };
        private SingletonTest()
        {

        }
        private static readonly Lazy<SingletonTest> lazy = new(() => new SingletonTest());
        public static SingletonTest Instance => lazy.Value;
        public IReadOnlyList<StringHolder> TextBoxes => textBoxes.AsReadOnly();
        public void SetTextBoxes(int tabIndex, string desc, string scrfail, string sol, string hint, string scrfix)
        {
            textBoxes[tabIndex].Description = desc;
            textBoxes[tabIndex].ScriptFail = scrfail;
            textBoxes[tabIndex].Solution = sol;
            textBoxes[tabIndex].Hints = hint;
            textBoxes[tabIndex].ScriptFix = scrfix;
        }
        public void SetStartTid(int tabIndex)
        {
            textBoxes[tabIndex].StartTimer = DateTime.Now.ToString("HH:mm", dk);
        }
        public void SetSlutTid(int tabIndex)
        {
            textBoxes[tabIndex].EndTimer = DateTime.Now.ToString("HH:mm", dk);
        }
        
            
        
    }
}
