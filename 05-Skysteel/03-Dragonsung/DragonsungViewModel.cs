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

namespace FFXIVRelicTracker._05_Skysteel._03_Dragonsung
{
    public class DragonsungViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private DragonsungModel dragonsung1Model;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        private Tuple<string, string, string, string> jobInfo;
        #endregion

        #region Constructors
        public DragonsungViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Dragonsung";
        public DragonsungModel DragonsungModel
        {
            get { return dragonsung1Model; }
            set
            {
                dragonsung1Model = value;
                OnPropertyChanged(nameof(DragonsungModel));
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
            get { return DragonsungModel.SelectedJob; }
            set
            {
                DragonsungModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(IsGatherer));
                OnPropertyChanged(nameof(IsFSH));
                OnPropertyChanged(nameof(DisplayInfo));

                if (value != null)
                {
                    jobInfo = SkysteelInfo.ReturnDragonsungTuple(SelectedJob);


                    ToolName = jobInfo.Item1;
                    CraftedMat = jobInfo.Item2;
                    FirstMat = jobInfo.Item3;
                    SecondMat = jobInfo.Item4;
                }
            }
        }
        public ObservableCollection<string> AvailableJobs
        {
            get { return dragonsung1Model.AvailableJobs; }
            set
            {
                dragonsung1Model.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public string ToolName { get { return dragonsung1Model.ToolName; } set { dragonsung1Model.ToolName = value; OnPropertyChanged(nameof(ToolName)); } }
        public string CraftedMat { get { return dragonsung1Model.CraftedMat; } set { dragonsung1Model.CraftedMat = value; OnPropertyChanged(nameof(CraftedMat)); } }
        public string FirstMat { get { return dragonsung1Model.FirstMat; } set { dragonsung1Model.FirstMat = value; OnPropertyChanged(nameof(FirstMat)); } }
        public string SecondMat { get { return dragonsung1Model.SecondMat; } set { dragonsung1Model.SecondMat = value; OnPropertyChanged(nameof(SecondMat)); } }


        public string SelectedToolType { get { return SkysteelInfo.ReturnToolName(SelectedJob); } }
        public bool DisplayInfo { get { return SelectedJob != null; } }
        public bool IsGatherer { get { return (SelectedJob == "MIN" | SelectedJob == "BTN" | SelectedJob == "FSH"); } }
        public bool IsFSH { get { return SelectedJob == "FSH"; } }
        public int RemaningYellowScrips { get { if (AvailableJobs == null) { LoadAvailableJobs(); } return AvailableJobs.Count * 30 * 50; } }


        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (DragonsungModel == null)
            {
                DragonsungModel = new DragonsungModel();
                selectedCharacter.SkysteelModel.DragonsungModel = DragonsungModel;
            }
            else { DragonsungModel = selectedCharacter.SkysteelModel.DragonsungModel; }
        }


        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (job.Dragonsung.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Dragonsung.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(RemaningYellowScrips));
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

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.Dragonsung, true);

            LoadAvailableJobs();

        }
        #endregion

        #endregion
    }
}
