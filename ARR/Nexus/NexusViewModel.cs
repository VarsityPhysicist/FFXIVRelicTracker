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
            switch (CurrentLight)
            {
                case int n when n >= 2000:
                    NexusActivity = "Bursting Activity (Complete)";
                    break;
                case int n when n >= 1800:
                    NexusActivity = "Extreme Activity";
                    break;
                case int n when n >= 1600:
                    NexusActivity = "Intense Activity";
                    break;
                case int n when n >= 1400:
                    NexusActivity = "Vigorous Activity";
                    break;
                case int n when n >= 1200:
                    NexusActivity = "Robust Activity";
                    break;
                case int n when n >= 1000:
                    NexusActivity = "Distinct Activity";
                    break;
                case int n when n >= 800:
                    NexusActivity = "Modest Activity";
                    break;
                case int n when n >= 600:
                    NexusActivity = "Slight Activity";
                    break;
                case int n when n >= 400:
                    NexusActivity = "Faint Activity";
                    break;
                case int n when n >= 200:
                    NexusActivity = "Indistinct Activity";
                    break;
                default:
                    NexusActivity = "No Activity";
                    break;
            }
        }
        private void LoadAvailableJobs()
        {
            AvailableNexusJobs = new ObservableCollection<string>();
            {

                foreach (ArrJobs job in ArrWeapon.JobList)
                {
                    if (job.Nexus.Progress != ArrProgress.States.Completed)
                    {
                        AvailableNexusJobs.Add(job.Name);
                    }
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
                        param => this.CompleteCommand(param),
                        param => this.CanComplete()
                        );
                }
                return _CompleteButton;
            }
        }

        private bool CanComplete() { return CurrentNexus != null; }
        private void CompleteCommand(object param)
        {

            ArrJobs tempJob = ArrWeapon.JobList[ArrWeapon.JobListString.IndexOf(CurrentNexus)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Nexus, true);

            CurrentLight = 0;
            LoadAvailableJobs();


        }

        #endregion
        #endregion
    }
}
