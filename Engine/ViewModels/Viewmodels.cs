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

        public Viewmodels()
        {
            
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
}
