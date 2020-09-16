using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._01_BaseTool
{
    public class BaseToolModel : ObservableObject
    {
        public BaseToolModel()
        {

        }
        public string SelectedJob { get;  set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
    }
}
