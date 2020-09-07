using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Zeta
{
    
    public class ZetaModel : ObservableObject
    {
        internal string CurrentZeta;
        internal BoolObject ramVisibility = new BoolObject();
        internal BoolObject bullVisibility = new BoolObject();
        internal BoolObject twinsVisibility = new BoolObject();
        internal BoolObject crabVisibility = new BoolObject();
        internal BoolObject lionVisibility = new BoolObject();
        internal BoolObject maidenVisibility = new BoolObject();
        internal BoolObject scalesVisibility = new BoolObject();
        internal BoolObject scorpionVisibility = new BoolObject();
        internal BoolObject archerVisibility = new BoolObject();
        internal BoolObject goatVisibility = new BoolObject();
        internal BoolObject water_bearerVisibility = new BoolObject();
        internal BoolObject fishVisibility = new BoolObject();
        internal int CurrentLight=0;
        internal string ZetaActivity="No Sense";

        public ZetaModel()
        {

        }
    }
}
