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

namespace FFXIVRelicTracker._05_ShB._01_Resistance
{
    public class ResistanceViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private ResistanceModel resistanceModel;
        #endregion

        #region Constructor
        public ResistanceViewModel()
        {

        }
        public ResistanceViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Resistance";

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    ResistanceModel = SelectedCharacter.ShBModel.ResistanceModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public ResistanceModel ResistanceModel
        {
            get { return resistanceModel; }
            set
            {
                resistanceModel = value;
                OnPropertyChanged(nameof(ResistanceModel));
            }
        }

        public string CurrentResistance
        {
            get { return resistanceModel.CurrentResistance; }
            set
            {
                resistanceModel.CurrentResistance = value;
                OnPropertyChanged(nameof(CurrentResistance));
            }
        }
        public int CurrentScalepowder
        {
            get { return resistanceModel.CurrentScalepowder; }
            set
            {
                if(value>=0 & value < 70)
                {
                    resistanceModel.CurrentScalepowder = value;
                    OnPropertyChanged(nameof(CurrentScalepowder));
                    OnPropertyChanged(nameof(ScalepowderCost));
                }
            }
        }
        public int NeededScalepowder { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return (Math.Min(16, AvailableJobs.Count)) * 4; } }
        public int ScalepowderCost { get { return ((NeededScalepowder-CurrentScalepowder) * 250); } }

        public ObservableCollection<string> AvailableJobs
        {
            get { return resistanceModel.AvailableJobs; }
            set
            {
                resistanceModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }
        public bool CompletedFirstResistance { get { return AvailableJobs.Count < ShBInfo.JobListString.Count; } }
        #endregion


        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach( ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                if(job.Resistance.Progress==BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Resistance.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    ShBInfo.ReloadJobList(AvailableJobs, job.Name);                 
                }
            }
            OnPropertyChanged(nameof(CompletedFirstResistance));
            OnPropertyChanged(nameof(NeededScalepowder));
            OnPropertyChanged(nameof(ScalepowderCost));
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

        private bool CompleteCan() { return CurrentResistance != null; }
        private void CompleteCommand()
        {

            ShBJob tempJob = selectedCharacter.ShBModel.ShbJobList[ShBInfo.JobListString.IndexOf(CurrentResistance)];

            ShBStageCompleter.ProgressClass(selectedCharacter, tempJob.Resistance, true);

            LoadAvailableJobs();

        }
        #endregion

        #region Increment Scalepowder
        private ICommand _ScalepowderButton;

        public ICommand ScalepowderButton
        {
            get
            {
                if (_ScalepowderButton == null)
                {
                    _ScalepowderButton = new RelayCommand(
                        param => this.ScalepowderCommand(param)
                        );
                }
                return _ScalepowderButton;
            }
        }

        private void ScalepowderCommand(object param)
        {
            CurrentScalepowder += 1;
        }
        #endregion
        #endregion
    }
}
