using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_ShB._05_AugmentedLawsOrder
{
    class AugmentedLawsOrderViewModel : ObservableObject, IPageViewModel
    {

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private AugmentedLawsOrderModel augmentedLawsOrderModel;
        #endregion

        #region Constructor
        public AugmentedLawsOrderViewModel()
        {

        }
        public AugmentedLawsOrderViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Augmented Law's Order";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AugmentedLawsOrderModel = SelectedCharacter.ShBModel.AugmentedLawsOrderModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public AugmentedLawsOrderModel AugmentedLawsOrderModel
        {
            get { return augmentedLawsOrderModel; }
            set
            {
                augmentedLawsOrderModel = value;
                OnPropertyChanged(nameof(AugmentedLawsOrderModel));
            }
        }

        public string CurrentAugmentedLawsOrder
        {
            get { return augmentedLawsOrderModel.CurrentAugmentedLawsOrder; }
            set
            {
                augmentedLawsOrderModel.CurrentAugmentedLawsOrder = value;
                OnPropertyChanged(nameof(CurrentAugmentedLawsOrder));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return augmentedLawsOrderModel.AvailableJobs; }
            set
            {
                augmentedLawsOrderModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int ArtifactCount
        {
            get
            {
                if (augmentedLawsOrderModel.ArtifactCount < 0) { ArtifactCount = 0; }
                return augmentedLawsOrderModel.ArtifactCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.ArtifactCount = 0; }
                else { augmentedLawsOrderModel.ArtifactCount = value; }
                OnPropertyChanged(nameof(ArtifactCount));
            }
        } 
        public int YellowCount
        {
            get
            {
                if (augmentedLawsOrderModel.YellowMemoryCount < 0) { YellowCount = 0; }
                return augmentedLawsOrderModel.YellowMemoryCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.YellowMemoryCount = 0; }
                else if (value > 17) { augmentedLawsOrderModel.YellowMemoryCount = 18; }
                else { augmentedLawsOrderModel.YellowMemoryCount = value; }
                OnPropertyChanged(nameof(YellowCount));
            }
        }
        public int PurpleCount
        {
            get
            {
                if (augmentedLawsOrderModel.PurpleMemoryCount < 0) { PurpleCount = 0; }
                return augmentedLawsOrderModel.PurpleMemoryCount;
            }
            set
            {
                if (value < 0) { augmentedLawsOrderModel.PurpleMemoryCount = 0; }
                else if (value > 17) { augmentedLawsOrderModel.PurpleMemoryCount = 18; }
                else { augmentedLawsOrderModel.PurpleMemoryCount = value; }
                OnPropertyChanged(nameof(PurpleCount));
            }
        }

        public int ArtifactNeeded
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return AvailableJobs.Count * 15;
            }
        }

        public bool CompletedOnce
        {
            get
            {
                if (AvailableJobs == null)
                    return false;
                else if (AvailableJobs.Count == ShBHelpers.ShBInfo.JobListString.Count)
                    return false;
                else return true;
            }
        }
                

        #endregion

        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.AugmentedLawsOrder.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.AugmentedLawsOrder.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(ArtifactCount));
        }

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

        private bool CompleteCan() { return CurrentAugmentedLawsOrder != null; }
        private void CompleteCommand()
        {

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(CurrentAugmentedLawsOrder)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.AugmentedLawsOrder, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(ArtifactCount));
            OnPropertyChanged(nameof(ArtifactNeeded));
            OnPropertyChanged(nameof(CompletedOnce));
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
            string itemType = (string)param;

            switch (itemType)
            {
                case "Purple":
                    PurpleCount += 1;
                    break;
                case "Yellow":
                    YellowCount += 1;
                    break;
                default:
                    ArtifactCount += 1;
                    break;
                    
                        
            }
        }
        #endregion
        #endregion
    }
}
