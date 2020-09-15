using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel._02_BasePlus1
{
    class BasePlus1ViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private BasePlus1Model basePlus1Model;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string> jobInfo;
        #endregion

        #region Constructors
        public BasePlus1ViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Skysteel Tool+1";
        public BasePlus1Model BasePlus1Model
        {
            get { return basePlus1Model; }
            set
            {
                basePlus1Model = value;
                OnPropertyChanged(nameof(BasePlus1Model));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    CheckModelExists();
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
                
            }
        }

        public string SelectedJob
        {
            get { return BasePlus1Model.SelectedJob; }
            set
            {
                BasePlus1Model.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnJobTuple(SelectedJob);


                    ToolName = jobInfo.Item1;
                    CraftedMat = jobInfo.Item2;
                    FirstMat = jobInfo.Item3;
                    SecondMat = jobInfo.Item4;
                }
            }
        }
        public ObservableCollection<string> AvailableJobs
        {
            get { return basePlus1Model.AvailableJobs; }
            set
            {
                basePlus1Model.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string ToolName { get { return basePlus1Model.ToolName; } set { basePlus1Model.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string CraftedMat { get { return basePlus1Model.CraftedMat; } set { basePlus1Model.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return basePlus1Model.FirstMat; } set { basePlus1Model.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return basePlus1Model.SecondMat; } set { basePlus1Model.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }


        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob!=null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (BasePlus1Model == null)
            {
                BasePlus1Model = new BasePlus1Model();
                selectedCharacter.SkysteelModel.BasePlus1Model = BasePlus1Model;
            }
            else { BasePlus1Model = selectedCharacter.SkysteelModel.BasePlus1Model; }
        }


        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (job.BasePlus1.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.BasePlus1.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
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

            SkysteelJob tempJob = selectedCharacter.SkysteelModel.SkysteelJobList[SkysteelInfo.JobListString.IndexOf(SelectedJob)];

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.BasePlus1, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion


    }
}
