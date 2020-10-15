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

namespace FFXIVRelicTracker._05_ShB._03_Recollection
{
    public class RecollectionViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private RecollectionModel recollectionModel;
        #endregion

        #region Constructor
        public RecollectionViewModel()
        {

        }
        public RecollectionViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Recollection";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    RecollectionModel = SelectedCharacter.ShBModel.RecollectionModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public RecollectionModel RecollectionModel
        {
            get { return recollectionModel; }
            set
            {
                recollectionModel = value;
                OnPropertyChanged(nameof(RecollectionModel));
            }
        }

        public string CurrentRecollection
        {
            get { return recollectionModel.CurrentRecollection; }
            set
            {
                recollectionModel.CurrentRecollection = value;
                OnPropertyChanged(nameof(CurrentRecollection));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return recollectionModel.AvailableJobs; }
            set
            {
                recollectionModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int MemoryCount { get { return recollectionModel.MemoryCount; } set { if (value >= 0) { recollectionModel.MemoryCount = value; OnPropertyChanged(nameof(MemoryCount)); } } }

        public int MemoryNeeded { get { if (AvailableJobs != null) { return AvailableJobs.Count * 6; } else { return 0; }; } }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.Recollection.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Recollection.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
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

        private bool CompleteCan() { return CurrentRecollection != null; }
        private void CompleteCommand()
        {

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(CurrentRecollection)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.Recollection, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(MemoryCount));
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
            MemoryCount += 1;
        }
        #endregion
        #endregion


    }
}
