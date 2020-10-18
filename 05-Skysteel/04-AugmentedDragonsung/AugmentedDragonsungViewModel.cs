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

namespace FFXIVRelicTracker._05_Skysteel._04_AugmentedDragonsung
{
    public class AugmentedDragonsungViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private AugmentedDragonsungModel augmentedDragonsungModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string, string> jobInfo;
        #endregion

        #region Constructors
        public AugmentedDragonsungViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Augmented Dragonsung";
        public AugmentedDragonsungModel AugmentedDragonsungModel
        {
            get { return augmentedDragonsungModel; }
            set
            {
                augmentedDragonsungModel = value;
                OnPropertyChanged(nameof(AugmentedDragonsungModel));
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
            get { return AugmentedDragonsungModel.SelectedJob; }
            set
            {
                AugmentedDragonsungModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));
                SetGatherLoc();

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnAugmentedDragonsungTuple(SelectedJob);


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
            get { return AugmentedDragonsungModel.AvailableJobs; }
            set
            {
                AugmentedDragonsungModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string ToolName { get { return AugmentedDragonsungModel.ToolName; } set { AugmentedDragonsungModel.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string TradedMat { get { return AugmentedDragonsungModel.TradedMat; } set { AugmentedDragonsungModel.TradedMat = value; OnPropertyChanged(nameof(TradedMat)); } }
        public string CraftedMat { get { return AugmentedDragonsungModel.CraftedMat; } set { AugmentedDragonsungModel.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return AugmentedDragonsungModel.FirstMat; } set { AugmentedDragonsungModel.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return AugmentedDragonsungModel.SecondMat; } set { AugmentedDragonsungModel.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }
        public string GatherLoc { get { return AugmentedDragonsungModel.GatherLoc; } set { AugmentedDragonsungModel.GatherLoc = value; OnPropertyChanged(nameof(GatherLoc)); } }


        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int MinRemainingYellowScrips { get { return AugmentedDragonsungModel.MinRemainingYellowScrips; } set { AugmentedDragonsungModel.MinRemainingYellowScrips = value; OnPropertyChanged(nameof(MinRemainingYellowScrips)); } }
        public int MaxRemainingYellowScrips { get { return AugmentedDragonsungModel.MaxRemainingYellowScrips; } set { AugmentedDragonsungModel.MaxRemainingYellowScrips = value; OnPropertyChanged(nameof(MaxRemainingYellowScrips)); } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (AugmentedDragonsungModel == null)
            {
                AugmentedDragonsungModel = new AugmentedDragonsungModel();
                selectedCharacter.SkysteelModel.AugmentedDragonsungModel = AugmentedDragonsungModel;
            }
            else { AugmentedDragonsungModel = selectedCharacter.SkysteelModel.AugmentedDragonsungModel; }
        }

        public void SetGatherLoc()
        {
            switch (SelectedJob)
            {
                case "MIN":
                    GatherLoc = " (Yanxia | The Lochs)";
                    break;
                case "BTN":
                    GatherLoc = " (Yanxia | The Lochs)";
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
                if (job.AugmentedDragonsung.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.AugmentedDragonsung.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            int tempCount = AvailableJobs.Count;

            if (AvailableJobs.Contains("MIN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("BTN")) { tempCount -= 1; }
            if (AvailableJobs.Contains("FSH")) { tempCount -= 1; }

            MinRemainingYellowScrips = tempCount * 18 * 60;
            MaxRemainingYellowScrips = tempCount * 45 * 60;
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

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.AugmentedDragonsung, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion
    }
}
