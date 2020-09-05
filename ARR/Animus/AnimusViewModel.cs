using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.ARR.Atma;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Animus
{
    public class AnimusViewModel : ObservableObject, IPageViewModel
    {
        private IEventAggregator iEventAggregator;
        private Character selectedCharacter;
        private AnimusModel animusModel;
        public ObservableCollection<string> AvailableBooks = new ObservableCollection<string>
            {
                "Book of Skyfire I",
                "Book of Skyfire II",
                "Book of Skyfall I",
                "Book of Skyfall II",
                "Book of Netherfire I",
                "Book of Netherfall I",
                "Book of Skywind I",
                "Book of Skyearth I"
            };

        private List<AnimusObject> LeveList = new List<AnimusObject>();
        private List<AnimusObject> FateList = new List<AnimusObject>();

        private List<AnimusObject> ObjectList = new List<AnimusObject>();


        public AnimusViewModel(IEventAggregator iEventAggregator)
        {
			this.iEventAggregator = iEventAggregator;
            SubscriptionToken subscriptionToken =
                            this
                                .iEventAggregator
                                .GetEvent<PubSubEvent<Character>>()
                                .Subscribe((details) =>
                                {
                                    this.SelectedCharacter = details;
                                });
            iEventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Subscribe((details) => { this.ArrWeapon = details; });
        }

        public string Name =>"Animus";
        public AnimusModel AnimusModel
        {
            get { return animusModel; }
            set
            {
                animusModel = value;
                OnPropertyChanged(nameof(AnimusModel));

                LeveList = new List<AnimusObject>
                {
                    Leve1, Leve2, Leve3
                }; 
                
                FateList = new List<AnimusObject>
                {
                    Fate1, Fate2, Fate3
                };

                ObjectList = new List<AnimusObject>
                {
                    Leve1, Leve2, Leve3,
                    Fate1, Fate2, Fate3
                };
            }
        }

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;

                OnPropertyChanged(nameof(SelectedCharacter));
                if (value != null)
                {
                    AnimusModel = SelectedCharacter.ArrProgress.AnimusModel;
                    AnimusBooks = animusModel.animusBooks;
                    ArrWeapon = SelectedCharacter.ArrProgress.Arr;

                }
            }
        }
        public string SelectedAnimusImage
        {
            get { return AnimusModel.selectedAnimusImage; }
            set
            {
                AnimusModel.selectedAnimusImage = value;
                OnPropertyChanged(nameof(SelectedAnimusImage));
            }
        }
        public string SelectedAnimusMap
        {
            get { return AnimusModel.selectedAnimusMap; }
            set
            {
                if (value == null)
                { SelectedAnimusImage = null; }
                else { SelectedAnimusImage = ArrInfo.ArrMapImages[value]; }

                AnimusModel.selectedAnimusMap = value;

                ReassignPoints();

                OnPropertyChanged(nameof(SelectedAnimusMap));
            }
        }
        public ArrWeapon ArrWeapon 
        {
            get { return arrWeapon; }
            set
            {
                arrWeapon = value;
                OnPropertyChanged(nameof(ArrWeapon));
                LoadAvailableJobs();
            }
        }

        public string CurrentBook
        {
            get { return animusModel.CurrentBook; }
            set
            {
                if (value != null)
                {
                    ShowBookItems = true;
                    int bookInt = ArrInfo.ReferenceBooks.IndexOf(value);

                    CreatureNames = ArrInfo.ReturnCreatureNames(bookInt);
                    CreatureMaps = ArrInfo.ReturnCreatureMaps(bookInt);
                    CreaturePoints = ArrInfo.ReturnCreaturePoints(bookInt);

                    Dungeons = ArrInfo.ReturnBookDungeons(bookInt);

                    FateNames = ArrInfo.ReturnFATENames(bookInt);
                    FateMaps = ArrInfo.ReturnFATEMaps(bookInt);
                    FatePoints = ArrInfo.ReturnFATEPoints(bookInt);

                    LeveNames = ArrInfo.ReturnLeveNames(bookInt);
                    LeveTypes = ArrInfo.ReturnLeveTypes(bookInt);
                    LevePersons = ArrInfo.ReturnLevePersons(bookInt);
                    LeveMaps = ArrInfo.ReturnLeveLocations(bookInt);
                    LevePoints = ArrInfo.ReturnLevePoints(bookInt);

                    if (animusModel.CurrentBook != null)
                    {
                        ResetBools();
                    }
                    AssignItems();

                    InitializeMapLists(animusModel.CurrentBook, value);

                    animusModel.CurrentBook = value;


                    OnPropertyChanged(nameof(CurrentBook));

                    ReassignPoints();
                }
                else
                {
                    ShowBookItems = false;
                }
            }
        }
        public int BookSelection
        {
            get { return animusModel.bookSelection; }
            set
            {
                animusModel.bookSelection = value;
                OnPropertyChanged("BookSelection");
            }
        }

        public ObservableCollection<string> AnimusBooks
        {
            get { return animusModel.animusBooks; }
            set
            {
                animusModel.animusBooks = value;
                OnPropertyChanged(nameof(AnimusBooks));
            }
        }

        public ObservableCollection<string> AvailableAnimusJobs
        {
            get { return animusModel.availableAnimusobs; }
            set
            {
                animusModel.availableAnimusobs = value;
                OnPropertyChanged(nameof(AvailableAnimusJobs));
            }
        }

        public string CurrentAnimus
        {
            get { return animusModel.CurrentAnimus; }
            set
            {
                animusModel.CurrentAnimus = value;
                OnPropertyChanged(nameof(CurrentAnimus));
            }
        }

        #region Animus Objects

        public AnimusObject DisplayLeve { get { return animusModel.DisplayLeve; } set { animusModel.DisplayLeve = value; OnPropertyChanged(nameof(DisplayLeve)); } }


        public AnimusObject Leve1 { get { return animusModel.Leve1; } set { animusModel.Leve1 = value; OnPropertyChanged(nameof(Leve1)); } }
        public AnimusObject Leve2 { get { return animusModel.Leve2; } set { animusModel.Leve2 = value; OnPropertyChanged(nameof(Leve2)); } }
        public AnimusObject Leve3 { get { return animusModel.Leve3; } set { animusModel.Leve3 = value; OnPropertyChanged(nameof(Leve3)); } }


        public AnimusObject DisplayFate { get { return animusModel.DisplayFate; } set { animusModel.DisplayFate = value; OnPropertyChanged(nameof(DisplayFate)); } }


        public AnimusObject Fate1 { get { return animusModel.Fate1; } set { animusModel.Fate1 = value; OnPropertyChanged(nameof(Fate1)); } }
        public AnimusObject Fate2 { get { return animusModel.Fate2; } set { animusModel.Fate2 = value; OnPropertyChanged(nameof(Fate2)); } }
        public AnimusObject Fate3 { get { return animusModel.Fate3; } set { animusModel.Fate3 = value; OnPropertyChanged(nameof(Fate3)); } }

        #endregion


        #region Book Information

        public bool SkyFire1Book { get { return animusModel.skyFire1Book; } set { animusModel.skyFire1Book = value; ChangedBookStatus(nameof(SkyFire1Book), value); OnPropertyChanged(nameof(SkyFire1Book)); } }
        public bool SkyFire2Book { get { return animusModel.skyFire2Book; } set { animusModel.skyFire2Book = value; ChangedBookStatus(nameof(SkyFire2Book), value); OnPropertyChanged(nameof(SkyFire2Book)); } }
        public bool SkyFall1Book { get { return animusModel.skyFall1Book; } set { animusModel.skyFall1Book = value; ChangedBookStatus(nameof(SkyFall1Book), value); OnPropertyChanged(nameof(SkyFall1Book)); } }
        public bool SkyFall2Book { get { return animusModel.skyFall2Book; } set { animusModel.skyFall2Book = value; ChangedBookStatus(nameof(SkyFall2Book), value); OnPropertyChanged(nameof(SkyFall2Book)); } }
        public bool NetherFire1Book { get { return animusModel.netherFire1Book; } set { animusModel.netherFire1Book = value; ChangedBookStatus(nameof(NetherFire1Book), value); OnPropertyChanged(nameof(NetherFire1Book)); } }
        public bool NetherFall1Book { get { return animusModel.netherFall1Book; } set { animusModel.netherFall1Book = value; ChangedBookStatus(nameof(NetherFall1Book), value); OnPropertyChanged(nameof(NetherFall1Book)); } }
        public bool SkyWind1Book { get { return animusModel.skyWind1Book; } set { animusModel.skyWind1Book = value; ChangedBookStatus(nameof(SkyWind1Book), value); OnPropertyChanged(nameof(SkyWind1Book)); } }
        public bool SkyEarth1Book { get { return animusModel.skyEarth1Book; } set { animusModel.skyEarth1Book = value; ChangedBookStatus(nameof(SkyEarth1Book), value); OnPropertyChanged(nameof(SkyEarth1Book)); } }

        #region Creature Points on Map

        #region Fate Point Parameters
        public Visibility FateVisibility1 { get { return animusModel.fateVisibility1; } set { animusModel.fateVisibility1 = value; OnPropertyChanged(nameof(FateVisibility1)); } }
        public string FateActive1 { get { return animusModel.fateActive1; } set { animusModel.fateActive1 = value; OnPropertyChanged(nameof(FateActive1)); } }
        public double FateX1 { get { return animusModel.fateX1; } set { animusModel.fateX1 = value; OnPropertyChanged(nameof(FateX1)); } }
        public double FateY1 { get { return animusModel.fateY1; } set { animusModel.fateY1 = value; OnPropertyChanged(nameof(FateY1)); } }
        #endregion

        #region Creature 1
        public Visibility CreatureVisibility1 { get { return animusModel.creatureVisibility1; } set { animusModel.creatureVisibility1 = value; OnPropertyChanged(nameof(CreatureVisibility1)); } }
        public string CreatureActive1 { get { return animusModel.creatureActive1; } set { animusModel.creatureActive1 = value; OnPropertyChanged(nameof(CreatureActive1)); } }
        public double CreatureX1 { get { return animusModel.creatureX1; } set { animusModel.creatureX1 = value; OnPropertyChanged(nameof(CreatureX1)); } }
        public double CreatureY1 { get { return animusModel.creatureY1; } set { animusModel.creatureY1 = value; OnPropertyChanged(nameof(CreatureY1)); } }
        #endregion

        #region Creature 2
        public Visibility CreatureVisibility2 { get { return animusModel.creatureVisibility2; } set { animusModel.creatureVisibility2 = value; OnPropertyChanged(nameof(CreatureVisibility2)); } }
        public string CreatureActive2 { get { return animusModel.creatureActive2; } set { animusModel.creatureActive2 = value; OnPropertyChanged(nameof(CreatureActive2)); } }
        public double CreatureX2 { get { return animusModel.creatureX2; } set { animusModel.creatureX2 = value; OnPropertyChanged(nameof(CreatureX2)); } }
        public double CreatureY2 { get { return animusModel.creatureY2; } set { animusModel.creatureY2 = value; OnPropertyChanged(nameof(CreatureY2)); } }
        #endregion

        #region Creature 3
        public Visibility CreatureVisibility3 { get { return animusModel.creatureVisibility3; } set { animusModel.creatureVisibility3 = value; OnPropertyChanged(nameof(CreatureVisibility3)); } }
        public string CreatureActive3 { get { return animusModel.creatureActive3; } set { animusModel.creatureActive3 = value; OnPropertyChanged(nameof(CreatureActive3)); } }
        public double CreatureX3 { get { return animusModel.creatureX3; } set { animusModel.creatureX3 = value; OnPropertyChanged(nameof(CreatureX3)); } }
        public double CreatureY3 { get { return animusModel.creatureY3; } set { animusModel.creatureY3 = value; OnPropertyChanged(nameof(CreatureY3)); } }
        #endregion

        #region Creature 4
        public Visibility CreatureVisibility4 { get { return animusModel.creatureVisibility4; } set { animusModel.creatureVisibility4 = value; OnPropertyChanged(nameof(CreatureVisibility4)); } }
        public string CreatureActive4 { get { return animusModel.creatureActive4; } set { animusModel.creatureActive4 = value; OnPropertyChanged(nameof(CreatureActive4)); } }
        public double CreatureX4 { get { return animusModel.creatureX4; } set { animusModel.creatureX4 = value; OnPropertyChanged(nameof(CreatureX4)); } }
        public double CreatureY4 { get { return animusModel.creatureY4; } set { animusModel.creatureY4 = value; OnPropertyChanged(nameof(CreatureY4)); } }
        #endregion

        #region Creature 5
        public Visibility CreatureVisibility5 { get { return animusModel.creatureVisibility5; } set { animusModel.creatureVisibility5 = value; OnPropertyChanged(nameof(CreatureVisibility5)); } }
        public string CreatureActive5 { get { return animusModel.creatureActive5; } set { animusModel.creatureActive5 = value; OnPropertyChanged(nameof(CreatureActive5)); } }
        public double CreatureX5 { get { return animusModel.creatureX5; } set { animusModel.creatureX5 = value; OnPropertyChanged(nameof(CreatureX5)); } }
        public double CreatureY5 { get { return animusModel.creatureY5; } set { animusModel.creatureY5 = value; OnPropertyChanged(nameof(CreatureY5)); } }
        #endregion

        #endregion

        public List<string> Leves { get { return animusModel.leves; } set { animusModel.leves = value; OnPropertyChanged(nameof(Leves)); } }
        public List<string> Fates { get { return animusModel.fates; } set { animusModel.fates = value; OnPropertyChanged(nameof(Fates)); } }
        public List<string> Creatures { get { return animusModel.creatures; } set { animusModel.creatures = value; OnPropertyChanged(nameof(Creatures)); } }

        public bool ShowBookItems { get { return animusModel.showBookItems; } set { animusModel.showBookItems = value; OnPropertyChanged(nameof(ShowBookItems)); } }
        public List<string> ObservableUniqueMaps { get { return animusModel.observableUniqueMaps; } set { animusModel.observableUniqueMaps = value; OnPropertyChanged(nameof(ObservableUniqueMaps)); } }
        public List<string> ObservableAllMaps { get { return animusModel.observableAllMaps; } set { animusModel.observableAllMaps = value; OnPropertyChanged(nameof(ObservableAllMaps)); } }

        public bool CompletedDungeon1 { get { return animusModel.completedDungeon1; } set { animusModel.completedDungeon1 = value; OnPropertyChanged(nameof(CompletedDungeon1)); } }
        public bool CompletedDungeon2 { get { return animusModel.completedDungeon2; } set { animusModel.completedDungeon2 = value; OnPropertyChanged(nameof(CompletedDungeon2)); } }
        public bool CompletedDungeon3 { get { return animusModel.completedDungeon3; } set { animusModel.completedDungeon3 = value; OnPropertyChanged(nameof(CompletedDungeon3)); } }

        public BoolObject CompletedFate1 { get { return animusModel.completedFate1; } set { animusModel.completedFate1 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedFate1)); } }
        public BoolObject CompletedFate2 { get { return animusModel.completedFate2; } set { animusModel.completedFate2 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedFate2)); } }
        public BoolObject CompletedFate3 { get { return animusModel.completedFate3; } set { animusModel.completedFate3 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedFate3)); } }

        public bool CompletedCreature1 { get { return animusModel.completedCreature1; } set { animusModel.completedCreature1 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature1)); } }
        public bool CompletedCreature2 { get { return animusModel.completedCreature2; } set { animusModel.completedCreature2 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature2)); } }
        public bool CompletedCreature3 { get { return animusModel.completedCreature3; } set { animusModel.completedCreature3 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature3)); } }
        public bool CompletedCreature4 { get { return animusModel.completedCreature4; } set { animusModel.completedCreature4 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature4)); } }
        public bool CompletedCreature5 { get { return animusModel.completedCreature5; } set { animusModel.completedCreature5 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature5)); } }
        public bool CompletedCreature6 { get { return animusModel.completedCreature6; } set { animusModel.completedCreature6 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature6)); } }
        public bool CompletedCreature7 { get { return animusModel.completedCreature7; } set { animusModel.completedCreature7 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature7)); } }
        public bool CompletedCreature8 { get { return animusModel.completedCreature8; } set { animusModel.completedCreature8 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature8)); } }
        public bool CompletedCreature9 { get { return animusModel.completedCreature9; } set { animusModel.completedCreature9 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature9)); } }
        public bool CompletedCreature10 { get { return animusModel.completedCreature10; } set { animusModel.completedCreature10 = value; ReCheckMaps(); OnPropertyChanged(nameof(CompletedCreature10)); } }

        public bool PossibleChecked1 { get { return animusModel.possibleChecked1; } set { animusModel.possibleChecked1 = value; OnPropertyChanged(nameof(PossibleChecked1)); } }
        public bool PossibleChecked2 { get { return animusModel.possibleChecked2; } set { animusModel.possibleChecked2 = value; OnPropertyChanged(nameof(PossibleChecked2)); } }
        public bool PossibleChecked3 { get { return animusModel.possibleChecked3; } set { animusModel.possibleChecked3 = value; OnPropertyChanged(nameof(PossibleChecked3)); } }
        public bool PossibleChecked4 { get { return animusModel.possibleChecked4; } set { animusModel.possibleChecked4 = value; OnPropertyChanged(nameof(PossibleChecked4)); } }
        public bool PossibleChecked5 { get { return animusModel.possibleChecked5; } set { animusModel.possibleChecked5 = value; OnPropertyChanged(nameof(PossibleChecked5)); } }
        public bool PossibleChecked6 { get { return animusModel.possibleChecked6; } set { animusModel.possibleChecked6 = value; OnPropertyChanged(nameof(PossibleChecked6)); } }
        public bool PossibleChecked7 { get { return animusModel.possibleChecked7; } set { animusModel.possibleChecked7 = value; OnPropertyChanged(nameof(PossibleChecked7)); } }
        public bool PossibleChecked8 { get { return animusModel.possibleChecked8; } set { animusModel.possibleChecked8 = value; OnPropertyChanged(nameof(PossibleChecked8)); } }
        public bool PossibleChecked9 { get { return animusModel.possibleChecked9; } set { animusModel.possibleChecked9 = value; OnPropertyChanged(nameof(PossibleChecked9)); } }
        public bool PossibleChecked10 { get { return animusModel.possibleChecked10; } set { animusModel.possibleChecked10 = value; OnPropertyChanged(nameof(PossibleChecked10)); } }

        public bool PossibleVisibility1 { get { return animusModel.possibleVisibility1; } set { animusModel.possibleVisibility1 = value; OnPropertyChanged(nameof(PossibleVisibility1)); } }
        public bool PossibleVisibility2 { get { return animusModel.possibleVisibility2; } set { animusModel.possibleVisibility2 = value; OnPropertyChanged(nameof(PossibleVisibility2)); } }
        public bool PossibleVisibility3 { get { return animusModel.possibleVisibility3; } set { animusModel.possibleVisibility3 = value; OnPropertyChanged(nameof(PossibleVisibility3)); } }
        public bool PossibleVisibility4 { get { return animusModel.possibleVisibility4; } set { animusModel.possibleVisibility4 = value; OnPropertyChanged(nameof(PossibleVisibility4)); } }
        public bool PossibleVisibility5 { get { return animusModel.possibleVisibility5; } set { animusModel.possibleVisibility5 = value; OnPropertyChanged(nameof(PossibleVisibility5)); } }
        public bool PossibleVisibility6 { get { return animusModel.possibleVisibility6; } set { animusModel.possibleVisibility6 = value; OnPropertyChanged(nameof(PossibleVisibility6)); } }
        public bool PossibleVisibility7 { get { return animusModel.possibleVisibility7; } set { animusModel.possibleVisibility7 = value; OnPropertyChanged(nameof(PossibleVisibility7)); } }
        public bool PossibleVisibility8 { get { return animusModel.possibleVisibility8; } set { animusModel.possibleVisibility8 = value; OnPropertyChanged(nameof(PossibleVisibility8)); } }
        public bool PossibleVisibility9 { get { return animusModel.possibleVisibility9; } set { animusModel.possibleVisibility9 = value; OnPropertyChanged(nameof(PossibleVisibility9)); } }
        public bool PossibleVisibility10 { get { return animusModel.possibleVisibility10; } set { animusModel.possibleVisibility10 = value; OnPropertyChanged(nameof(PossibleVisibility10)); } }

        public string PossibleMap1 { get { return animusModel.possibleMap1; } set { animusModel.possibleMap1 = value; OnPropertyChanged(nameof(PossibleMap1)); } }
        public string PossibleMap2 { get { return animusModel.possibleMap2; } set { animusModel.possibleMap2 = value; OnPropertyChanged(nameof(PossibleMap2)); } }
        public string PossibleMap3 { get { return animusModel.possibleMap3; } set { animusModel.possibleMap3 = value; OnPropertyChanged(nameof(PossibleMap3)); } }
        public string PossibleMap4 { get { return animusModel.possibleMap4; } set { animusModel.possibleMap4 = value; OnPropertyChanged(nameof(PossibleMap4)); } }
        public string PossibleMap5 { get { return animusModel.possibleMap5; } set { animusModel.possibleMap5 = value; OnPropertyChanged(nameof(PossibleMap5)); } }
        public string PossibleMap6 { get { return animusModel.possibleMap6; } set { animusModel.possibleMap6 = value; OnPropertyChanged(nameof(PossibleMap6)); } }
        public string PossibleMap7 { get { return animusModel.possibleMap7; } set { animusModel.possibleMap7 = value; OnPropertyChanged(nameof(PossibleMap7)); } }
        public string PossibleMap8 { get { return animusModel.possibleMap8; } set { animusModel.possibleMap8 = value; OnPropertyChanged(nameof(PossibleMap8)); } }
        public string PossibleMap9 { get { return animusModel.possibleMap9; } set { animusModel.possibleMap9 = value; OnPropertyChanged(nameof(PossibleMap9)); } }
        public string PossibleMap10 { get { return animusModel.possibleMap10; } set { animusModel.possibleMap10 = value; OnPropertyChanged(nameof(PossibleMap10)); } }

        #region Animus Per Book properties

        private List<string> CreatureNames;
        private List<string> CreatureMaps;
        private List<PointF> CreaturePoints;

        private List<string> Dungeons;

        private List<string> FateNames;
        private List<string> FateMaps;
        private List<PointF> FatePoints;

        private List<string> LeveNames;
        private List<string> LeveTypes;
        private List<string> LevePersons;
        private List<string> LeveMaps;
        private List<PointF> LevePoints;

        public string Creature1 { get { return animusModel.creature1; } set { animusModel.creature1 = value; OnPropertyChanged(nameof(Creature1)); } }
        public string Creature2 { get { return animusModel.creature2; } set { animusModel.creature2 = value; OnPropertyChanged(nameof(Creature2)); } }
        public string Creature3 { get { return animusModel.creature3; } set { animusModel.creature3 = value; OnPropertyChanged(nameof(Creature3)); } }
        public string Creature4 { get { return animusModel.creature4; } set { animusModel.creature4 = value; OnPropertyChanged(nameof(Creature4)); } }
        public string Creature5 { get { return animusModel.creature5; } set { animusModel.creature5 = value; OnPropertyChanged(nameof(Creature5)); } }
        public string Creature6 { get { return animusModel.creature6; } set { animusModel.creature6 = value; OnPropertyChanged(nameof(Creature6)); } }
        public string Creature7 { get { return animusModel.creature7; } set { animusModel.creature7 = value; OnPropertyChanged(nameof(Creature7)); } }
        public string Creature8 { get { return animusModel.creature8; } set { animusModel.creature8 = value; OnPropertyChanged(nameof(Creature8)); } }
        public string Creature9 { get { return animusModel.creature9; } set { animusModel.creature9 = value; OnPropertyChanged(nameof(Creature9)); } }
        public string Creature10 { get { return animusModel.creature10; } set { animusModel.creature10 = value; OnPropertyChanged(nameof(Creature10)); } }


        public string CreatureMap1 { get { return animusModel.creatureMap1; } set { animusModel.creatureMap1 = value; OnPropertyChanged(nameof(CreatureMap1)); } }
        public string CreatureMap2 { get { return animusModel.creatureMap2; } set { animusModel.creatureMap2 = value; OnPropertyChanged(nameof(CreatureMap2)); } }
        public string CreatureMap3 { get { return animusModel.creatureMap3; } set { animusModel.creatureMap3 = value; OnPropertyChanged(nameof(CreatureMap3)); } }
        public string CreatureMap4 { get { return animusModel.creatureMap4; } set { animusModel.creatureMap4 = value; OnPropertyChanged(nameof(CreatureMap4)); } }
        public string CreatureMap5 { get { return animusModel.creatureMap5; } set { animusModel.creatureMap5 = value; OnPropertyChanged(nameof(CreatureMap5)); } }
        public string CreatureMap6 { get { return animusModel.creatureMap6; } set { animusModel.creatureMap6 = value; OnPropertyChanged(nameof(CreatureMap6)); } }
        public string CreatureMap7 { get { return animusModel.creatureMap7; } set { animusModel.creatureMap7 = value; OnPropertyChanged(nameof(CreatureMap7)); } }
        public string CreatureMap8 { get { return animusModel.creatureMap8; } set { animusModel.creatureMap8 = value; OnPropertyChanged(nameof(CreatureMap8)); } }
        public string CreatureMap9 { get { return animusModel.creatureMap9; } set { animusModel.creatureMap9 = value; OnPropertyChanged(nameof(CreatureMap9)); } }
        public string CreatureMap10 { get { return animusModel.creatureMap10; } set { animusModel.creatureMap10 = value; OnPropertyChanged(nameof(CreatureMap10)); } }


        public PointF CreaturePoint1 { get { return animusModel.creaturePoint1; } set { animusModel.creaturePoint1 = value; OnPropertyChanged(nameof(CreaturePoint1)); } }
        public PointF CreaturePoint2 { get { return animusModel.creaturePoint2; } set { animusModel.creaturePoint2 = value; OnPropertyChanged(nameof(CreaturePoint2)); } }
        public PointF CreaturePoint3 { get { return animusModel.creaturePoint3; } set { animusModel.creaturePoint3 = value; OnPropertyChanged(nameof(CreaturePoint3)); } }
        public PointF CreaturePoint4 { get { return animusModel.creaturePoint4; } set { animusModel.creaturePoint4 = value; OnPropertyChanged(nameof(CreaturePoint4)); } }
        public PointF CreaturePoint5 { get { return animusModel.creaturePoint5; } set { animusModel.creaturePoint5 = value; OnPropertyChanged(nameof(CreaturePoint5)); } }
        public PointF CreaturePoint6 { get { return animusModel.creaturePoint6; } set { animusModel.creaturePoint6 = value; OnPropertyChanged(nameof(CreaturePoint6)); } }
        public PointF CreaturePoint7 { get { return animusModel.creaturePoint7; } set { animusModel.creaturePoint7 = value; OnPropertyChanged(nameof(CreaturePoint7)); } }
        public PointF CreaturePoint8 { get { return animusModel.creaturePoint8; } set { animusModel.creaturePoint8 = value; OnPropertyChanged(nameof(CreaturePoint8)); } }
        public PointF CreaturePoint9 { get { return animusModel.creaturePoint9; } set { animusModel.creaturePoint9 = value; OnPropertyChanged(nameof(CreaturePoint9)); } }
        public PointF CreaturePoint10 { get { return animusModel.creaturePoint10; } set { animusModel.creaturePoint10 = value; OnPropertyChanged(nameof(CreaturePoint10)); } }


        public string Dungeon1 { get { return animusModel.dungeon1; } set { animusModel.dungeon1 = value; OnPropertyChanged(nameof(Dungeon1)); } }
        public string Dungeon2 { get { return animusModel.dungeon2; } set { animusModel.dungeon2 = value; OnPropertyChanged(nameof(Dungeon2)); } }
        public string Dungeon3 { get { return animusModel.dungeon3; } set { animusModel.dungeon3 = value; OnPropertyChanged(nameof(Dungeon3)); } }


        public string FateName1 { get { return animusModel.fateName1; } set { animusModel.fateName3 = value; OnPropertyChanged(nameof(FateName1)); } }
        public string FateName2 { get { return animusModel.fateName2; } set { animusModel.fateName2 = value; OnPropertyChanged(nameof(FateName2)); } }
        public string FateName3 { get { return animusModel.fateName3; } set { animusModel.fateName1 = value; OnPropertyChanged(nameof(FateName3)); } }

        public string FateMap1 { get { return animusModel.fateMap1; } set { animusModel.fateMap3 = value; OnPropertyChanged(nameof(FateMap1)); } }
        public string FateMap2 { get { return animusModel.fateMap2; } set { animusModel.fateMap2 = value; OnPropertyChanged(nameof(FateMap2)); } }
        public string FateMap3 { get { return animusModel.fateMap3; } set { animusModel.fateMap1 = value; OnPropertyChanged(nameof(FateMap3)); } }

        public PointF FatePoint1 { get { return animusModel.fatePoint1; } set { animusModel.fatePoint3 = value; OnPropertyChanged(nameof(FatePoint1)); } }
        public PointF FatePoint2 { get { return animusModel.fatePoint2; } set { animusModel.fatePoint2 = value; OnPropertyChanged(nameof(FatePoint2)); } }
        public PointF FatePoint3 { get { return animusModel.fatePoint3; } set { animusModel.fatePoint1 = value; OnPropertyChanged(nameof(FatePoint3)); } }

        #endregion
        #endregion

        #region Methods

        private void ResetBooks()
        {
            SkyFire1Book = false;
            SkyFire2Book = false;
            SkyFall1Book = false;
            SkyFall2Book = false;
            NetherFire1Book = false;
            NetherFall1Book = false;
            SkyWind1Book = false;
            SkyEarth1Book = false;
        }

        private void ResetBools()
        {
            ClearMaps();

            CompletedCreature1 = false;
            CompletedCreature2 = false;
            CompletedCreature3 = false;
            CompletedCreature4 = false;
            CompletedCreature5 = false;
            CompletedCreature6 = false;
            CompletedCreature7 = false;
            CompletedCreature8 = false;
            CompletedCreature9 = false;
            CompletedCreature10 = false;

            CompletedDungeon1 = false;
            CompletedDungeon2 = false;
            CompletedDungeon3 = false;

            Leve1.Completed.Bool = false;
            Leve2.Completed.Bool = false;
            Leve3.Completed.Bool = false;

            Fate1.Completed.Bool = false;
            Fate2.Completed.Bool = false;
            Fate3.Completed.Bool = false;
        }

        private void LoadAvailableJobs()
        {
            AvailableAnimusJobs = new ObservableCollection<string>();
            {

                foreach(ArrStages job in ArrWeapon)
                {
                    if (job.Atma.Progress==ArrProgress.States.Completed & job.Animus.Progress != ArrProgress.States.Completed)
                    {
                        AvailableAnimusJobs.Add(job.Name);
                    }
                }
            }
        }

        public void ReassignPoints()
        {
            #region Set Fates

            DisplayFate = new AnimusObject();

            foreach (AnimusObject leve in FateList)
            {
                if (leve.Map == SelectedAnimusMap)
                {
                    DisplayFate = leve;
                }
            }

            FateVisibility1 = Visibility.Visible;

            if (FateMap1 == SelectedAnimusMap & !CompletedFate1.Bool)
            {
                FateActive1 = FateName1;
                FateX1 = (800 * FatePoint1.X / 42) - 15;
                FateY1 = (790 * FatePoint1.Y / 42) - 15;
            }
            else if (FateMap2 == SelectedAnimusMap & !CompletedFate2.Bool)
            {
                FateActive1 = FateName2;
                FateX1 = 800 * FatePoint2.X / 42 - 15;
                FateY1 = 800 * FatePoint2.Y / 42 - 15;
            }
            else if (FateMap3 == SelectedAnimusMap & !CompletedFate3.Bool)
            {
                FateActive1 = FateName3;
                FateX1 = 800 * FatePoint3.X / 42 - 15;
                FateY1 = 800 * FatePoint3.Y / 42 - 15;
            }
            else
            {
                FateVisibility1 = Visibility.Hidden;
            }

            #endregion
            #region Set Leves

            DisplayLeve = new AnimusObject();

            foreach(AnimusObject leve in LeveList)
            {
                if(leve.Map== SelectedAnimusMap)
                {
                    DisplayLeve = leve;
                }
            }

            #endregion

            UnassignPoints();

            #region Set Creatures

            List<string> tempX = new List<string>
            {
                nameof(CreatureX1),
                nameof(CreatureX2),
                nameof(CreatureX3),
                nameof(CreatureX4),
                nameof(CreatureX5)
            };

            List<string> tempY = new List<string>
            {
                nameof(CreatureY1),
                nameof(CreatureY2),
                nameof(CreatureY3),
                nameof(CreatureY4),
                nameof(CreatureY5)
            };

            List<string> tempVisibility = new List<string>
            {
                nameof(CreatureVisibility1),
                nameof(CreatureVisibility2),
                nameof(CreatureVisibility3),
                nameof(CreatureVisibility4),
                nameof(CreatureVisibility5)
            };

            List<string> tempActive = new List<string>
            {
                nameof(CreatureActive1),
                nameof(CreatureActive2),
                nameof(CreatureActive3),
                nameof(CreatureActive4),
                nameof(CreatureActive5)
            };

            List<string> tempCreatureMaps = new List<string>
            {
                nameof(CreatureMap1),
                nameof(CreatureMap2),
                nameof(CreatureMap3),
                nameof(CreatureMap4),
                nameof(CreatureMap5),
                nameof(CreatureMap6),
                nameof(CreatureMap7),
                nameof(CreatureMap8),
                nameof(CreatureMap9),
                nameof(CreatureMap10)
            };

            List<string> tempCreatureNames = new List<string>
            {
                nameof(Creature1),
                nameof(Creature2),
                nameof(Creature3),
                nameof(Creature4),
                nameof(Creature5),
                nameof(Creature6),
                nameof(Creature7),
                nameof(Creature8),
                nameof(Creature9),
                nameof(Creature10)
            };

            List<string> tempPoints = new List<string>
            {
                nameof(CreaturePoint1),
                nameof(CreaturePoint2),
                nameof(CreaturePoint3),
                nameof(CreaturePoint4),
                nameof(CreaturePoint5),
                nameof(CreaturePoint6),
                nameof(CreaturePoint7),
                nameof(CreaturePoint8),
                nameof(CreaturePoint9),
                nameof(CreaturePoint10)
            };

            List<string> tempBools = new List<string>
            {
                nameof(CompletedCreature1),
                nameof(CompletedCreature2),
                nameof(CompletedCreature3),
                nameof(CompletedCreature4),
                nameof(CompletedCreature5),
                nameof(CompletedCreature6),
                nameof(CompletedCreature7),
                nameof(CompletedCreature8),
                nameof(CompletedCreature9),
                nameof(CompletedCreature10)
            };

            while (tempCreatureMaps.Count > 0)
            {
                PropertyInfo creatureMap = typeof(AnimusViewModel).GetProperty(tempCreatureMaps[0]);
                string checkMap = (string)creatureMap.GetValue(this);
                if (checkMap == SelectedAnimusMap)
                {
                    PropertyInfo creaturePoint = typeof(AnimusViewModel).GetProperty(tempPoints[0]);
                    PointF selectPoint = (PointF)creaturePoint.GetValue(this);

                    PropertyInfo creatureName = typeof(AnimusViewModel).GetProperty(tempCreatureNames[0]);
                    string selectName = (string)creatureName.GetValue(this);

                    PropertyInfo creatureComplete = typeof(AnimusViewModel).GetProperty(tempBools[0]);
                    bool selectBool = (bool)creatureComplete.GetValue(this);

                    for (int i = 0; i <= 4; i++)
                    {
                        PropertyInfo plotPointX = typeof(AnimusViewModel).GetProperty(tempX[i]);
                        PropertyInfo plotPointY = typeof(AnimusViewModel).GetProperty(tempY[i]);

                        PropertyInfo plotName = typeof(AnimusViewModel).GetProperty(tempActive[i]);



                        PropertyInfo plotVisibility = typeof(AnimusViewModel).GetProperty(tempVisibility[i]);
                        Visibility selectVisibility = (Visibility)plotVisibility.GetValue(this);

                        double tempDbl = (double)plotPointX.GetValue(this);
                        if (tempDbl == (double)0)
                        {

                            plotPointX.SetValue(this, 800 * selectPoint.X / 42 - 15);
                            plotPointY.SetValue(this, 790 * selectPoint.Y / 42 - 15);
                            if (!selectBool)
                            {
                                plotVisibility.SetValue(this, Visibility.Visible);
                            }
                            else { plotVisibility.SetValue(this, Visibility.Hidden); }
                            plotName.SetValue(this, selectName);
                            break;
                        }
                    }
                }
                tempCreatureMaps.RemoveAt(0);
                tempCreatureNames.RemoveAt(0);
                tempPoints.RemoveAt(0);
                tempBools.RemoveAt(0);
            }

            AssignItems();

            #endregion

        }

        private void ChangedBookStatus(string book, bool newStatus)
        {
            int bookindex = ArrInfo.BookList.IndexOf(book);
            string bookString = ArrInfo.ReferenceBooks[bookindex];

            int compareIndex;

            if (newStatus)
            {

                AnimusBooks.Remove(bookString);

                // Maybe not needed, apparently I shouldn't bind to combobox indices
                if (BookSelection == AnimusBooks.IndexOf(bookString))
                { BookSelection = 0; }
            }
            else
            {
                switch (AnimusBooks.Count)
                {
                    case 0:
                        AnimusBooks.Add(bookString);
                        CurrentBook = AnimusBooks[0];
                        break;
                    case 1:
                        compareIndex = ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[0]);
                        if (compareIndex > bookindex)
                        {
                            AnimusBooks.Insert(0, bookString);
                        }
                        break;
                    default:
                        compareIndex = ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[0]);
                        if (compareIndex > bookindex)
                        {
                            AnimusBooks.Insert(0, bookString);
                        }
                        else if (ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[AnimusBooks.Count - 1]) < bookindex)
                        {
                            AnimusBooks.Add(bookString);
                        }
                        else
                        {
                            int maxIndex = AnimusBooks.Count;
                            int firstIndex;
                            int secondIndex;
                            for (int i = 0; i < maxIndex - 1; i++)
                            {
                                firstIndex = ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[i]);
                                secondIndex = ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[i + 1]);

                                if (firstIndex < bookindex & bookindex < secondIndex)
                                {
                                    AnimusBooks.Insert(i + 1, bookString);
                                    break;
                                }
                            }
                        }
                        break;
                }
            }

        }
        private void UnassignPoints()
        {
            CreatureX1 = 0;
            CreatureX2 = 0;
            CreatureX3 = 0;
            CreatureX4 = 0;
            CreatureX5 = 0;

            CreatureY1 = 0;
            CreatureY2 = 0;
            CreatureY3 = 0;
            CreatureY4 = 0;
            CreatureY5 = 0;

            CreatureVisibility1 = Visibility.Hidden;
            CreatureVisibility2 = Visibility.Hidden;
            CreatureVisibility3 = Visibility.Hidden;
            CreatureVisibility4 = Visibility.Hidden;
            CreatureVisibility5 = Visibility.Hidden;

            CreatureActive1 = null;
            CreatureActive2 = null;
            CreatureActive3 = null;
            CreatureActive4 = null;
            CreatureActive5 = null;

        }

        public void AssignItems()
        {
            Fates = new List<string>();
            Leves = new List<string>();
            Creatures = new List<string>();

            #region Unique Map assignment+sort

            List<string> UniqueMaps = new List<string>();

            foreach (string map in CreatureMaps)
            {
                if (UniqueMaps.IndexOf(map) < 0) { UniqueMaps.Add(map); }
            }

            foreach (string map in FateMaps)
            {
                if (UniqueMaps.IndexOf(map) < 0) { UniqueMaps.Add(map); }
            }

            foreach (string map in LeveMaps)
            {
                if (UniqueMaps.IndexOf(map) < 0) { UniqueMaps.Add(map); }
            }

            List<string> tempUniqueMaps = SortArrMaps(UniqueMaps);

            #endregion

            #region Sort Creatures

            List<string> sortedCreatureMaps = new List<string>();
            List<string> sortedCreatureNames = new List<string>();
            List<PointF> sortedCreaturePoints = new List<PointF>();

            List<string> tempCreatureMaps = new List<string>();
            List<string> tempCreatureNames = new List<string>();

            foreach (string map in CreatureMaps)
            {
                tempCreatureMaps.Add(map);
            }
            foreach (string creature in CreatureNames)
            {
                tempCreatureNames.Add(creature);
            }

            tempCreatureMaps = SortArrMaps(tempCreatureMaps);

            while (tempCreatureNames.Count != 0)
            {
                for (int i = 0; i <= tempCreatureMaps.Count - 1; i++)
                {
                    for (int i2 = 0; i2 <= tempCreatureMaps.Count - 1; i2++)
                    {
                        if (tempCreatureMaps[i] == CreatureMaps[i2] & !sortedCreatureNames.Contains(CreatureNames[i2]))
                        {
                            sortedCreatureMaps.Add(CreatureMaps[i2]);
                            sortedCreatureNames.Add(CreatureNames[i2]);
                            sortedCreaturePoints.Add(CreaturePoints[i2]);
                            tempCreatureNames.Remove(CreatureNames[i2]);
                            break;
                        }
                    }
                }
            }
            #endregion

            #region Sort Fates

            List<string> sortedFateMaps = new List<string>();
            List<string> sortedFateNames = new List<string>();
            List<PointF> sortedFatePoints = new List<PointF>();

            List<string> tempFateMaps = new List<string>();
            List<string> tempFateNames = new List<string>();

            foreach (string map in FateMaps)
            {
                tempFateMaps.Add(map);
            }
            foreach (string creature in FateNames)
            {
                tempFateNames.Add(creature);
            }

            tempFateMaps = SortArrMaps(tempFateMaps);

            while (tempFateNames.Count != 0)
            {
                for (int i = 0; i <= tempFateMaps.Count - 1; i++)
                {
                    for (int i2 = 0; i2 <= tempFateMaps.Count - 1; i2++)
                    {
                        if (tempFateMaps[i] == FateMaps[i2] & !sortedFateNames.Contains(FateNames[i2]))
                        {
                            sortedFateMaps.Add(FateMaps[i2]);
                            sortedFateNames.Add(FateNames[i2]);
                            sortedFatePoints.Add(FatePoints[i2]);
                            tempFateNames.Remove(FateNames[i2]);
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Sort Leves

            List<string> sortedLeveMaps = new List<string>();
            List<string> sortedLeveNames = new List<string>();
            List<string> sortedLeveTypes = new List<string>();
            List<string> sortedLevePerson = new List<string>();
            List<PointF> sortedLevePoints = new List<PointF>();

            List<string> tempLeveMaps = new List<string>();
            List<string> tempLeveNames = new List<string>();

            foreach (string map in LeveMaps)
            {
                tempLeveMaps.Add(map);
            }
            foreach (string name in LeveNames)
            {
                tempLeveNames.Add(name);
            }

            tempLeveMaps = SortArrMaps(tempLeveMaps);

            while (tempLeveNames.Count != 0)
            {
                for (int i = 0; i <= tempLeveMaps.Count - 1; i++)
                {
                    for (int i2 = 0; i2 <= tempLeveMaps.Count - 1; i2++)
                    {
                        if (tempLeveMaps[i] == LeveMaps[i2] & !sortedLeveNames.Contains(LeveNames[i2]))
                        {
                            sortedLeveMaps.Add(LeveMaps[i2]);
                            sortedLeveNames.Add(LeveNames[i2]);
                            sortedLeveTypes.Add(LeveTypes[i2]);
                            sortedLevePerson.Add(LevePersons[i2]);
                            sortedLevePoints.Add(LevePoints[i2]);
                            tempLeveNames.Remove(LeveNames[i2]);
                            break;
                        }
                    }
                }
            }

            #endregion

            Creatures = sortedCreatureNames;
            Fates = sortedFateNames;
            Leves = sortedLeveNames;

            #region Assign all after sorting

            Creature1 = sortedCreatureNames[0];
            Creature2 = sortedCreatureNames[1];
            Creature3 = sortedCreatureNames[2];
            Creature4 = sortedCreatureNames[3];
            Creature5 = sortedCreatureNames[4];
            Creature6 = sortedCreatureNames[5];
            Creature7 = sortedCreatureNames[6];
            Creature8 = sortedCreatureNames[7];
            Creature9 = sortedCreatureNames[8];
            Creature10 = sortedCreatureNames[9];

            CreatureMap1 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[0])];
            CreatureMap2 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[1])];
            CreatureMap3 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[2])];
            CreatureMap4 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[3])];
            CreatureMap5 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[4])];
            CreatureMap6 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[5])];
            CreatureMap7 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[6])];
            CreatureMap8 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[7])];
            CreatureMap9 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[8])];
            CreatureMap10 = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[9])];

            CreaturePoint1 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[0])];
            CreaturePoint2 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[1])];
            CreaturePoint3 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[2])];
            CreaturePoint4 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[3])];
            CreaturePoint5 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[4])];
            CreaturePoint6 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[5])];
            CreaturePoint7 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[6])];
            CreaturePoint8 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[7])];
            CreaturePoint9 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[8])];
            CreaturePoint10 = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[9])];

            Dungeons.Sort();

            Dungeon1 = Dungeons[0];
            Dungeon2 = Dungeons[1];
            Dungeon3 = Dungeons[2];

            FateName1 = sortedFateNames[0];
            FateName2 = sortedFateNames[1];
            FateName3 = sortedFateNames[2];

            CompletedFate1.Name = sortedFateNames[0];
            CompletedFate2.Name = sortedFateNames[1];
            CompletedFate3.Name = sortedFateNames[2];


            FateMap1 = FateMaps[FateNames.IndexOf(sortedFateNames[0])];
            FateMap2 = FateMaps[FateNames.IndexOf(sortedFateNames[1])];
            FateMap3 = FateMaps[FateNames.IndexOf(sortedFateNames[2])];

            FatePoint1 = FatePoints[FateNames.IndexOf(sortedFateNames[0])];
            FatePoint2 = FatePoints[FateNames.IndexOf(sortedFateNames[1])];
            FatePoint3 = FatePoints[FateNames.IndexOf(sortedFateNames[2])];

            Leve1.Name = sortedLeveNames[0];
            Leve2.Name = sortedLeveNames[1];
            Leve3.Name = sortedLeveNames[2];

            Leve1.Person = LevePersons[LeveNames.IndexOf(sortedLeveNames[0])];
            Leve2.Person = LevePersons[LeveNames.IndexOf(sortedLeveNames[1])];
            Leve3.Person = LevePersons[LeveNames.IndexOf(sortedLeveNames[2])];

            Leve1.Map = LeveMaps[LeveNames.IndexOf(sortedLeveNames[0])];
            Leve2.Map = LeveMaps[LeveNames.IndexOf(sortedLeveNames[1])];
            Leve3.Map = LeveMaps[LeveNames.IndexOf(sortedLeveNames[2])];

            Leve1.PointF = LevePoints[LeveNames.IndexOf(sortedLeveNames[0])];
            Leve2.PointF = LevePoints[LeveNames.IndexOf(sortedLeveNames[1])];
            Leve3.PointF = LevePoints[LeveNames.IndexOf(sortedLeveNames[2])];

            Leve1.Type = LeveTypes[LeveNames.IndexOf(sortedLeveNames[0])];
            Leve2.Type = LeveTypes[LeveNames.IndexOf(sortedLeveNames[1])];
            Leve3.Type = LeveTypes[LeveNames.IndexOf(sortedLeveNames[2])];

            #endregion
        }
        private void ClearMaps()
        {
            PossibleVisibility1 = false;
            PossibleVisibility2 = false;
            PossibleVisibility3 = false;
            PossibleVisibility4 = false;
            PossibleVisibility5 = false;
            PossibleVisibility6 = false;
            PossibleVisibility7 = false;
            PossibleVisibility8 = false;
            PossibleVisibility9 = false;
            PossibleVisibility10 = false;
        }

        // Maybe restructure this when possible
        //  commented out line was preventing adding of map radiobutton visibility when all books were checked and a book was unchecked
        private void InitializeMapLists(string oldBook, string newBook)
        {
            ClearMaps();

            ObservableAllMaps = new List<string>();

            if (!CompletedCreature1) { ObservableAllMaps.Add(CreatureMap1); }
            if (!CompletedCreature2) { ObservableAllMaps.Add(CreatureMap2); }
            if (!CompletedCreature3) { ObservableAllMaps.Add(CreatureMap3); }
            if (!CompletedCreature4) { ObservableAllMaps.Add(CreatureMap4); }
            if (!CompletedCreature5) { ObservableAllMaps.Add(CreatureMap5); }
            if (!CompletedCreature6) { ObservableAllMaps.Add(CreatureMap6); }
            if (!CompletedCreature7) { ObservableAllMaps.Add(CreatureMap7); }
            if (!CompletedCreature8) { ObservableAllMaps.Add(CreatureMap8); }
            if (!CompletedCreature9) { ObservableAllMaps.Add(CreatureMap9); }
            if (!CompletedCreature10) { ObservableAllMaps.Add(CreatureMap10); }

            if (!Leve1.Completed.Bool) { ObservableAllMaps.Add(Leve1.Map); }
            if (!Leve2.Completed.Bool) { ObservableAllMaps.Add(Leve2.Map); }
            if (!Leve3.Completed.Bool) { ObservableAllMaps.Add(Leve3.Map); }

            if (!CompletedFate1.Bool) { ObservableAllMaps.Add(FateMap1); }
            if (!CompletedFate2.Bool) { ObservableAllMaps.Add(FateMap2); }
            if (!CompletedFate3.Bool) { ObservableAllMaps.Add(FateMap3); }

            ObservableUniqueMaps = GetUniqueInList(ObservableAllMaps);

            ObservableUniqueMaps = SortArrMaps(ObservableUniqueMaps);

            int tempcount = ObservableUniqueMaps.Count;

            //if (oldBook != newBook | CurrentBook == null)
            {
                PossibleMap1 = ObservableUniqueMaps[0]; PossibleVisibility1 = true;
                PossibleMap2 = ObservableUniqueMaps[1]; PossibleVisibility2 = true;
                PossibleMap3 = ObservableUniqueMaps[2]; PossibleVisibility3 = true;
                PossibleMap4 = ObservableUniqueMaps[3]; PossibleVisibility4 = true;
                PossibleMap5 = ObservableUniqueMaps[4]; PossibleVisibility5 = true;
                PossibleMap6 = ObservableUniqueMaps[5]; PossibleVisibility6 = true;
                if (tempcount > 6)
                {
                    PossibleMap7 = ObservableUniqueMaps[6]; PossibleVisibility7 = true;
                    if (tempcount > 7)
                    {
                        PossibleMap8 = ObservableUniqueMaps[7]; PossibleVisibility8 = true;
                        if (tempcount > 8)
                        {
                            PossibleMap9 = ObservableUniqueMaps[8]; PossibleVisibility9 = true;
                            if (tempcount > 9)
                            {
                                PossibleMap10 = ObservableUniqueMaps[9]; PossibleVisibility10 = true;
                            }
                        }
                    }
                }
            }

            if (ObservableUniqueMaps.Count > 0)
            { SelectedAnimusMap = ObservableUniqueMaps[0]; }
            else { SelectedAnimusMap = null; }
            UnCheckAnimusRadioButtons();
        }
        private List<string> GetUniqueInList(List<string> inputList)
        {
            List<string> tempList = new List<string>();

            foreach (string item in inputList)
            {
                if (tempList.IndexOf(item) < 0)
                {
                    tempList.Add(item);
                }
            }
            return tempList;
        }
        private void UnCheckAnimusRadioButtons()
        {
            PossibleChecked1 = PossibleMap1 == SelectedAnimusMap;
            PossibleChecked2 = PossibleMap2 == SelectedAnimusMap;
            PossibleChecked3 = PossibleMap3 == SelectedAnimusMap;
            PossibleChecked4 = PossibleMap4 == SelectedAnimusMap;
            PossibleChecked5 = PossibleMap5 == SelectedAnimusMap;
            PossibleChecked6 = PossibleMap6 == SelectedAnimusMap;
            PossibleChecked7 = PossibleMap7 == SelectedAnimusMap;
            PossibleChecked8 = PossibleMap8 == SelectedAnimusMap;
            PossibleChecked9 = PossibleMap9 == SelectedAnimusMap;
            PossibleChecked10 = PossibleMap10 == SelectedAnimusMap;
        }
        private void ReCheckMaps()
        {
            ClearMaps();

            int initialMaps = ObservableUniqueMaps.Count;

            ObservableAllMaps = new List<string>();

            if (!CompletedCreature1) { ObservableAllMaps.Add(CreatureMap1); }
            if (!CompletedCreature2) { ObservableAllMaps.Add(CreatureMap2); }
            if (!CompletedCreature3) { ObservableAllMaps.Add(CreatureMap3); }
            if (!CompletedCreature4) { ObservableAllMaps.Add(CreatureMap4); }
            if (!CompletedCreature5) { ObservableAllMaps.Add(CreatureMap5); }
            if (!CompletedCreature6) { ObservableAllMaps.Add(CreatureMap6); }
            if (!CompletedCreature7) { ObservableAllMaps.Add(CreatureMap7); }
            if (!CompletedCreature8) { ObservableAllMaps.Add(CreatureMap8); }
            if (!CompletedCreature9) { ObservableAllMaps.Add(CreatureMap9); }
            if (!CompletedCreature10) { ObservableAllMaps.Add(CreatureMap10); }

            if (!Leve1.Completed.Bool) { ObservableAllMaps.Add(Leve1.Map); }
            if (!Leve2.Completed.Bool) { ObservableAllMaps.Add(Leve2.Map); }
            if (!Leve3.Completed.Bool) { ObservableAllMaps.Add(Leve3.Map); }

            if (!CompletedFate1.Bool) { ObservableAllMaps.Add(FateMap1); }
            if (!CompletedFate2.Bool) { ObservableAllMaps.Add(FateMap2); }
            if (!CompletedFate3.Bool) { ObservableAllMaps.Add(FateMap3); }

            ObservableUniqueMaps = GetUniqueInList(ObservableAllMaps);

            ObservableUniqueMaps = SortArrMaps(ObservableUniqueMaps);

            int tempcount = ObservableUniqueMaps.Count;

            if (tempcount > 0)
            {
                PossibleMap1 = ObservableUniqueMaps[0]; PossibleVisibility1 = true;
                if (tempcount > 1)
                {
                    PossibleMap2 = ObservableUniqueMaps[1]; PossibleVisibility2 = true;
                    if (tempcount > 2)
                    {
                        PossibleMap3 = ObservableUniqueMaps[2]; PossibleVisibility3 = true;
                        if (tempcount > 3)
                        {
                            PossibleMap4 = ObservableUniqueMaps[3]; PossibleVisibility4 = true;
                            if (tempcount > 4)
                            {
                                PossibleMap5 = ObservableUniqueMaps[4]; PossibleVisibility5 = true;
                                if (tempcount > 5)
                                {
                                    PossibleMap6 = ObservableUniqueMaps[5]; PossibleVisibility6 = true;
                                    if (tempcount > 6)
                                    {
                                        PossibleMap7 = ObservableUniqueMaps[6]; PossibleVisibility7 = true;
                                        if (tempcount > 7)
                                        {
                                            PossibleMap8 = ObservableUniqueMaps[7]; PossibleVisibility8 = true;
                                            if (tempcount > 8)
                                            {
                                                PossibleMap9 = ObservableUniqueMaps[8]; PossibleVisibility9 = true;
                                                if (tempcount > 9)
                                                {
                                                    PossibleMap10 = ObservableUniqueMaps[9]; PossibleVisibility10 = true;

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (!ObservableUniqueMaps.Contains(SelectedAnimusMap))
            {
                if (ObservableUniqueMaps.Count > 0)
                { SelectedAnimusMap = ObservableUniqueMaps[0]; }
                else { SelectedAnimusMap = null; }
                UnCheckAnimusRadioButtons();
            }
            else if (tempcount > initialMaps) { SelectedAnimusMap = ObservableUniqueMaps[0]; }
            ReassignPoints();
        }
        private List<string> SortArrMaps(List<string> inputMap)
        {
            List<string> tempUniqueMaps = new List<string>();

            for (int mapIndex = 10; mapIndex >= 0; mapIndex--)
            {
                if (inputMap.Count - 1 >= mapIndex)
                {
                    if (inputMap[mapIndex].Contains("Noscea"))
                    {
                        tempUniqueMaps.Add(inputMap[mapIndex]);
                        inputMap.RemoveAt(mapIndex);
                    }
                }
            }
            for (int mapIndex = 10; mapIndex >= 0; mapIndex--)
            {
                if (inputMap.Count - 1 >= mapIndex)
                {
                    if (inputMap[mapIndex].Contains("Shroud"))
                    {
                        tempUniqueMaps.Add(inputMap[mapIndex]);
                        inputMap.RemoveAt(mapIndex);
                    }
                }
            }
            for (int mapIndex = 10; mapIndex >= 0; mapIndex--)
            {
                if (inputMap.Count - 1 >= mapIndex)
                {
                    if (inputMap[mapIndex].Contains("Than"))
                    {
                        tempUniqueMaps.Add(inputMap[mapIndex]);
                        inputMap.RemoveAt(mapIndex);
                    }
                }
            }
            for (int mapIndex = 10; mapIndex >= 0; mapIndex--)
            {
                if (inputMap.Count - 1 >= mapIndex)
                {
                    if (inputMap[mapIndex].Contains("Central"))
                    {
                        tempUniqueMaps.Add(inputMap[mapIndex]);
                        inputMap.RemoveAt(mapIndex);
                    }
                }
            }
            for (int mapIndex = 10; mapIndex >= 0; mapIndex--)
            {
                if (inputMap.Count - 1 >= mapIndex)
                {

                    tempUniqueMaps.Add(inputMap[mapIndex]);
                    inputMap.RemoveAt(mapIndex);

                }
            }
            return tempUniqueMaps;
        }
        #endregion

        #region Commands

        #region Complete Animus

        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.AnimusCommand(param),
                        param => this.AnimusCan()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool AnimusCan() { return CurrentAnimus != null; }
        private void AnimusCommand(object param)
        {

            ArrStages arrStages = ArrWeapon.JobList[ArrWeapon.JobListString.IndexOf(CurrentAnimus)];
            arrStages.Animus.Progress = ArrProgress.States.Completed;
            LoadAvailableJobs();
            ResetBools();
            ResetBooks();

        }
        #endregion

            #region Change Displayed Map
        private ICommand _AnimusRadioButton;
        public ICommand AnimusRadioButton
        {
            get
            {
                if (_AnimusRadioButton == null)
                {
                    _AnimusRadioButton = new RelayCommand(
                        param => this.PickMap(param));
                }
                return _AnimusRadioButton;
            }
        }

        private void PickMap(object param)
        {
            string mapSelection = (string)param;

            PossibleChecked1 = PossibleMap1 == mapSelection;
            PossibleChecked2 = PossibleMap2 == mapSelection;
            PossibleChecked3 = PossibleMap3 == mapSelection;
            PossibleChecked4 = PossibleMap4 == mapSelection;
            PossibleChecked5 = PossibleMap5 == mapSelection;
            PossibleChecked6 = PossibleMap6 == mapSelection;
            PossibleChecked7 = PossibleMap7 == mapSelection;
            PossibleChecked8 = PossibleMap8 == mapSelection;
            PossibleChecked9 = PossibleMap9 == mapSelection;
            PossibleChecked10 = PossibleMap10 == mapSelection;

            SelectedAnimusMap = (string)param;
        }

        private ICommand _AnimusMapClick;
        private ArrWeapon arrWeapon;
        private ICommand _CompleteButton;

        public ICommand AnimusMapClick
        {
            get
            {
                if (_AnimusMapClick == null)
                {
                    _AnimusMapClick = new RelayCommand(
                        param => this.ClickMap(param)
                        );
                }
                return _AnimusMapClick;
            }
        }

        private void ClickMap(object input)
        {
            AnimusObject animusObject = (AnimusObject)input;

            string tempName = animusObject.Name;

            foreach(AnimusObject animusObject1 in ObjectList)
            {
                if (tempName == animusObject1.Name)
                {
                    animusObject1.Completed.Bool = true;
                }
            }
            ReassignPoints();

        }

        #endregion

        #endregion
    }
}
