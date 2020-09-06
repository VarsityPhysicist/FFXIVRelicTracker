using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;

namespace FFXIVRelicTracker.ARR.Relic
{
    public class RelicModel : ObservableObject
    {

        public RelicModel()
        {
            //jobRelics = new List<ArrProgress>();
            //jobRelicStates = new List<ArrProgress.States>();
        }

        internal Visibility RelicVisibility = Visibility.Hidden;

        internal List<ArrProgress> jobRelics;
        internal int relicIndex;
        internal string currentRelic;
        internal string RelicDestination;
        internal string RelicClassWeapon;
        internal string RelicBeastmen1;
        internal string RelicBeastmen2;
        internal string RelicBeastmen3;
        internal string RelicMap;
        internal PointF RelicPoint;
        internal string RelicMateria;
        internal ObservableCollection<string> availableRelicJobs= new ObservableCollection<string>();

    }
}
