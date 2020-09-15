using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._01_BaseTool
{
    public class BaseToolModel : ObservableObject
    {
        public string SelectedJob { get; internal set; }
        public ObservableCollection<string> AvailableJobs { get; internal set; }
    }
}
