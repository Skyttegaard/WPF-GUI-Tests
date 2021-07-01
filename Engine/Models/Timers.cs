using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class Timers : BaseNotificationClass
    {
        private string _currentTimer = "00:00";
        private string _startTimer = "00:00";
        private string _endTimer = "00:00";
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
    }
}
