using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Nexus
{
    class NexusViewModel : ObservableObject, IPageViewModel
    {
        public string Name => "Nexus";
        private IEventAggregator iEventAggregator;
        private ArrWeapon arrWeapon;
        private Character selectedCharacter;
        private NexusModel nexusModel;
        private ObservableCollection<string> availableNexusJobs;

        public NexusViewModel(IEventAggregator iEventAggregator)
        {
            this.iEventAggregator = iEventAggregator;
            SubscriptionToken subscriptionToken =
                            this
                                .iEventAggregator
                                .GetEvent<PubSubEvent<Character>>()
                                .Subscribe((details) =>
                                {
                                    this.SelectedCharacter = details;
                                });
            iEventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Subscribe((details) => { this.ArrWeapon = details; });
        }

        #region Properties

        #region ViewModel Properties
        public NexusModel NexusModel
        {
            get { return nexusModel; }
            set
            {
                nexusModel = value;
                OnPropertyChanged(nameof(NexusModel));
            }
        }

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;

                OnPropertyChanged(nameof(SelectedCharacter));
                if (value != null)
                {
                    NexusModel = SelectedCharacter.ArrProgress.NexusModel;
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
                }
            }
        }
        public ArrWeapon ArrWeapon
        {
            get { return arrWeapon; }
            set
            {
                arrWeapon = value;
                OnPropertyChanged(nameof(ArrWeapon));
                LoadAvailableJobs();
            }
        }

        public ObservableCollection<string> AvailableNexusJobs
        {
            get { return availableNexusJobs; }
            set
            {
                availableNexusJobs = value;
                OnPropertyChanged(nameof(AvailableNexusJobs));
            }
        }

        public string CurrentNexus
        {
            get { return NexusModel.CurrentNexus; }
            set
            {
                NexusModel.CurrentNexus = value;
                OnPropertyChanged(nameof(CurrentNexus));
            }
        }
        public string LightError
        {
            get { return lightError; }
            set
            {
                lightError = value;
                OnPropertyChanged(nameof(LightError));
            }
        }

        public string CurrentLightString
        {
            get { return currentLightString; }
            set
            {
                LightError = "";
                currentLightString = value;
                OnPropertyChanged(nameof(CurrentLightString));
                if (CurrentLight.ToString() != value & value != "")
                {
                    try
                    {
                        CurrentLight = Int32.Parse(value);
                    }
                    catch (FormatException)
                    {
                        LightError = "Enter numeric values only";
                    }
                }
                else if (value == "")
                {
                    CurrentLight = 0;
                }

            }
        }

        public int CurrentLight
        {
            get { return NexusModel.CurrentLight; }
            set
            {
                if(value>=0 & value<=3000)
                {
                    NexusModel.CurrentLight = value;
                    if (value.ToString() != CurrentLightString)
                    {
                        CurrentLightString = value.ToString();
                    }
                    OnPropertyChanged(nameof(CurrentLight));
                    UpdateActivity();
                }
            }
        }

        public string NexusActivity
        {
            get { return NexusModel.NexusActivity; }
            set
            {
                NexusModel.NexusActivity = value;
                OnPropertyChanged(nameof(NexusActivity));
            }
        }

        #endregion

        #endregion

        #region Methods
        private void UpdateActivity()
        {
            NexusActivity = CurrentLight switch
            {
                int n when n >= 2000 => "Bursting Activity (Complete)",
                int n when n >= 1800 => "Extreme Activity",
                int n when n >= 1600 => "Intense Activity",
                int n when n >= 1400 => "Vigorous Activity",
                int n when n >= 1200 => "Robust Activity",
                int n when n >= 1000 => "Distinct Activity",
                int n when n >= 800 => "Modest Activity",
                int n when n >= 600 => "Slight Activity",
                int n when n >= 400 => "Faint Activity",
                int n when n >= 200 => "Indistinct Activity",
                _ => "No Activity",
            };
        }
        public void LoadAvailableJobs()
        {
            if (availableNexusJobs == null) { availableNexusJobs = new ObservableCollection<string>(); }
            foreach (ArrJobs job in ArrWeapon.JobList)
            {
                if (job.Nexus.Progress != ArrProgress.States.Completed & !availableNexusJobs.Contains(job.Name))
                {
                    AvailableNexusJobs.Add(job.Name);
                }
            }
        }
        #endregion

        #region Commands

        #region Add Points
        private ICommand _AddPointsButton;

        public ICommand AddPointsButton
        {
            get
            {
                if (_AddPointsButton == null)
                {
                    _AddPointsButton = new RelayCommand(
                        param => this.AddCommand(param),
                        param => this.CanAdd()
                        );
                }
                return _AddPointsButton;
            }
        }

        private bool CanAdd() { return true; }
        private void AddCommand(object param)
        {
            CurrentLight += Int32.Parse((string)param);
        }
        #endregion

        #region Complete Button
        private ICommand _CompleteButton;
        private string currentLightString;
        private string lightError;

        public ICommand CompleteButton
        {
            get
            {
                if (_CompleteButton == null)
                {
                    _CompleteButton = new RelayCommand(
                        param => this.CompleteCommand(),
                        param => this.CanComplete()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CanComplete() { return CurrentNexus != null; }
        private void CompleteCommand()
        {

            ArrJobs tempJob = ArrWeapon.JobList[ArrInfo.JobListString.IndexOf(CurrentNexus)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Nexus, true);

            CurrentLight = 0;
            LoadAvailableJobs();


        }

        #endregion
        #endregion
    }
}
