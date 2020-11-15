using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    
    public class ArrProgress : BaseProgressClass
    {
        
        public ArrProgress()
        {
            Progress = States.NA;
        }
        public ArrProgress(string name, string Job)
            :this()
        {
            this.Name = name;
            this.Job = Job;
        }

        public ArrProgress(ArrProgress arrProgress, string name, string Job)
        {
            this.Name = name;
            this.Progress = arrProgress.Progress;
            this.Job = Job;
        }

        private string name;
        private string job;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Job
        {
            get { return job; }
            set
            {
                job = value;
                OnPropertyChanged(nameof(Job));
            }
        }
    }
}
