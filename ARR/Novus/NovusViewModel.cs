using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Novus
{
    class NovusViewModel : ObservableObject, IPageViewModel
    {
        private IEventAggregator iEventAggregator;
        private Character selectedCharacter;
        private NovusModel novusModel;
        private ArrWeapon arrWeapon;

        public NovusViewModel(IEventAggregator iEventAggregator)
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

        public string Name => "Novus";

        #region Properties

        #region Character Properties
        public NovusModel NovusModel
        {
            get { return novusModel; }
            set
            {
                novusModel = value;
                OnPropertyChanged(nameof(NovusModel));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    NovusModel = selectedCharacter.ArrProgress.NovusModel;
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
                }
            }
        }
        private List<string> knownWeapons=new List<string>
        {
            "PLD","DRG"
        };
        public ArrWeapon ArrWeapon
        {
            get { return arrWeapon; }
            set
            {
                if (value != null)
                {

                    arrWeapon = value;
                    LoadAvailableJobs();
                    OnPropertyChanged(nameof(ArrWeapon));
                }
            }
        }
        public ObservableCollection<string> AvailableNovusJobs
        {
            get { return availableNovusJobs; }
            set
            {
                availableNovusJobs = value;
                OnPropertyChanged(nameof(AvailableNovusJobs));
            }
        }

        public bool PLDNovus
        {
            get { return novusModel.PLDNovus; }
            set
            {
                novusModel.PLDNovus = value;
                OnPropertyChanged(nameof(PLDNovus));
            }
        }

        public bool NonPLDNovus
        {
            get { return novusModel.NonPLDNovus; }
            set
            {
                novusModel.NonPLDNovus = value;
                OnPropertyChanged(nameof(NonPLDNovus));
            }
        }

        public string CurrentNovus
        {
            get { return novusModel.CurrentNovus; }
            set
            {
                novusModel.CurrentNovus = value;
                OnPropertyChanged(nameof(CurrentNovus));

                ShowContent = false;

                if (value != null)
                {
                    if (value == "PLD")
                    {
                        ShowContent = false;
                        PLDNovus = true;
                        NonPLDNovus = false;
                    }
                    else
                    {
                        if (knownWeapons.Contains(value)) { KnownWeapon = true; }
                        WeaponName = ArrInfo.WeaponNames[value];
                        
                        ShowContent = true;
                        PLDNovus = false;
                        NonPLDNovus = true;
                    }
                    GetMateriaMaxCounts(value);
                }
            }
        }



        #endregion

        #region Model Properties
        #region Materia Counts

        #region PLD Counts
        #region Sword
        public int MateriaSwordSum
        {
            get { return novusModel.MateriaSwordSum; }
            set
            {
                if (value >= 0 & value <= 53)
                {
                    novusModel.MateriaSwordSum = value;
                    OnPropertyChanged(nameof(MateriaSwordSum));
                }
            }
        }
        public int HeavenEyeSwordCount
        {
            get { return novusModel.HeavenEyeSwordCount; }
            set
            {
                if (value >= 0 & value <= HeavenEyeSwordMax)
                {
                    novusModel.HeavenEyeSwordCount = value;
                    OnPropertyChanged(nameof(HeavenEyeSwordCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuickarmSwordCount
        {
            get { return novusModel.QuickarmSwordCount; }
            set
            {
                if (value >= 0 & value <= QuickarmSwordMax)
                {
                    novusModel.QuickarmSwordCount = value;
                    OnPropertyChanged(nameof(QuickarmSwordCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int SavageAimSwordCount
        {
            get { return novusModel.SavageAimSwordCount; }
            set
            {
                if (value >= 0 & value <= SavageAimSwordMax)
                {
                    novusModel.SavageAimSwordCount = value;
                    OnPropertyChanged(nameof(SavageAimSwordCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int PietySwordCount
        {
            get { return novusModel.PietySwordCount; }
            set
            {
                if (value >= 0 & value <= PietySwordMax)
                {
                    novusModel.PietySwordCount = value;
                    OnPropertyChanged(nameof(PietySwordCount));
                    CalculateMateriaSum();
                }

            }
        }
        public int SavageMightSwordCount
        {
            get { return novusModel.SavageMightSwordCount; }
            set
            {
                if (value >= 0 & value <= SavageMightSwordMax)
                {
                    novusModel.SavageMightSwordCount = value;
                    OnPropertyChanged(nameof(SavageMightSwordCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuicktongueSwordCount
        {
            get { return novusModel.QuicktongueSwordCount; }
            set
            {
                if (value >= 0 & value <= QuicktongueSwordMax)
                {
                    novusModel.QuicktongueSwordCount = value;
                    OnPropertyChanged(nameof(QuicktongueSwordCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int BattledanceSwordCount
        {
            get { return novusModel.BattledanceSwordCount; }
            set
            {
                if (value >= 0 & value <= BattledanceSwordMax)
                {
                    novusModel.BattledanceSwordCount = value;
                    OnPropertyChanged(nameof(BattledanceSwordCount));
                    CalculateMateriaSum();

                }

            }
        }
        #region Materia Max
        public int HeavenEyeSwordMax
        {
            get { return 31; }
        }

        public int QuickarmSwordMax
        {
            get { return 31; }
        }

        public int SavageAimSwordMax
        {
            get { return 31; }
        }

        public int PietySwordMax
        {
            get { return 17; }
        }
        public int SavageMightSwordMax
        {
            get { return 22; }
        }

        public int QuicktongueSwordMax
        {
            get { return 31; }
        }

        public int BattledanceSwordMax
        {
            get { return 31; }

        }
        #endregion
        #endregion

        #region Shield
        public int MateriaShieldSum
        {
            get { return novusModel.MateriaShieldSum; }
            set
            {
                if (value >= 0 & value <= 22)
                {
                    novusModel.MateriaShieldSum = value;
                    OnPropertyChanged(nameof(MateriaShieldSum));
                }
            }
        }
        public int HeavenEyeShieldCount
        {
            get { return novusModel.HeavenEyeShieldCount; }
            set
            {
                if (value >= 0 & value <= HeavenEyeShieldMax)
                {
                    novusModel.HeavenEyeShieldCount = value;
                    OnPropertyChanged(nameof(HeavenEyeShieldCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuickarmShieldCount
        {
            get { return novusModel.QuickarmShieldCount; }
            set
            {
                if (value >= 0 & value <= QuickarmShieldMax)
                {
                    novusModel.QuickarmShieldCount = value;
                    OnPropertyChanged(nameof(QuickarmShieldCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int SavageAimShieldCount
        {
            get { return novusModel.SavageAimShieldCount; }
            set
            {
                if (value >= 0 & value <= SavageAimShieldMax)
                {
                    novusModel.SavageAimShieldCount = value;
                    OnPropertyChanged(nameof(SavageAimShieldCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int PietyShieldCount
        {
            get { return novusModel.PietyShieldCount; }
            set
            {
                if (value >= 0 & value <= PietyShieldMax)
                {
                    novusModel.PietyShieldCount = value;
                    OnPropertyChanged(nameof(PietyShieldCount));
                    CalculateMateriaSum();
                }

            }
        }
        public int SavageMightShieldCount
        {
            get { return novusModel.SavageMightShieldCount; }
            set
            {
                if (value >= 0 & value <= SavageMightShieldMax)
                {
                    novusModel.SavageMightShieldCount = value;
                    OnPropertyChanged(nameof(SavageMightShieldCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuicktongueShieldCount
        {
            get { return novusModel.QuicktongueShieldCount; }
            set
            {
                if (value >= 0 & value <= QuicktongueShieldMax)
                {
                    novusModel.QuicktongueShieldCount = value;
                    OnPropertyChanged(nameof(QuicktongueShieldCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int BattledanceShieldCount
        {
            get { return novusModel.BattledanceShieldCount; }
            set
            {
                if (value >= 0 & value <= BattledanceShieldMax)
                {
                    novusModel.BattledanceShieldCount = value;
                    OnPropertyChanged(nameof(BattledanceShieldCount));
                    CalculateMateriaSum();

                }

            }
        }
        #region Materia Max
        public int HeavenEyeShieldMax
        {
            get { return 13; }
        }

        public int QuickarmShieldMax
        {
            get { return 13; }
        }

        public int SavageAimShieldMax
        {
            get { return 13; }
        }

        public int PietyShieldMax
        {
            get { return 7; }
        }
        public int SavageMightShieldMax
        {
            get { return 9; }
        }

        public int QuicktongueShieldMax
        {
            get { return 13; }

        }

        public int BattledanceShieldMax
        {
            get { return 13; }

        }
        #endregion
        #endregion

        #endregion

        public int MateriaSum
        {
            get { return novusModel.MateriaSum;}
            set
            {
                if(value>=0 & value <= 75)
                {
                    novusModel.MateriaSum = value;
                    OnPropertyChanged(nameof(MateriaSum));
                }
            }
        }
        public int HeavenEyeCount
        {
            get { return novusModel.HeavenEyeCount; }
            set
            {
                if (value >= 0 & value <= HeavenEyeMax)
                {
                    novusModel.HeavenEyeCount = value;
                    OnPropertyChanged(nameof(HeavenEyeCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuickarmCount
        {
            get { return novusModel.QuickarmCount; }
            set
            {
                if (value >= 0 & value <= QuickarmMax)
                {
                    novusModel.QuickarmCount = value;
                    OnPropertyChanged(nameof(QuickarmCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int SavageAimCount
        {
            get { return novusModel.SavageAimCount; }
            set
            {
                if (value >= 0 & value <= SavageAimMax)
                {
                    novusModel.SavageAimCount = value;
                    OnPropertyChanged(nameof(SavageAimCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int PietyCount
        {
            get { return novusModel.PietyCount; }
            set
            {
                if (value >= 0 & value <= PietyMax)
                {
                    novusModel.PietyCount = value;
                    OnPropertyChanged(nameof(PietyCount));
                    CalculateMateriaSum();
                }

            }
        }
        public int SavageMightCount
        {
            get { return novusModel.SavageMightCount; }
            set
            {
                if (value >= 0 & value <= SavageMightMax)
                {
                    novusModel.SavageMightCount = value;
                    OnPropertyChanged(nameof(SavageMightCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuicktongueCount
        {
            get { return novusModel.QuicktongueCount; }
            set
            {
                if (value >= 0 & value <= QuicktongueMax)
                {
                    novusModel.QuicktongueCount = value;
                    OnPropertyChanged(nameof(QuicktongueCount));
                    CalculateMateriaSum();
                }

            }
        }

        public int BattledanceCount
        {
            get { return novusModel.BattledanceCount; }
            set
            {
                if (value >= 0 & value <= BattledanceMax)
                {
                    novusModel.BattledanceCount = value;
                    OnPropertyChanged(nameof(BattledanceCount));
                    CalculateMateriaSum();

                }

            }
        }

        #region Materia Max
        public int HeavenEyeMax
        {
            get { return novusModel.HeavenEyeMax; }
            set
            {
                if (value >= 0 & value <= 44)
                {
                    novusModel.HeavenEyeMax = value;
                    OnPropertyChanged(nameof(HeavenEyeMax));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuickarmMax
        {
            get { return novusModel.QuickarmMax; }
            set
            {
                if (value >= 0 & value <= 44)
                {
                    novusModel.QuickarmMax = value;
                    OnPropertyChanged(nameof(QuickarmMax));
                    CalculateMateriaSum();
                }

            }
        }

        public int SavageAimMax
        {
            get { return novusModel.SavageAimMax; }
            set
            {
                if (value >= 0 & value <= 44)
                {
                    novusModel.SavageAimMax = value;
                    OnPropertyChanged(nameof(SavageAimMax));
                    CalculateMateriaSum();
                }

            }
        }

        public int PietyMax
        {
            get { return novusModel.PietyMax; }
            set
            {
                if (value >= 0 & value <= 23)
                {
                    novusModel.PietyMax = value;
                    OnPropertyChanged(nameof(PietyMax));
                    CalculateMateriaSum();
                }

            }
        }
        public int SavageMightMax
        {
            get { return novusModel.SavageMightMax; }
            set
            {
                if (value >= 0 & value <= 31)
                {
                    novusModel.SavageMightMax = value;
                    OnPropertyChanged(nameof(SavageMightMax));
                    CalculateMateriaSum();
                }

            }
        }

        public int QuicktongueMax
        {
            get { return novusModel.QuicktongueMax; }
            set
            {
                if (value >= 0 & value <= 44)
                {
                    novusModel.QuicktongueMax = value;
                    OnPropertyChanged(nameof(QuicktongueMax));
                    CalculateMateriaSum();
                }

            }
        }

        public int BattledanceMax
        {
            get { return novusModel.BattledanceMax; }
            set
            {
                if (value >= 0 & value <= 44)
                {
                    novusModel.BattledanceMax = value;
                    OnPropertyChanged(nameof(BattledanceMax));
                    CalculateMateriaSum();

                }

            }
        }
        #endregion

        #endregion
        #endregion

        public string WeaponName
        {
            get { return novusModel.WeaponName; }
            set
            {
                novusModel.WeaponName = value;
                OnPropertyChanged(nameof(WeaponName));
            }
        }

        public bool KnownWeapon
        {
            get { return novusModel.KnownWeapon; }
            set
            {
                novusModel.KnownWeapon = value;
                OnPropertyChanged(nameof(KnownWeapon));
            }
        }

        public bool ShowContent
        {
            get { return novusModel.ShowContent; }
            set
            {
                novusModel.ShowContent = value;
                OnPropertyChanged(nameof(ShowContent));
            }
        }

        #endregion

        #region Methods

        private void GetMateriaMaxCounts (string SelectedJob)
        {
            Tuple<int, int, int, int, int, int, int> tuple= ArrInfo.MateriaCaps[SelectedJob];

            HeavenEyeMax = tuple.Item1;
            QuickarmMax = tuple.Item2;
            SavageAimMax = tuple.Item3;
            PietyMax = tuple.Item4;
            SavageMightMax = tuple.Item5;
            QuicktongueMax = tuple.Item6;
            BattledanceMax = tuple.Item7;
        }

        private void CalculateMateriaSum()
        {
            if (NonPLDNovus)
            {
                MateriaSum = HeavenEyeCount + QuickarmCount + SavageAimCount + PietyCount + SavageMightCount + QuicktongueCount + BattledanceCount;
            }
            else if (NonPLDNovus)
            {
                MateriaSwordSum = HeavenEyeSwordCount + QuickarmSwordCount + SavageAimSwordCount + PietySwordCount 
                    + SavageMightSwordCount + QuicktongueSwordCount + BattledanceSwordCount;

                MateriaShieldSum = HeavenEyeShieldCount + QuickarmShieldCount + SavageAimShieldCount + PietyShieldCount 
                    + SavageMightShieldCount + QuicktongueShieldCount + BattledanceShieldCount;
            }
        }

        private void ResetCounts()
        {
            HeavenEyeCount = 0;
            QuickarmCount = 0;
            SavageAimCount = 0;
            PietyCount = 0;
            SavageMightCount = 0;
            QuicktongueCount = 0;
            BattledanceCount = 0;
        }

        private void LoadAvailableJobs()
        {
            AvailableNovusJobs = new ObservableCollection<string>();
            {

                foreach (ArrJobs job in ArrWeapon.JobList)
                {
                    if (job.Novus.Progress != ArrProgress.States.Completed)
                    {
                        AvailableNovusJobs.Add(job.Name);
                    }
                }
            }
        }

        #endregion

        #region Commands
        private ICommand _CompleteButton;
        private ICommand _IncrementButton;
        private ICommand _DecrementButton;
        private ObservableCollection<string> availableNovusJobs;
        public ICommand IncrementButton
        {
            get
            {
                if (_IncrementButton == null)
                {
                    _IncrementButton = new RelayCommand(
                        param => this.IncrementCommand(param,true)                        
                        );
                }
                return _IncrementButton;
            }
        }
        public ICommand DencrementButton
        {
            get
            {
                if (_DecrementButton == null)
                {
                    _DecrementButton = new RelayCommand(
                        param => this.IncrementCommand(param, false));
                }
                return _DecrementButton;
            }
        }
        private void IncrementCommand(object param, bool add)
        {
            string sum = (string)param;

            Type classType = typeof(NovusViewModel);

            PropertyInfo CommandTarget = classType.GetProperty(sum);

            switch (add)
            {
                case true:
                    CommandTarget.SetValue(this, (int)CommandTarget.GetValue(this) + 1);
                    break;
                case false:
                    CommandTarget.SetValue(this, (int)CommandTarget.GetValue(this) - 1);
                    break;
            }
        }

        #region Complete Button
        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CompleteCommand(),
                        param => this.CanComplete()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CanComplete() { return CurrentNovus != null; }
        private void CompleteCommand()
        {

            ArrJobs tempJob = ArrWeapon.JobList[ArrInfo.JobListString.IndexOf(CurrentNovus)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Novus, true);

            ResetCounts();
            LoadAvailableJobs();
            PLDNovus = false;
            NonPLDNovus = false;

        }
        #endregion
        #endregion
    }
}
