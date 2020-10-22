using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW._01_Animated
{
    public class AnimatedModel: BaseStageModel
    {
        public AnimatedModel()
        {
        }

        public int FireCount { get; set; }
        public int WindCount { get; set; }
        public int LightningCount { get; set; }
        public int IceCount { get; set; }
        public int EarthCount { get; set; }
        public int WaterCount { get; set; }
    }
}
