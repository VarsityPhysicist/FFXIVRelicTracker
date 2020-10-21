using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._07_Complete
{
    public class CompleteViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private CompleteModel completeModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public CompleteViewModel()
        {
        }
        public CompleteViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Complete";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    completeModel = selectedCharacter.HWModel.CompleteModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public string SelectedJob
        {
            get { return completeModel.SelectedJob; }
            set
            {
                if (SelectedJob != value) { ResetDungeons(); Light = 0; }

                completeModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(SharpenedWeapon));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return completeModel.AvailableJobs; }
            set
            {
                completeModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public CompleteModel CompleteModel
        {
            get { return completeModel; }
            set
            {
                if (value != null)
                {
                    completeModel = value;
                    OnPropertyChanged(nameof(CompleteModel));
                }
            }
        }
        public string SharpenedWeapon
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedJob)) { return "Sharpened Anima Weapon"; }
                else { return "Sharpened " + HWInfo.ReturnAnimaWeaponName(SelectedJob); }
            }
        }

        #region Dungeons
        public ObservableCollection<bool> DungeonBools
        {
            get
            {
                if (completeModel.DungeonBools == null) { ResetDungeons(); }
                return completeModel.DungeonBools;
            }
            set { completeModel.DungeonBools = value; OnPropertyChanged(nameof(DungeonBools)); }
        }

        public bool DungeonBool0 { get { return DungeonBools[0]; } set { DungeonBools[0] = value; AdjustBools(0,value); } }
        public bool DungeonBool1 { get { return DungeonBools[1]; } set { DungeonBools[1] = value; AdjustBools(1, value); } }
        public bool DungeonBool2 { get { return DungeonBools[2]; } set { DungeonBools[2] = value; AdjustBools(2,value); } }

        #endregion

        #region Light
        public int Light
        {
            get => completeModel.Light;
            set 
            {
                if (value >= 2000) { completeModel.Light = 2000; }
                else if (value <= 0) { completeModel.Light = 0; }
                else { completeModel.Light = value; }
                OnPropertyChanged(nameof(Light));
            }
        }
        #endregion

        #region Pneumite
        public int CurrentPneumite
        {
            get 
            {
                if (completeModel.CurrentPneumite < 0) { CurrentPneumite = 0; }
                return  completeModel.CurrentPneumite; 
            }
            set
            {
                if (value < 0) { completeModel.CurrentPneumite = 0; }
                else { completeModel.CurrentPneumite = value; }
                CheckPneumite();
            }
        }

        public int RemainingPneumite
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return Math.Max(0, (AvailableJobs.Count * 15) - CurrentPneumite);
            }
        }

        public int RemainingPoetics => RemainingPneumite * 100;
        public int RemainingSeals => RemainingPneumite * 4000;

        #endregion

        #endregion

        #region Methods
        private void ResetDungeons()
        {
            completeModel.DungeonBools = new ObservableCollection<bool>
            {
                false, false, false
            };
        }
        private void AdjustBools(int BoolIndex, bool value)
        {
            if (value)
            {
                for (int index = 0; index <= BoolIndex; index++)
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

            OnPropertyChanged(nameof(DungeonBool0));
            OnPropertyChanged(nameof(DungeonBool1));
            OnPropertyChanged(nameof(DungeonBool2));
        }
        private void CheckPneumite()
        {
            OnPropertyChanged(nameof(CurrentPneumite));
            OnPropertyChanged(nameof(RemainingPneumite));
            OnPropertyChanged(nameof(RemainingPoetics));
            OnPropertyChanged(nameof(RemainingSeals));
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Complete.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Complete.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(SharpenedWeapon));
            CheckPneumite();
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

            HWStageCompleter.ProgressClass(selectedCharacter, tempJob.Complete, true);

            LoadAvailableJobs();
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
            int materialCount = int.Parse((string)param);
            switch (materialCount)
            {
                case 1:
                    CurrentPneumite += materialCount;
                    CheckPneumite();
                    break;
                default:
                    Light += materialCount;
                    break;
            }
        }

        #endregion
        #endregion


    }
}
