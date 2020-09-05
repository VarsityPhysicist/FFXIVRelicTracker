using FFXIVRelicTracker.ARR.Animus;
using FFXIVRelicTracker.ARR.Atma;
using FFXIVRelicTracker.ARR.Braves;
using FFXIVRelicTracker.ARR.Nexus;
using FFXIVRelicTracker.ARR.Novus;
using FFXIVRelicTracker.ARR.Relic;
using FFXIVRelicTracker.ARR.Zeta;
using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;

namespace FFXIVRelicTracker.Models
{
    public class ArrModel:ObservableObject
    {
        private ArrWeapon arr;
        private RelicModel relicModel;
        private AtmaModel atmaModel;
        private AnimusModel animusModel;
        private NovusModel novusModel;
        private NexusModel nexusModel;
        private BravesModel bravesModel;
        private ZetaModel zetaModel;

        public ArrModel()
        {
            Arr = new ArrWeapon();
            RelicModel = new RelicModel();
            AtmaModel = new AtmaModel();
            AnimusModel = new AnimusModel();
            NovusModel = new NovusModel();
            NexusModel = new NexusModel();
            BravesModel = new BravesModel();
            ZetaModel = new ZetaModel();
        }
        public ArrWeapon Arr
        {
            get { return arr; }
            set
            {
                if(value!=null)
                {
                    arr = value;
                    OnPropertyChanged(nameof(Arr));
                }
            }
        }

        #region Stage Models
        public RelicModel RelicModel
        {
            get { return relicModel; }
            set
            {
                if (value != null)
                {
                    relicModel = value;
                    OnPropertyChanged(nameof(RelicModel));
                }
            }
        }

        public AtmaModel AtmaModel
        {
            get { return atmaModel; }
            set
            {
                if (value != null)
                {
                    atmaModel = value;
                    OnPropertyChanged(nameof(AtmaModel));
                }
            }
        }

        public AnimusModel AnimusModel
        {
            get { return animusModel; }
            set
            {
                if (value != null)
                {
                    animusModel = value;
                    OnPropertyChanged(nameof(AnimusModel));
                }
            }
        }

        public NovusModel NovusModel
        {
            get { return novusModel; }
            set
            {
                if (value != null)
                {
                    novusModel = value;
                    OnPropertyChanged(nameof(NovusModel));
                }
            }
        }

        public NexusModel NexusModel
        {
            get { return nexusModel; }
            set
            {
                if (value != null)
                {
                    nexusModel = value;
                    OnPropertyChanged(nameof(NexusModel));
                }
            }
        }
        public BravesModel BravesModel
        {
            get { return bravesModel; }
            set
            {
                if (value != null)
                {
                    bravesModel = value;
                    OnPropertyChanged(nameof(BravesModel));
                }
            }
        }
        public ZetaModel ZetaModel
        {
            get { return zetaModel; }
            set
            {
                if (value != null)
                {
                    zetaModel = value;
                    OnPropertyChanged(nameof(ZetaModel));
                }
            }
        }

        #endregion
        //#region RelicInfo
        //#region RelicProperties
        //public string RelicSelection;

        //private string relicMap;
        //public string RelicMap
        //{
        //    get { return relicMap; }
        //    set
        //    {
        //        relicMap = value;
        //        NotifyPropertyChanged("RelicMap");
        //    }
        //}

        //private string relicDestination;
        //public string RelicDestination
        //{
        //    get { return relicDestination; }
        //    set
        //    {
        //        relicDestination = value;
        //        NotifyPropertyChanged("RelicDestination");
        //    }
        //}

        //private PointF relicPoint;
        //public PointF RelicPoint
        //{
        //    get { return relicPoint; }
        //    set
        //    {
        //        relicPoint = value;
        //        RelicX = value.X;
        //        RelicY = value.Y;
        //        NotifyPropertyChanged("RelicPoint");
        //    }
        //}

        //private float relicX;
        //private float relicY;

        //public float RelicX
        //{
        //    get { return relicX; }
        //    set
        //    {
        //        relicX = relicPoint.X;
        //        NotifyPropertyChanged("RelicX");
        //    }
        //}

        //public float RelicY
        //{
        //    get { return relicY; }
        //    set
        //    {
        //        relicY = relicPoint.Y;
        //        NotifyPropertyChanged("RelicY");
        //    }
        //}

        //private Visibility relicVisibility = Visibility.Hidden;
        //public Visibility RelicVisibility
        //{
        //    get { return relicVisibility; }
        //    set
        //    {
        //        relicVisibility = value;
        //        NotifyPropertyChanged("RelicVisibility");
        //    }
        //}

        //private string relicMateria;
        //private string relicClassWeapon;

        //public string RelicMateria
        //{
        //    get { return relicMateria; }
        //    set { relicMateria = value; NotifyPropertyChanged("RelicMateria"); }
        //}
        //public string RelicClassWeapon
        //{
        //    get { return relicClassWeapon; }
        //    set { relicClassWeapon = value; NotifyPropertyChanged("RelicClassWeapon"); }
        //}

        //private string relicBeastmen1;
        //private string relicBeastmen2;
        //private string relicBeastmen3;

        //public string RelicBeastmen1
        //{
        //    get { return relicBeastmen1; }
        //    set { relicBeastmen1 = value; NotifyPropertyChanged("RelicBeastmen1"); }
        //}
        //public string RelicBeastmen2
        //{
        //    get { return relicBeastmen2; }
        //    set { relicBeastmen2 = value; NotifyPropertyChanged("RelicBeastmen2"); }
        //}
        //public string RelicBeastmen3
        //{
        //    get { return relicBeastmen3; }
        //    set { relicBeastmen3 = value; NotifyPropertyChanged("RelicBeastmen3"); }
        //}


        //public ObservableCollection<string> availableRelicJobs = new ObservableCollection<string>
        //{
        //    "PLD",
        //    "WAR",
        //    "WHM",
        //    "SCH",
        //    "MNK",
        //    "DRG",
        //    "NIN",
        //    "BRD",
        //    "BLM",
        //    "SMN"
        //};

        //#endregion

        //private string activeRelic;
        //public string ActiveRelic
        //{
        //    get { return activeRelic; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            activeRelic = value;
        //            RelicMap = ArrInfo.ReturnRelicMap(ActiveRelic);
        //            RelicDestination = ArrInfo.ReturnRelicDestination(ActiveRelic);
        //            RelicPoint = ArrInfo.ReturnRelicPoint(ActiveRelic);
        //            RelicMateria = ArrInfo.ReturnMateria(ActiveRelic);
        //            RelicClassWeapon = ArrInfo.ReturnClassWeapon(ActiveRelic);

        //            List<string> templist = ArrInfo.ReturnBeastmen(ActiveRelic);

        //            RelicBeastmen1 = templist[0];
        //            RelicBeastmen2 = templist[1];
        //            RelicBeastmen3 = templist[2];

        //            List<string> commandList = new List<string> { value + "_Relic", "N/A" };

        //        }
        //        if (value != null) { RelicVisibility = Visibility.Visible; }
        //        else { RelicVisibility = Visibility.Hidden; }
        //        NotifyPropertyChanged("ActiveRelic");
        //    }
        //}


        //private int relicIndex;
        //public int RelicIndex
        //{
        //    get { return relicIndex; }
        //    set
        //    {
        //        relicIndex = value;
        //        NotifyPropertyChanged("RelicIndex");
        //    }
        //}
        //#endregion
    }
}
