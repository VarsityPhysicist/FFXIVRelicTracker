using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._02_BasePlus1
{
    public class BasePlus1Model : ObservableObject
    {
        public string SelectedJob { get;  set; }
        public string ToolName { get;  set; }
        public string FirstMat { get;  set; }
        public string SecondMat { get;  set; }
        public string CraftedMat { get;  set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public int RemainingYellowScrips { get; set; }
        public string GatherLoc { get; internal set; }

        public BasePlus1Model()
        {

        }
    }
}
