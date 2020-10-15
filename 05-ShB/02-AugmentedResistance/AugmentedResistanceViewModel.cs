using FFXIVRelicTracker._05_ShB.ShBHelpers;
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

namespace FFXIVRelicTracker._05_ShB._02_AugmentedResistance
{

    public class AugmentedResistanceViewModel : ObservableObject, IPageViewModel
    {

        #region Fields
        public string Name => "Augmented Resistance";
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AugmentedResistanceModel augmentedResistanceModel;
        #endregion

        #region Constructor
        public AugmentedResistanceViewModel()
        {

        }

        public AugmentedResistanceViewModel(IEventAggregator eventAggregator)
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
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AugmentedResistanceModel = SelectedCharacter.ShBModel.AugmentedResistanceModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AugmentedResistanceModel AugmentedResistanceModel
        {
            get { return augmentedResistanceModel; }
            set
            {
                augmentedResistanceModel = value;
                OnPropertyChanged(nameof(AugmentedResistanceModel));
            }
        }

        public string CurrentAugmentedResistance
        {
            get { return augmentedResistanceModel.CurrentAugmentedResistance; }
            set
            {
                augmentedResistanceModel.CurrentAugmentedResistance = value;
                OnPropertyChanged(nameof(CurrentAugmentedResistance));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return augmentedResistanceModel.AvailableJobs; }
            set
            {
                augmentedResistanceModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public int MemoryNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count*20; } else { return 0; }; } }
        public int HarrowingCount { get { return augmentedResistanceModel.HarrowingCount; } set { if (value >= 0) { augmentedResistanceModel.HarrowingCount = value; OnPropertyChanged(nameof(HarrowingCount)); } } }
        public int TorturedCount { get { return augmentedResistanceModel.TorturedCount; } set { if (value >= 0) { augmentedResistanceModel.TorturedCount = value; OnPropertyChanged(nameof(TorturedCount)); } } }
        public int SorrowfulCount { get { return augmentedResistanceModel.SorrowfulCount; } set { if (value >= 0) { augmentedResistanceModel.SorrowfulCount = value; OnPropertyChanged(nameof(SorrowfulCount)); } } }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.AugmentedResistance.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.AugmentedResistance.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(MemoryNeeded));
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

        private bool CompleteCan() { return CurrentAugmentedResistance != null; }
        private void CompleteCommand()
        {

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(CurrentAugmentedResistance)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.AugmentedResistance, true);

            LoadAvailableJobs();
            OnPropertyChanged(nameof(HarrowingCount));
            OnPropertyChanged(nameof(TorturedCount)); 
            OnPropertyChanged(nameof(SorrowfulCount)); 

        }
        #endregion

        #region Increment Memory
        private ICommand _MemoryButton;

        public ICommand MemoryButton
        {
            get
            {
                if (_MemoryButton == null)
                {
                    _MemoryButton = new RelayCommand(
                        param => this.MemoryCommand(param)
                        );
                }
                return _MemoryButton;
            }
        }

        private void MemoryCommand(object param)
        {
            switch (param)
            {
                case "Harrowing":
                    HarrowingCount += 1;
                    break;
                case "Tortured":
                    TorturedCount += 1;
                    break;
                case "Sorrowful":
                    SorrowfulCount += 1;
                    break;
                default:
                    break;
            }
        }
        #endregion
        #endregion


    }
}
