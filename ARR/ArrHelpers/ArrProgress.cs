using FFXIVRelicTracker.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    
    public class ArrProgress :ObservableObject
    {
        
        public ArrProgress()
        {
            Progress = States.NA;
        }
        public ArrProgress(string name)
            :this()
        {
            this.Name = name;
        }
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
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
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
