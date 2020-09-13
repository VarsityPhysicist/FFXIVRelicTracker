using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.Helpers
{
    public class BaseProgressClass : ObservableObject
    {
        private States progress;


        public States Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }
        public enum States
        {
            NA,
            Initiated,
            Completed
        }
    }
}
