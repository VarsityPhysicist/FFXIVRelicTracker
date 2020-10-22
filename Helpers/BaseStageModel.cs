using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker.Helpers
{
    public abstract class BaseStageModel : ObservableObject
    {

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string SelectedJob { get; set; }
    }
}
