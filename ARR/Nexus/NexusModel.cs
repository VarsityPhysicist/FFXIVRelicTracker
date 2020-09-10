using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker.ARR.Nexus
{
    
    public class NexusModel
    {
        public string CurrentNexus{ get; set;}
        public int CurrentLight{ get; set;}
        public string NexusActivity{ get; set;}
        public ObservableCollection<string> AvailableNexusJobs { get; internal set; }

        public NexusModel()
        {

        }
    }
}
