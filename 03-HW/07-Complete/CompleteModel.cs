using FFXIVRelicTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW._07_Complete
{
    public class CompleteModel:BaseStageModel
    {
        public CompleteModel()
        {
        }

        public ObservableCollection<bool> DungeonBools { get;  set; }
        public int Light { get; set; }
        public int CurrentPneumite { get; set; }
    }
}
