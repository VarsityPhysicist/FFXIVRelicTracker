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

        public int FireCount { get; internal set; }
        public int WindCount { get; internal set; }
        public int LightningCount { get; internal set; }
        public int IceCount { get; internal set; }
        public int EarthCount { get; internal set; }
        public int WaterCount { get; internal set; }
    }
}
