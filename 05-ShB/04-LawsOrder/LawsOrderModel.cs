using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._04_LawsOrder
{
    public class LawsOrderModel : ObservableObject
    {
        public LawsOrderModel()
        {

        }

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string CurrentLawsOrder { get; set; }
        public int MemoryCount { get; set; }
    }
}
