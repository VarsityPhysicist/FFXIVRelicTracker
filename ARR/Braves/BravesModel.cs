using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Braves
{
    public class BravesModel : ObservableObject
    {
        public BravesModel()
        {

        }
        internal string CurrentBraves;
        internal bool FirstQuest;
        internal bool FifthQuest;
        internal bool FourthQuest;
        internal bool ThirdQuest;
        internal bool SecondQuest;
        internal int RemainingGil;
        internal int RemainingSeals;
        internal int RemainingPoetics;
    }
}
