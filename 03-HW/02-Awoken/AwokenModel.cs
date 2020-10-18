using FFXIVRelicTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW._02_Awoken
{
    public class AwokenModel : BaseStageModel
    {
        public AwokenModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
    }
}
