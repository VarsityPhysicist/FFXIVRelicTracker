using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._01_Animated
{
    class AnimatedViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private AnimatedModel animatedModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructors
        public AnimatedViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Animated";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AnimatedModel = selectedCharacter.HWModel.AnimatedModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return animatedModel.SelectedJob; }
            set
            {
                animatedModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return animatedModel.AvailableJobs; }
            set
            {
                animatedModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public AnimatedModel AnimatedModel
        {
            get { return animatedModel; }
            set
            {
                if (value != null)
                {
                    animatedModel = value;
                    OnPropertyChanged(nameof(AnimatedModel));
                }
            }
        }

        public int RemainingCrystals { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return AvailableJobs.Count; } }
        public int WindCount { get { if (animatedModel.WindCount < 0) { WindCount = 0; } return animatedModel.WindCount; } set { if (value >= 0 & value <= 20) { animatedModel.WindCount = value; OnPropertyChanged(nameof(WindCount)); } } }
        public int FireCount { get { if (animatedModel.FireCount < 0) { FireCount = 0; } return animatedModel.FireCount; } set { if(value>=0 & value <= 20) { animatedModel.FireCount = value; OnPropertyChanged(nameof(FireCount)); } } }
        public int LightningCount { get { if (animatedModel.LightningCount < 0) { LightningCount = 0; } return animatedModel.LightningCount; } set { if(value>=0 & value <= 20) { animatedModel.LightningCount = value; OnPropertyChanged(nameof(LightningCount)); } } }
        public int IceCount { get { if (animatedModel.IceCount < 0) { IceCount = 0; } return animatedModel.IceCount; } set { if(value>=0 & value <= 20) { animatedModel.IceCount = value; OnPropertyChanged(nameof(IceCount)); } } }
        public int EarthCount { get { if (animatedModel.EarthCount < 0) { EarthCount = 0; } return animatedModel.EarthCount; } set { if(value>=0 & value <= 20) { animatedModel.EarthCount = value; OnPropertyChanged(nameof(EarthCount)); } } }
        public int WaterCount { get { if (animatedModel.WaterCount < 0) { WaterCount = 0; } return animatedModel.WaterCount; } set { if(value>=0 & value <= 20) { animatedModel.WaterCount = value; OnPropertyChanged(nameof(WaterCount)); } } }
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Animated.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Animated.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(RemainingCrystals));

            OnPropertyChanged(nameof(WindCount));
            OnPropertyChanged(nameof(FireCount));
            OnPropertyChanged(nameof(LightningCount));
            OnPropertyChanged(nameof(IceCount));
            OnPropertyChanged(nameof(EarthCount));
            OnPropertyChanged(nameof(WaterCount));

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

            HWStageCompleter.ProgressClass(selectedCharacter, tempJob.Animated, true);

            LoadAvailableJobs();
        }
        #endregion

        #region Add/Subtract Materia

        private ICommand _IncrementButton;
        private ICommand _DecrementButton;

        public ICommand IncrementButton
        {
            get
            {
                if (_IncrementButton == null)
                {
                    _IncrementButton = new RelayCommand(
                        param => this.IncrementCommand(param, true)
                        );
                }
                return _IncrementButton;
            }
        }
        public ICommand DecrementButton
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

            int addedValue;
            if (add) { addedValue = 1; }
            else { addedValue = -1; }

            switch (sum)
            {
                case "WindCount":
                    WindCount += addedValue;
                    break;
                case "FireCount":
                    FireCount += addedValue;
                    break;
                case "LightningCount":
                    LightningCount += addedValue;
                    break;
                case "IceCount":
                    IceCount += addedValue;
                    break;
                case "EarthCount":
                    EarthCount += addedValue;
                    break;
                case "WaterCount":
                    WaterCount += addedValue;
                    break;
            }
        }

        #endregion
        #endregion
    }
}
