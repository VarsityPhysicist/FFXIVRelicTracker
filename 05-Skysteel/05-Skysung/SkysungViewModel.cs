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

namespace FFXIVRelicTracker._05_Skysteel._05_Skysung
{
    class SkysungViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private SkysungModel skysungModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string, string> jobInfo;
        #endregion

        #region Constructors
        public SkysungViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Skysung";
        public SkysungModel SkysungModel
        {
            get { return skysungModel; }
            set
            {
                skysungModel = value;
                OnPropertyChanged(nameof(SkysungModel));
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
            get { return SkysungModel.SelectedJob; }
            set
            {
                SkysungModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));
                SetGatherLoc();

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnSkysungTuple(SelectedJob);


                    ToolName = jobInfo.Item1;
                    TradedMat = jobInfo.Item2;
                    CraftedMat = jobInfo.Item3;
                    FirstMat = jobInfo.Item4;
                    SecondMat = jobInfo.Item5;
                }
            }
        }
        public ObservableCollection<string> AvailableJobs
        {
            get { return SkysungModel.AvailableJobs; }
            set
            {
                SkysungModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string ToolName { get { return SkysungModel.ToolName; } set { SkysungModel.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string TradedMat { get { return SkysungModel.TradedMat; } set { SkysungModel.TradedMat = value; OnPropertyChanged(nameof(TradedMat)); } }
        public string CraftedMat { get { return SkysungModel.CraftedMat; } set { SkysungModel.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return SkysungModel.FirstMat; } set { SkysungModel.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return SkysungModel.SecondMat; } set { SkysungModel.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }
        public string GatherLoc { get { return SkysungModel.GatherLoc; } set { SkysungModel.GatherLoc = value; OnPropertyChanged(nameof(GatherLoc)); } }


        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int MinRemainingYellowScrips { get { return SkysungModel.MinRemainingYellowScrips; } set { SkysungModel.MinRemainingYellowScrips = value; OnPropertyChanged(nameof(MinRemainingYellowScrips)); } }
        public int MaxRemainingYellowScrips { get { return SkysungModel.MaxRemainingYellowScrips; } set { SkysungModel.MaxRemainingYellowScrips = value; OnPropertyChanged(nameof(MaxRemainingYellowScrips)); } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (SkysungModel == null)
            {
                SkysungModel = new SkysungModel();
                selectedCharacter.SkysteelModel.SkysungModel = SkysungModel;
            }
            else { SkysungModel = selectedCharacter.SkysteelModel.SkysungModel; }
        }

        public void SetGatherLoc()
        {
            switch (SelectedJob)
            {
                case "MIN":
                    GatherLoc = " (Azys Lla)";
                    break;
                case "BTN":
                    GatherLoc = " (Azys Lla)";
                    break;
                default:
                    GatherLoc = "";
                    break;
            }
        }
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (job.Skysung.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Skysung.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            int tempCount = AvailableJobs.Count;

            if (AvailableJobs.Contains("MIN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("BTN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("FSH")) { tempCount -= 1; }

            MinRemainingYellowScrips = tempCount * 21 * 60;
            MaxRemainingYellowScrips = tempCount * 53 * 60;
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

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.Skysung, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion
    }
}
