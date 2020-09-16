using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker.ARR.Zeta
{
    
    public class ZetaModel : ObservableObject
    {
        public string CurrentZeta{ get; set;}
        public BoolObject ramVisibility { get; set; }
        public BoolObject bullVisibility { get; set; }
        public BoolObject twinsVisibility { get; set; }
        public BoolObject crabVisibility { get; set; }
        public BoolObject lionVisibility { get; set; }
        public BoolObject maidenVisibility { get; set; }
        public BoolObject scalesVisibility { get; set; }
        public BoolObject scorpionVisibility { get; set; }
        public BoolObject archerVisibility { get; set; }
        public BoolObject goatVisibility { get; set; }
        public BoolObject water_bearerVisibility { get; set; }
        public BoolObject fishVisibility  { get; set;}
        public int CurrentLight{ get; set;}
        public string ZetaActivity{ get; set;}
        public ObservableCollection<string> AvailableZetaJobs { get; set; }

        public ZetaModel()
        {
            ZetaActivity = "No Sense";
            CurrentLight = 0;

            ramVisibility = new BoolObject();
            bullVisibility = new BoolObject();
            twinsVisibility = new BoolObject();
            crabVisibility = new BoolObject();
            lionVisibility = new BoolObject();
            maidenVisibility = new BoolObject();
            scalesVisibility = new BoolObject();
            scorpionVisibility = new BoolObject();
            archerVisibility = new BoolObject();
            goatVisibility = new BoolObject();
            water_bearerVisibility = new BoolObject();
            fishVisibility = new BoolObject();
        }
    }
}
