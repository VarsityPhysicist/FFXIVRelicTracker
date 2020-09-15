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

namespace FFXIVRelicTracker._05_Skysteel._01_BaseTool
{
    public class BaseToolViewModel : ObservableObject, IPageViewModel, IIncompleteViewModel
    {
        #region Fields
        private BaseToolModel baseToolModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;

        #endregion

        #region Constructors
        public BaseToolViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Base Tool";

        public BaseToolModel BaseToolModel
        {
            get { return baseToolModel; }
            set
            {
                baseToolModel = value;
                OnPropertyChanged(nameof(BaseToolModel));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;

                CheckModelExists();

                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public string SelectedJob
        {
            get { return baseToolModel.SelectedJob; }
            set
            {
                baseToolModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return baseToolModel.AvailableJobs; }
            set
            {
                baseToolModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public bool CompletedFirstTool { get { return AvailableJobs.Count < SkysteelInfo.JobListString.Count; } }
        #endregion

        #region Methods
        public void CheckModelExists()
        {
            if (BaseToolModel == null)
            {
                BaseToolModel = new BaseToolModel();
                selectedCharacter.SkysteelModel.BaseToolModel = BaseToolModel;
            }
            else { BaseToolModel = selectedCharacter.SkysteelModel.BaseToolModel; }
        }


        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                if (job.BaseTool.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.BaseTool.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    SkysteelInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(CompletedFirstTool));
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

            SkysteelInfo.ProgressClass(selectedCharacter, tempJob.BaseTool, true);

            LoadAvailableJobs();

        }
        #endregion

       
        #endregion
    }
}
