using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._08_Lux
{
    public class LuxViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private LuxModel luxModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public LuxViewModel()
        {
        }
        public LuxViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            SubscriptionToken subscriptionToken =
                        this
                            .eventAggregator
                            .GetEvent<PubSubEvent<Character>>()
                            .Subscribe((details) =>
                            {
                                this.SelectedCharacter = details;
                            });

        }
        #endregion

        #region Properties
        public string Name => "Lux";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    luxModel = selectedCharacter.HWModel.LuxModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return luxModel.SelectedJob; }
            set
            {
                if (value != null & value != luxModel.SelectedJob)
                {
                    ResetBools();
                    RecheckBools();
                }
                luxModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(CompleteWeapon));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return luxModel.AvailableJobs; }
            set
            {
                luxModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public LuxModel LuxModel
        {
            get { return luxModel; }
            set
            {
                if (value != null)
                {
                    luxModel = value;
                    OnPropertyChanged(nameof(LuxModel));
                }
            }
        }
        public string CompleteWeapon
        {
            get
            {
                if (SelectedJob == null | SelectedJob == "") { return "Complete Weapon"; }
                else { return HWInfo.ReturnCompleteWeaponName(SelectedJob); }
            }
        }


        private ObservableCollection<bool> DungeonBools
        {
            get
            {
                if (luxModel.DungeonBools == null) { ResetBools(); }
                return luxModel.DungeonBools;
            }
            set
            {
                luxModel.DungeonBools = value;
                OnPropertyChanged(nameof(DungeonBools));
            }
        }

        public bool DungeonBool00 { get { return DungeonBools[00]; } set { DungeonBools[00] = value; AdjustBools(00, value); } }
        public bool DungeonBool01 { get { return DungeonBools[01]; } set { DungeonBools[01] = value; AdjustBools(01, value); } }
        public bool DungeonBool02 { get { return DungeonBools[02]; } set { DungeonBools[02] = value; AdjustBools(02, value); } }
        public bool DungeonBool03 { get { return DungeonBools[03]; } set { DungeonBools[03] = value; AdjustBools(03, value); } }
        public bool DungeonBool04 { get { return DungeonBools[04]; } set { DungeonBools[04] = value; AdjustBools(04, value); } }
        public bool DungeonBool05 { get { return DungeonBools[05]; } set { DungeonBools[05] = value; AdjustBools(05, value); } }
        public bool DungeonBool06 { get { return DungeonBools[06]; } set { DungeonBools[06] = value; AdjustBools(06, value); } }
        public bool DungeonBool07 { get { return DungeonBools[07]; } set { DungeonBools[07] = value; AdjustBools(07, value); } }
        public bool DungeonBool08 { get { return DungeonBools[08]; } set { DungeonBools[08] = value; AdjustBools(08, value); } }
        public bool DungeonBool09 { get { return DungeonBools[09]; } set { DungeonBools[09] = value; AdjustBools(09, value); } }
        public bool DungeonBool10 { get { return DungeonBools[10]; } set { DungeonBools[10] = value; AdjustBools(10, value); } }
        public bool DungeonBool11 { get { return DungeonBools[11]; } set { DungeonBools[11] = value; AdjustBools(11, value); } }

        public int Ink
        {
            get
            {
                if (luxModel.Ink < 0) { Ink = 0; }
                return LuxModel.Ink;
            }
            set
            {
                if (value < 0) { LuxModel.Ink = 0; }
                else { LuxModel.Ink = value; }
                RecheckInk();
            }
        }

        public int RemaningInk
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return Math.Max(0,AvailableJobs.Count - Ink);
            }
        }

        public int RemainingPoetics => RemaningInk * 500;

        #endregion

        #region Methods
        private void RecheckInk()
        {
            OnPropertyChanged(nameof(Ink));
            OnPropertyChanged(nameof(RemaningInk));
            OnPropertyChanged(nameof(RemainingPoetics));
        }
        private void ResetBools()
        {
            luxModel.DungeonBools = new ObservableCollection<bool>()
                    {
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false,
                        false
                    };
            
        }
        private void AdjustBools(int BoolIndex, bool value)
        {

            switch (BoolIndex)
            {
                case var expression when BoolIndex <= 2:
                    SetBools(0,3, value);
                    break;
                case var expression when BoolIndex <= 6:
                    SetBools(3,7, value);
                    break;
                case var expression when BoolIndex <= 8:
                    SetBools(7,10, value);
                    break;
                default:
                    SetBools(9,12, value);
                    break;

            }
            RecheckBools();
        }
        private void RecheckBools()
        {
            OnPropertyChanged(nameof(DungeonBool00));
            OnPropertyChanged(nameof(DungeonBool01));
            OnPropertyChanged(nameof(DungeonBool02));
            OnPropertyChanged(nameof(DungeonBool03));
            OnPropertyChanged(nameof(DungeonBool04));
            OnPropertyChanged(nameof(DungeonBool05));
            OnPropertyChanged(nameof(DungeonBool06));
            OnPropertyChanged(nameof(DungeonBool07));
            OnPropertyChanged(nameof(DungeonBool08));
            OnPropertyChanged(nameof(DungeonBool09));
            OnPropertyChanged(nameof(DungeonBool10));
            OnPropertyChanged(nameof(DungeonBool11));
        }
        private void SetBools(int TrueIndex,int FalseIndex, bool value)
        {
            if (value)
            {
                for (int index = 0; index < TrueIndex; index++)
                {
                    DungeonBools[index] = true;
                }
            }
            else
            {
                for (int index = FalseIndex; index < DungeonBools.Count; index++)
                {
                    DungeonBools[index] = false;
                }
            }
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Lux.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Lux.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            RecheckInk();
        }
        #endregion

        #region Commands
        #region Complete Button
        private ICommand _CompleteButton;

        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CompleteCommand(),
                        param => this.CompleteCan()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CompleteCan() { return SelectedJob != null; }
        private void CompleteCommand()
        {

            HWJob tempJob = selectedCharacter.HWModel.HWJobList[HWInfo.JobListString.IndexOf(SelectedJob)];

            HWStageCompleter.ProgressClass(selectedCharacter, tempJob.Lux, true);

            LoadAvailableJobs();
            ResetBools();
        }
        #endregion
        #region Add Materials

        private ICommand _IncrementButton;

        public ICommand IncrementButton
        {
            get
            {
                if (_IncrementButton == null)
                {
                    _IncrementButton = new RelayCommand(
                        param => this.IncrementCommand(param)
                        );
                }
                return _IncrementButton;
            }
        }
        private void IncrementCommand(object param)
        {
            Ink += 1;
        }

        #endregion
        #endregion


    }
}
