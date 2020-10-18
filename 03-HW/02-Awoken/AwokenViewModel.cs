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
using System.Windows.Navigation;

namespace FFXIVRelicTracker._03_HW._02_Awoken
{
    public class AwokenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private AwokenModel awokenModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructors
        public AwokenViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Awoken";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AwokenModel = selectedCharacter.HWModel.AwokenModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return awokenModel.SelectedJob; }
            set
            {
                if(value!=null & value != awokenModel.SelectedJob)
                {
                    DungeonBools = new ObservableCollection<bool>()
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
                        false
                    };
                }
                awokenModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(AnimatedWeapon));
                OnPropertyChanged(nameof(AwokenWeapon));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return awokenModel.AvailableJobs; }
            set
            {
                awokenModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public AwokenModel AwokenModel
        {
            get { return awokenModel; }
            set
            {
                if (value != null)
                {
                    awokenModel = value;
                    OnPropertyChanged(nameof(AwokenModel));
                }
            }
        }

        public string AnimatedWeapon
        {
            //Check if saving+loading does not set this as expected
            get
            {
                if(SelectedJob==null | SelectedJob == "") { return "Animated Weapon"; }
                else { return HWInfo.ReturnAnimatedWeaponName(SelectedJob); }
            }
        }
        public string AwokenWeapon
        {
            //Check if saving+loading does not set this as expected
            get
            {
                if (SelectedJob == null | SelectedJob == "") { return "Weapon Awoken"; }
                else { return HWInfo.ReturnAwokenWeaponName(SelectedJob); }
            }
        }
        private ObservableCollection<bool> DungeonBools
        {
            get
            {
                if (awokenModel.DungeonBools == null)
                {
                    awokenModel.DungeonBools = new ObservableCollection<bool>()
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
                        false
                    };
                }
                return awokenModel.DungeonBools;
            }
            set
            {
                awokenModel.DungeonBools = value;
                OnPropertyChanged(nameof(DungeonBools));
            }
        }

        public bool Dungeon00 { get { return DungeonBools[00]; } set { DungeonBools[00] = value; AdjustBools(00, value);} }
        public bool Dungeon01 { get { return DungeonBools[01]; } set { DungeonBools[01] = value; AdjustBools(01, value);} }
        public bool Dungeon02 { get { return DungeonBools[02]; } set { DungeonBools[02] = value; AdjustBools(02, value);} }
        public bool Dungeon03 { get { return DungeonBools[03]; } set { DungeonBools[03] = value; AdjustBools(03, value);} }
        public bool Dungeon04 { get { return DungeonBools[04]; } set { DungeonBools[04] = value; AdjustBools(04, value);} }
        public bool Dungeon05 { get { return DungeonBools[05]; } set { DungeonBools[05] = value; AdjustBools(05, value);} }
        public bool Dungeon06 { get { return DungeonBools[06]; } set { DungeonBools[06] = value; AdjustBools(06, value);} }
        public bool Dungeon07 { get { return DungeonBools[07]; } set { DungeonBools[07] = value; AdjustBools(07, value);} }
        public bool Dungeon08 { get { return DungeonBools[08]; } set { DungeonBools[08] = value; AdjustBools(08, value);} }
        public bool Dungeon09 { get { return DungeonBools[09]; } set { DungeonBools[09] = value; AdjustBools(09, value);} }

        #endregion

        #region Methods
        private void AdjustBools(int BoolIndex, bool value)
        {
            if (value)
            {
                for(int index = 0; index <= BoolIndex; index++)
                {
                    DungeonBools[index] = true;
                }
            }
            else
            {
                for (int index = BoolIndex; index < DungeonBools.Count; index++)
                {
                    DungeonBools[index] = false;
                }
            }

            OnPropertyChanged(nameof(Dungeon00));
            OnPropertyChanged(nameof(Dungeon01));
            OnPropertyChanged(nameof(Dungeon02));
            OnPropertyChanged(nameof(Dungeon03));
            OnPropertyChanged(nameof(Dungeon04));
            OnPropertyChanged(nameof(Dungeon05));
            OnPropertyChanged(nameof(Dungeon06));
            OnPropertyChanged(nameof(Dungeon07));
            OnPropertyChanged(nameof(Dungeon08));
            OnPropertyChanged(nameof(Dungeon09));
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Awoken.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Awoken.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
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

            HWInfo.ProgressClass(selectedCharacter, tempJob.Awoken, true);

            LoadAvailableJobs();
        }
        #endregion

        #endregion


    }
}
