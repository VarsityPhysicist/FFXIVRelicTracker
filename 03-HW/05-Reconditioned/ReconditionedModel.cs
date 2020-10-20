using FFXIVRelicTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._03_HW._05_Reconditioned
{
    public class ReconditionedModel :BaseStageModel
    {
        public ReconditionedModel()
        {
        }

        public int CurrentPoints { get; set; }
        public int CurrentUmbrite { get; set; }
        public int CurrentSand { get; set; }
    }
}
