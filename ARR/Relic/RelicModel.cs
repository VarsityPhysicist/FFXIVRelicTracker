using FFXIVRelicTracker.ARR.ArrHelpers;
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
        }

        internal Visibility relicVisibility = Visibility.Hidden;
        internal List<ArrProgress> JobRelics;
        internal int RelicIndex;
        internal string CurrentRelic;
        internal string RelicDestination;
        internal string RelicClassWeapon;
        internal string RelicBeastmen1;
        internal string RelicBeastmen2;
        internal string RelicBeastmen3;
        internal string RelicMap;
        internal PointF RelicPoint;
        internal string RelicMateria;
        internal ObservableCollection<string> availableRelicJobs= new ObservableCollection<string>();

        public Visibility RelicVisibility { get { return relicVisibility; } set { relicVisibility = value; OnPropertyChanged(nameof(RelicVisibility)); } }
        //public List<ArrProgress> JobRelics;
        //public int RelicIndex;
        //public string CurrentRelic;
        //public string RelicDestination;
        //public string RelicClassWeapon;
        //public string RelicBeastmen1;
        //public string RelicBeastmen2;
        //public string RelicBeastmen3;
        //public string RelicMap;
        //public PointF RelicPoint;
        //public string RelicMateria;
        //public ObservableCollection<string> availableRelicJobs = new ObservableCollection<string>();

    }
}
