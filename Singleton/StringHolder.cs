using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    public class StringHolder : INotifyPropertyChanged
    {

        private string _description;
        private string _scriptFail;
        private string _solution;
        private string _hints;
        private string _scriptFix;
        private string _currentTimer = "00:00";
        private string _startTimer = "00:00";
        private string _endTimer = "00:00";

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        public string ScriptFail
        {
            get => _scriptFail;
            set
            {
                _scriptFail = value;
                OnPropertyChanged();
            }
        }
        public string Solution
        {
            get => _solution;
            set
            {
                _solution = value;
                OnPropertyChanged();
            }
        }
        public string Hints
        {
            get => _hints;
            set
            {
                _hints = value;
                OnPropertyChanged();
            }
        }
        public string ScriptFix
        {
            get => _scriptFix;
            set
            {
                _scriptFix = value;
                OnPropertyChanged();
            }
        }
        public string CurrentTimer
        {
            get => _currentTimer;
            set
            {
                _currentTimer = value;
                OnPropertyChanged();
            }
        }
        public string StartTimer
        {
            get => _startTimer;
            set
            {
                _startTimer = value;
                OnPropertyChanged();
            }
        }
        public string EndTimer
        {
            get => _endTimer;
            set
            {
                _endTimer = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
