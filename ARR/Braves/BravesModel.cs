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
        public string CurrentBraves{ get; set;}
        public bool FirstQuest{ get; set;}
        public bool FifthQuest{ get; set;}
        public bool FourthQuest{ get; set;}
        public bool ThirdQuest{ get; set;}
        public bool SecondQuest{ get; set;}
        public int RemainingGil{ get; set;}
        public int RemainingSeals{ get; set;}
        public int RemainingPoetics{ get; set;}
    }
}
