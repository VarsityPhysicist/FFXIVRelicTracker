using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.ARR.Atma;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
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
        private ArrWeapon arrWeapon;
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
                "Book of Skywind II",
                "Book of Skyearth I"
            };

        private List<AnimusObject> LeveList = new List<AnimusObject>();
        private List<AnimusObject> FateList = new List<AnimusObject>();
        private List<AnimusObject> CreatureList = new List<AnimusObject>();

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

        #region ViewModel Properties

        public string Name =>"Animus";
        public AnimusModel AnimusModel
        {
            get { return animusModel; }
            set
            {
                animusModel = value;
                OnPropertyChanged(nameof(AnimusModel));

                InitializeLists();

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
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
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
                
                AnimusModel.selectedAnimusMap = value;
                ReassignPoints();
                if (value == null)
                { 
                    HideLocations();
                    SelectedAnimusImage = null; 
                }
                else { SelectedAnimusImage = ArrInfo.ArrMapImages[value]; }



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
            }
        }

        public string CurrentBook
        {
            get { return animusModel.CurrentBook; }
            set
            {
                if (value != null)
                {
                    

                    if (animusModel.CurrentBook != null)
                    {
                        ResetBools();
                    }


                    
                    animusModel.CurrentBook = value;

                    InitializeMapLists(animusModel.CurrentBook, value);

                    AssignItems();
                    ShowBookItems = true;
                    ReadBooks();

                    OnPropertyChanged(nameof(CurrentBook));

                    ReassignPoints();

                    SelectedAnimusMap = ObservableUniqueMaps[0];
                    ReCheckMaps();
                }
                else
                {
                    HideLocations();

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

        #endregion

        #region Animus Objects

        #region Leves
        public AnimusObject DisplayLeve { get { return animusModel.DisplayLeve; } set { animusModel.DisplayLeve = value; OnPropertyChanged(nameof(DisplayLeve)); } }

        public AnimusObject Leve1 { get { return animusModel.Leve1; } set { animusModel.Leve1 = value; OnPropertyChanged(nameof(Leve1)); } }
        public AnimusObject Leve2 { get { return animusModel.Leve2; } set { animusModel.Leve2 = value; OnPropertyChanged(nameof(Leve2)); } }
        public AnimusObject Leve3 { get { return animusModel.Leve3; } set { animusModel.Leve3 = value; OnPropertyChanged(nameof(Leve3)); } }
        #endregion

        #region Fates
        public AnimusObject DisplayFate { get { return animusModel.DisplayFate; } set { animusModel.DisplayFate = value; OnPropertyChanged(nameof(DisplayFate)); } }

        public AnimusObject Fate1 { get { return animusModel.Fate1; } set { animusModel.Fate1 = value; OnPropertyChanged(nameof(Fate1)); } }
        public AnimusObject Fate2 { get { return animusModel.Fate2; } set { animusModel.Fate2 = value; OnPropertyChanged(nameof(Fate2)); } }
        public AnimusObject Fate3 { get { return animusModel.Fate3; } set { animusModel.Fate3 = value; OnPropertyChanged(nameof(Fate3)); } }
        #endregion

        #region Creatures
        public AnimusObject DisplayCreature1 { get { return animusModel.DisplayCreature1; } set { animusModel.DisplayCreature1 = value; OnPropertyChanged(nameof(DisplayCreature1)); } }
        public AnimusObject DisplayCreature2 { get { return animusModel.DisplayCreature2; } set { animusModel.DisplayCreature2 = value; OnPropertyChanged(nameof(DisplayCreature2)); } }
        public AnimusObject DisplayCreature3 { get { return animusModel.DisplayCreature3; } set { animusModel.DisplayCreature3 = value; OnPropertyChanged(nameof(DisplayCreature3)); } }

        public AnimusObject Creature1 { get { return animusModel.Creature1; } set { animusModel.Creature1 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature1)); } }
        public AnimusObject Creature2 { get { return animusModel.Creature2; } set { animusModel.Creature2 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature2)); } }
        public AnimusObject Creature3 { get { return animusModel.Creature3; } set { animusModel.Creature3 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature3)); } }
        public AnimusObject Creature4 { get { return animusModel.Creature4; } set { animusModel.Creature4 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature4)); } }
        public AnimusObject Creature5 { get { return animusModel.Creature5; } set { animusModel.Creature5 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature5)); } }
        public AnimusObject Creature6 { get { return animusModel.Creature6; } set { animusModel.Creature6 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature6)); } }
        public AnimusObject Creature7 { get { return animusModel.Creature7; } set { animusModel.Creature7 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature7)); } }
        public AnimusObject Creature8 { get { return animusModel.Creature8; } set { animusModel.Creature8 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature8)); } }
        public AnimusObject Creature9 { get { return animusModel.Creature9; } set { animusModel.Creature9 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature9)); } }
        public AnimusObject Creature10 { get { return animusModel.Creature10; } set { animusModel.Creature10 = value; ReCheckMaps(); OnPropertyChanged(nameof(Creature10)); } }
        #endregion

        #region Bools
        public bool CompletedDungeon1 { get { return animusModel.completedDungeon1; } set { animusModel.completedDungeon1 = value; OnPropertyChanged(nameof(CompletedDungeon1)); } }
        public bool CompletedDungeon2 { get { return animusModel.completedDungeon2; } set { animusModel.completedDungeon2 = value; OnPropertyChanged(nameof(CompletedDungeon2)); } }
        public bool CompletedDungeon3 { get { return animusModel.completedDungeon3; } set { animusModel.completedDungeon3 = value; OnPropertyChanged(nameof(CompletedDungeon3)); } }

        public bool CreatureBool1 { get { return animusModel.Creature1.Completed.Bool; } set { animusModel.Creature1.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool1)); } }
        public bool CreatureBool2 { get { return Creature2.Completed.Bool; } set { Creature2.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool2)); } }
        public bool CreatureBool3 { get { return Creature3.Completed.Bool; } set { Creature3.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool3)); } }
        public bool CreatureBool4 { get { return Creature4.Completed.Bool; } set { Creature4.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool4)); } }
        public bool CreatureBool5 { get { return Creature5.Completed.Bool; } set { Creature5.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool5)); } }
        public bool CreatureBool6 { get { return Creature6.Completed.Bool; } set { Creature6.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool6)); } }
        public bool CreatureBool7 { get { return Creature7.Completed.Bool; } set { Creature7.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool7)); } }
        public bool CreatureBool8 { get { return Creature8.Completed.Bool; } set { Creature8.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool8)); } }
        public bool CreatureBool9 { get { return Creature9.Completed.Bool; } set { Creature9.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool9)); } }
        public bool CreatureBool10 { get { return Creature10.Completed.Bool; } set { Creature10.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(CreatureBool10)); } }

        public bool FateBool1 { get { return Fate1.Completed.Bool; } set { Fate1.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(FateBool1)); } }
        public bool FateBool2 { get { return Fate2.Completed.Bool; } set { Fate2.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(FateBool2)); } }
        public bool FateBool3 { get { return Fate3.Completed.Bool; } set { Fate3.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(FateBool3)); } }

        public bool LeveBool1 { get { return Leve1.Completed.Bool; } set { Leve1.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(LeveBool1)); } }
        public bool LeveBool2 { get { return Leve2.Completed.Bool; } set { Leve2.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(LeveBool2)); } }
        public bool LeveBool3 { get { return Leve3.Completed.Bool; } set { Leve3.Completed.Bool = value; ReCheckMaps(); OnPropertyChanged(nameof(LeveBool3)); } }


        #endregion
        #endregion


        #region Book Information

        public bool SkyFire1Book { get { return animusModel.skyFire1Book; } set { animusModel.skyFire1Book = value; ChangedBookStatus(nameof(SkyFire1Book), value); OnPropertyChanged(nameof(SkyFire1Book)); } }
        public bool SkyFire2Book { get { return animusModel.skyFire2Book; } set { animusModel.skyFire2Book = value; ChangedBookStatus(nameof(SkyFire2Book), value); OnPropertyChanged(nameof(SkyFire2Book)); } }
        public bool SkyFall1Book { get { return animusModel.skyFall1Book; } set { animusModel.skyFall1Book = value; ChangedBookStatus(nameof(SkyFall1Book), value); OnPropertyChanged(nameof(SkyFall1Book)); } }
        public bool SkyFall2Book { get { return animusModel.skyFall2Book; } set { animusModel.skyFall2Book = value; ChangedBookStatus(nameof(SkyFall2Book), value); OnPropertyChanged(nameof(SkyFall2Book)); } }
        public bool NetherFire1Book { get { return animusModel.netherFire1Book; } set { animusModel.netherFire1Book = value; ChangedBookStatus(nameof(NetherFire1Book), value); OnPropertyChanged(nameof(NetherFire1Book)); } }
        public bool NetherFall1Book { get { return animusModel.netherFall1Book; } set { animusModel.netherFall1Book = value; ChangedBookStatus(nameof(NetherFall1Book), value); OnPropertyChanged(nameof(NetherFall1Book)); } }
        public bool SkyWind1Book { get { return animusModel.skyWind1Book; } set { animusModel.skyWind1Book = value; ChangedBookStatus(nameof(SkyWind1Book), value); OnPropertyChanged(nameof(SkyWind1Book)); } }
        public bool SkyWind2Book { get { return animusModel.skyWind2Book; } set { animusModel.skyWind2Book = value; ChangedBookStatus(nameof(SkyWind2Book), value); OnPropertyChanged(nameof(SkyWind2Book)); } }
        public bool SkyEarth1Book { get { return animusModel.skyEarth1Book; } set { animusModel.skyEarth1Book = value; ChangedBookStatus(nameof(SkyEarth1Book), value); OnPropertyChanged(nameof(SkyEarth1Book)); } }

        private List<bool> bookBools = new List<bool>();

        public List<string> Leves { get { return animusModel.leves; } set { animusModel.leves = value; OnPropertyChanged(nameof(Leves)); } }
        public List<string> Fates { get { return animusModel.fates; } set { animusModel.fates = value; OnPropertyChanged(nameof(Fates)); } }
        public List<string> Creatures { get { return animusModel.creatures; } set { animusModel.creatures = value; OnPropertyChanged(nameof(Creatures)); } }

        public bool ShowBookItems { get { return animusModel.showBookItems; } set { animusModel.showBookItems = value; OnPropertyChanged(nameof(ShowBookItems)); } }
        public List<string> ObservableUniqueMaps { get { return animusModel.observableUniqueMaps; } set { animusModel.observableUniqueMaps = value; OnPropertyChanged(nameof(ObservableUniqueMaps)); } }
        public List<string> ObservableAllMaps { get { return animusModel.observableAllMaps; } set { animusModel.observableAllMaps = value; OnPropertyChanged(nameof(ObservableAllMaps)); } }




        #region RadioButton Properties

        private List<bool> CheckedButtonList;
        private List<bool> VisibilityButtonList;
        private List<string> MapButtonList;

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

        #endregion
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

        public string Dungeon1 { get { return animusModel.dungeon1; } set { animusModel.dungeon1 = value; OnPropertyChanged(nameof(Dungeon1)); } }
        public string Dungeon2 { get { return animusModel.dungeon2; } set { animusModel.dungeon2 = value; OnPropertyChanged(nameof(Dungeon2)); } }
        public string Dungeon3 { get { return animusModel.dungeon3; } set { animusModel.dungeon3 = value; OnPropertyChanged(nameof(Dungeon3)); } }

        #endregion
        #endregion

        #region Methods
        private void ReadBooks()
        {
            int bookInt = ArrInfo.ReferenceBooks.IndexOf(CurrentBook);

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
        }
        private void InitializeLists()
        {

            LeveList = new List<AnimusObject>
                {
                    Leve1, Leve2, Leve3
                };

            FateList = new List<AnimusObject>
                {
                    Fate1, Fate2, Fate3
                };

            CreatureList = new List<AnimusObject>
                {
                    Creature1, Creature2, Creature3, Creature4, Creature5,
                    Creature6, Creature7, Creature8, Creature9, Creature10
                };

            ObjectList = new List<AnimusObject>
                {
                    Leve1, Leve2, Leve3,
                    Fate1, Fate2, Fate3,

                    Creature1, Creature2, Creature3, Creature4, Creature5,
                    Creature6, Creature7, Creature8, Creature9, Creature10
                };

            CheckedButtonList = new List<bool>
                {
                    PossibleChecked1,
                    PossibleChecked2,
                    PossibleChecked3,
                    PossibleChecked4,
                    PossibleChecked5,
                    PossibleChecked6,
                    PossibleChecked7,
                    PossibleChecked8,
                    PossibleChecked9,
                    PossibleChecked10
                };

            VisibilityButtonList = new List<bool>
                {
                    PossibleVisibility1,
                    PossibleVisibility2,
                    PossibleVisibility3,
                    PossibleVisibility4,
                    PossibleVisibility5,
                    PossibleVisibility6,
                    PossibleVisibility7,
                    PossibleVisibility8,
                    PossibleVisibility9,
                    PossibleVisibility10
                };

            MapButtonList = new List<string>
                {
                    PossibleMap1,
                    PossibleMap2,
                    PossibleMap3,
                    PossibleMap4,
                    PossibleMap5,
                    PossibleMap6,
                    PossibleMap7,
                    PossibleMap8,
                    PossibleMap9,
                    PossibleMap10
                };
        }

        private void HideLocations()
        {
            DisplayCreature1.Completed.Bool = true;
            DisplayCreature2.Completed.Bool = true;
            DisplayCreature3.Completed.Bool = true;

            DisplayFate.Completed.Bool = true;
            DisplayLeve.Completed.Bool = true;
        }

        private void ResetBooks()
        {
            SkyFire1Book = false;
            SkyFire2Book = false;
            SkyFall1Book = false;
            SkyFall2Book = false;
            NetherFire1Book = false;
            NetherFall1Book = false;
            SkyWind1Book = false;
            SkyWind2Book = false;
            SkyEarth1Book = false;
        }

        private void ResetBools()
        {
            ClearMaps();

            CompletedDungeon1  = false;
            CompletedDungeon2  = false;
            CompletedDungeon3 = false;

            CreatureBool1  = false;
            CreatureBool2  = false;
            CreatureBool3  = false;
            CreatureBool4  = false;
            CreatureBool5  = false;
            CreatureBool6  = false;
            CreatureBool7  = false;
            CreatureBool8  = false;
            CreatureBool9  = false;
            CreatureBool10 = false;

            FateBool1  = false;
            FateBool2  = false;
            FateBool3 = false;

            LeveBool1  = false;
            LeveBool2  = false;
            LeveBool3 = false;

        }
        private void ReCheckBools()
        {
            //For some reason, checkboxes IsChecked is not being triggered by clicking items on the map, but the strikethrough on the completion
            //  list is working. Resending this event will announce if a checkbox needs to be checked

            OnPropertyChanged(nameof(CreatureBool1));
            OnPropertyChanged(nameof(CreatureBool2));
            OnPropertyChanged(nameof(CreatureBool3));
            OnPropertyChanged(nameof(CreatureBool4));
            OnPropertyChanged(nameof(CreatureBool5));
            OnPropertyChanged(nameof(CreatureBool6));
            OnPropertyChanged(nameof(CreatureBool7));
            OnPropertyChanged(nameof(CreatureBool8));
            OnPropertyChanged(nameof(CreatureBool9));
            OnPropertyChanged(nameof(CreatureBool10));

            OnPropertyChanged(nameof(FateBool1));
            OnPropertyChanged(nameof(FateBool2));
            OnPropertyChanged(nameof(FateBool3));

            OnPropertyChanged(nameof(LeveBool1));
            OnPropertyChanged(nameof(LeveBool2));
            OnPropertyChanged(nameof(LeveBool3));

            OnPropertyChanged(nameof(CompletedDungeon1));
            OnPropertyChanged(nameof(CompletedDungeon2));
            OnPropertyChanged(nameof(CompletedDungeon3));


        }

        public void LoadAvailableJobs()
        {
            ReadBooks();

            if (AvailableAnimusJobs == null) { AvailableAnimusJobs = new ObservableCollection<string>(); }
            foreach(ArrJobs job in ArrWeapon.JobList)
            {
                if ( job.Animus.Progress != ArrProgress.States.Completed & !AvailableAnimusJobs.Contains(job.Name))
                {
                    AvailableAnimusJobs.Add(job.Name);
                }
                if (job.Animus.Progress == ArrProgress.States.Completed & AvailableAnimusJobs.Contains(job.Name))
                {
                    AvailableAnimusJobs.Remove(job.Name);
                }
            }
                RecheckAvailableBooks();
            //Refreshes view for people with older versions that may have had a bug resolved
            CurrentBook = CurrentBook;
        }

        public void ReassignPoints()
        {

            DisplayCreature1 = new AnimusObject(true);
            DisplayCreature2 = new AnimusObject(true);
            DisplayCreature3 = new AnimusObject(true);

            DisplayLeve = new AnimusObject(true);

            DisplayFate = new AnimusObject(true);

            #region Set Fates

            foreach (AnimusObject fate in FateList)
            {
                if (fate.Map == SelectedAnimusMap)
                {
                    DisplayFate = fate;
                }
            }

            #endregion
            #region Set Leves

            foreach(AnimusObject leve in LeveList)
            {
                if(leve.Map== SelectedAnimusMap)
                {
                    DisplayLeve = leve;
                }
            }

            #endregion
            #region Set Creatures

            foreach (AnimusObject creature in CreatureList)
            {
                if (creature.Map == SelectedAnimusMap)
                {
                    if (DisplayCreature1.Name == null) {DisplayCreature1 = creature; }
                    else if(DisplayCreature2.Name == null) { DisplayCreature2 = creature; }
                    else if(DisplayCreature3.Name == null) { DisplayCreature3 = creature; }
                    else { throw new Exception(); }
                }
            }
            AssignItems();

            #endregion
        }

        private void RecheckAvailableBooks()
        {
            bookBools = new List<bool>()
            {
                SkyFire1Book,
                SkyFire2Book,
                SkyFall1Book,
                SkyFall2Book,
                NetherFire1Book,
                NetherFall1Book,
                SkyWind1Book,
                SkyWind2Book,
                SkyEarth1Book
            };
            foreach (string book in ArrInfo.ReferenceBooks)
            {
                int bookOrder = ArrInfo.ReferenceBooks.IndexOf(book);
                bool tempBool = bookBools[bookOrder];

                int compareInt;

                if(tempBool & AnimusBooks.Contains(book))
                {
                    AnimusBooks.Remove(book);
                }
                else if(!tempBool & !AnimusBooks.Contains(book))
                {
                    switch (AnimusBooks.Count)
                    {
                        case 0:
                            AnimusBooks.Add(book);
                            break;
                        default:
                            for(int i=0; i < AnimusBooks.Count; i++)
                            {
                                compareInt = ArrInfo.ReferenceBooks.IndexOf(AnimusBooks[i]);
                                if (compareInt > bookOrder) 
                                { 
                                    AnimusBooks.Insert(i, book); 
                                    break;
                                }
                            }
                            if (!AnimusBooks.Contains(book)) { AnimusBooks.Add(book); }
                            break;
                    }
                }
                
            }
        }

        private void ChangedBookStatus(string book, bool newStatus)
        {
            int bookindex = ArrInfo.BookList.IndexOf(book);
            string bookString = ArrInfo.ReferenceBooks[bookindex];

            if(CurrentBook== bookString & newStatus) { ResetBools(); }

            RecheckAvailableBooks();
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

            Creature1.Name = sortedCreatureNames[0];
            Creature2.Name = sortedCreatureNames[1];
            Creature3.Name = sortedCreatureNames[2];
            Creature4.Name = sortedCreatureNames[3];
            Creature5.Name = sortedCreatureNames[4];
            Creature6.Name = sortedCreatureNames[5];
            Creature7.Name = sortedCreatureNames[6];
            Creature8.Name = sortedCreatureNames[7];
            Creature9.Name = sortedCreatureNames[8];
            Creature10.Name = sortedCreatureNames[9];

            Creature1.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[0])];
            Creature2.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[1])];
            Creature3.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[2])];
            Creature4.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[3])];
            Creature5.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[4])];
            Creature6.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[5])];
            Creature7.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[6])];
            Creature8.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[7])];
            Creature9.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[8])];
            Creature10.Map = CreatureMaps[CreatureNames.IndexOf(sortedCreatureNames[9])];

            Creature1.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[0])];
            Creature2.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[1])];
            Creature3.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[2])];
            Creature4.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[3])];
            Creature5.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[4])];
            Creature6.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[5])];
            Creature7.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[6])];
            Creature8.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[7])];
            Creature9.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[8])];
            Creature10.PointF = CreaturePoints[CreatureNames.IndexOf(sortedCreatureNames[9])];

            Dungeons.Sort();

            Dungeon1 = Dungeons[0];
            Dungeon2 = Dungeons[1];
            Dungeon3 = Dungeons[2];

            Fate1.Name = sortedFateNames[0];
            Fate2.Name = sortedFateNames[1];
            Fate3.Name = sortedFateNames[2];

            Fate1.Map = FateMaps[FateNames.IndexOf(sortedFateNames[0])];
            Fate2.Map = FateMaps[FateNames.IndexOf(sortedFateNames[1])];
            Fate3.Map = FateMaps[FateNames.IndexOf(sortedFateNames[2])];

            Fate1.PointF = FatePoints[FateNames.IndexOf(sortedFateNames[0])];
            Fate2.PointF = FatePoints[FateNames.IndexOf(sortedFateNames[1])];
            Fate3.PointF = FatePoints[FateNames.IndexOf(sortedFateNames[2])];


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

            foreach(AnimusObject obj in ObjectList)
            {
                if (!obj.Completed.Bool)
                {
                    ObservableAllMaps.Add(obj.Map);
                }
            }

            ObservableUniqueMaps = GetUniqueInList(ObservableAllMaps);

            ObservableUniqueMaps = SortArrMaps(ObservableUniqueMaps);

            int tempcount = ObservableUniqueMaps.Count;

            //if (oldBook != newBook | CurrentBook == null)
            {

                //only works if map visibilities are store in a reference object
                for(int i = 0; i < tempcount; i++)
                {
                    MapButtonList[i] = ObservableUniqueMaps[i];
                    VisibilityButtonList[i] = true;
                }

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

            foreach (AnimusObject obj in ObjectList)
            {
                if (!obj.Completed.Bool)
                {
                    ObservableAllMaps.Add(obj.Map);
                }
            }

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

            else if (tempcount > initialMaps)
            {
                if (SelectedAnimusMap != null)
                {
                    AssignRadioButtons();
                }
                else { SelectedAnimusMap = ObservableUniqueMaps[0];  }
               
            }
            else { AssignRadioButtons(); }
            if (SelectedAnimusMap != null)
            {
                ReassignPoints();
            }
            
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

        private void AssignRadioButtons()
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
                        param => this.AnimusCommand(),
                        param => this.AnimusCan()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool AnimusCan() { return CurrentAnimus != null; }
        private void AnimusCommand()
        {

            ArrJobs tempJob = ArrWeapon.JobList[ArrInfo.JobListString.IndexOf(CurrentAnimus)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Animus,true);


            LoadAvailableJobs();
            ResetBools();
            ResetBooks();

        }
        #endregion

        #region Checkbox Command
        private ICommand _CompleteButton;

        public ICommand CheckboxButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CheckboxCommand(),
                        param => this.CheckboxCan()
                        );
                }
                return _CompleteButton;
            }
        }
        private bool CheckboxCan() { return true; }

        private void CheckboxCommand()
        {
            ReCheckMaps();
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
            SelectedAnimusMap = (string)param;
            AssignRadioButtons();
        }

        #endregion

        #region Click Map

        private ICommand _AnimusMapClick;

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
            ReCheckMaps();
            if (SelectedAnimusImage != null) {ReassignPoints(); }

            ReCheckBools();



        }

        #endregion

        #endregion
    }
}
