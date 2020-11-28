using FFXIVRelicTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW._08_Lux
{
    public class LuxModel:BaseStageModel
    {
        public LuxModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
        public int Ink { get;  set; }
    }
}
