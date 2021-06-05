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

namespace FFXIVRelicTracker._05_ShB._04_LawsOrder
{
    public class LawsOrderViewModel : ObservableObject, IPageViewModel
    {

        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private LawsOrderModel lawsOrderModel;
        #endregion

        #region Constructor
        public LawsOrderViewModel()
        {

        }
        public LawsOrderViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Law's Order";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    LawsOrderModel = SelectedCharacter.ShBModel.LawsOrderModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public LawsOrderModel LawsOrderModel
        {
            get { return lawsOrderModel; }
            set
            {
                lawsOrderModel = value;
                OnPropertyChanged(nameof(LawsOrderModel));
            }
        }

        public string CurrentLawsOrder
        {
            get { return lawsOrderModel.CurrentLawsOrder; }
            set
            {
                lawsOrderModel.CurrentLawsOrder = value;
                OnPropertyChanged(nameof(CurrentLawsOrder));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return lawsOrderModel.AvailableJobs; }
            set
            {
                lawsOrderModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public int MemoryCount
        {
            get
            {
                if (lawsOrderModel.MemoryCount < 0) { MemoryCount = 0; }
                return lawsOrderModel.MemoryCount;
            }
            set
            {
                if (value < 0) { lawsOrderModel.MemoryCount = 0; }
                else { lawsOrderModel.MemoryCount = value; }
                OnPropertyChanged(nameof(MemoryCount));
            }
        }
        
        public int MemoryNeeded
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return AvailableJobs.Count * 15;
            }
        }

        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if (job.LawsOrder.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.LawsOrder.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            //Calculate remaining memories to acquire
            OnPropertyChanged(nameof(MemoryCount));
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

        private bool CompleteCan() { return CurrentLawsOrder != null; }
        private void CompleteCommand()
        {

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(CurrentLawsOrder)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.LawsOrder, true);

            LoadAvailableJobs();

            OnPropertyChanged(nameof(MemoryCount));
            OnPropertyChanged(nameof(MemoryNeeded));
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
