using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._06_Blades
{
    public class BladesModel : ObservableObject
    {
        public BladesModel()
        {

        }

        public int Compact1 { get; set; }
        public int Compact2 { get; set; }

        public int Book1 { get; set; }
        public int Book2 { get; set; }

        public int Memory1 { get; set; }
        public int Memory2 { get; set; }


        public int EmotionCount { get; set; }
        public string CurrentBlade { get; set; }
        public ObservableCollection<string> AvailableJobs { get; set; }
    }
}
