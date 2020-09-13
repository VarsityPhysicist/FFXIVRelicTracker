using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._01_Resistance
{
    public class ResistanceModel: ObservableObject
    {
        public ResistanceModel()
        {
        }

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string CurrentResistance { get; set; }
        public int CurrentScalepowder { get; internal set; }
    }
}
