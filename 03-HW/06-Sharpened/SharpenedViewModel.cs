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

namespace FFXIVRelicTracker._03_HW._06_Sharpened
{
    public class SharpenedViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private SharpenedModel sharpenedModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public SharpenedViewModel()
        {
        }
        public SharpenedViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Sharpened";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    sharpenedModel = selectedCharacter.HWModel.SharpenedModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public string SelectedJob
        {
            get { return sharpenedModel.SelectedJob; }
            set
            {
                sharpenedModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return sharpenedModel.AvailableJobs; }
            set
            {
                sharpenedModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public SharpenedModel SharpenedModel
        {
            get { return sharpenedModel; }
            set
            {
                if (value != null)
                {
                    sharpenedModel = value;
                    OnPropertyChanged(nameof(SharpenedModel));
                }
            }
        }

        public int CurrentCluster
        {
            get => sharpenedModel.Currentcluster;
            set
            {
                if (value <= 0) { sharpenedModel.Currentcluster = 0; }
                else { sharpenedModel.Currentcluster = value; }
            }
        }

        public int RemainingCluster
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return Math.Max(0, (AvailableJobs.Count * 50) - CurrentCluster);
            }
        }
        #endregion

        #region Methods
        private void Recalculate()
        {
            OnPropertyChanged(nameof(CurrentCluster));
            OnPropertyChanged(nameof(RemainingCluster));
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Sharpened.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Sharpened.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            Recalculate();
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

            HWStageCompleter.ProgressClass(selectedCharacter, tempJob.Sharpened, true);

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
            string materialCount = (string)param;
            switch (materialCount)
            {
                case "1":
                    CurrentCluster += 1;
                    break;
                case "18":
                    CurrentCluster += 18;
                    break;
            }
            Recalculate();
        }

        #endregion
        #endregion


    }
}
