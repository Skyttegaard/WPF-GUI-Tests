using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using System.Windows;
namespace Engine.ViewModels
{
    public class Viewmodels : BaseNotificationClass
    {
        private string _currentTime = "00:00";
        private List<TestClass> _TESTSTRINGS;
        public IReadOnlyList<TestClass> TESTSTRINGS => _TESTSTRINGS.AsReadOnly();
        public Viewmodels()
        {
            _TESTSTRINGS = new List<TestClass>();
            TestVOIDTING();
        }
        private void TestVOIDTING()
        {
            _TESTSTRINGS.Add(new TestClass("TEST1"));
            _TESTSTRINGS.Add(new TestClass("TEST2"));
            _TESTSTRINGS.Add(new TestClass("TEST3"));
            _TESTSTRINGS.Add(new TestClass("TEST4"));
        }
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }
        public void ButtonTimeout()
        {
        }

    }
    public class TestClass
    {
        public string Name { get; set; }
        public TestClass(string name)
        {
            Name  = name;
        }
    }
}

