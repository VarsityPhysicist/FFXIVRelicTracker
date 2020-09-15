using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._02_BasePlus1
{
    public class BasePlus1Model : ObservableObject
    {
        public string SelectedJob { get; internal set; }
        public string ToolName { get; internal set; }
        public string FirstMat { get; internal set; }
        public string SecondMat { get; internal set; }
        public string CraftedMat { get; internal set; }
        public ObservableCollection<string> AvailableJobs { get; internal set; }

        public BasePlus1Model()
        {

        }
    }
}
