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

        public Visibility relicVisibility = Visibility.Hidden;
        public List<ArrProgress> JobRelics{ get; set;}
        public int RelicIndex{ get; set;}
        public string RelicDestination{ get; set;}
        public string RelicClassWeapon{ get; set;}
        public string RelicBeastmen1{ get; set;}
        public string RelicBeastmen2{ get; set;}
        public string RelicBeastmen3{ get; set;}
        public string RelicMap{ get; set;}
        public PointF RelicPoint{ get; set;}
        public string RelicMateria{ get; set;}
        public ObservableCollection<string> availableRelicJobs= new ObservableCollection<string>();

        public Visibility RelicVisibility { get { return relicVisibility; } set { relicVisibility = value; OnPropertyChanged(nameof(RelicVisibility)); } }

        private string currentRelic;
        public string CurrentRelic { get { return currentRelic; } set { currentRelic = value; } }

        public bool Stage1Complete { get; internal set; }
        public bool Stage2Complete { get; internal set; }
        public bool Stage3Complete { get; internal set; }
        public bool Stage4Complete { get; internal set; }
        public bool Stage5Complete { get; internal set; }
        public bool Stage6Complete { get; internal set; }
        public bool Stage7Complete { get; internal set; }
        public bool Stage8Complete { get; internal set; }
        public bool IfritComplete { get; internal set; }
        public bool GarudaComplete { get; internal set; }
        public bool ShivaComplete { get; internal set; }
        public bool BeastMan1Complete { get; internal set; }
        public bool BeastMan2Complete { get; internal set; }
        public bool BeastMan3Complete { get; internal set; }

    }
}
