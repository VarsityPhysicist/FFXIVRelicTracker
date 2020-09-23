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
using System.Windows.Navigation;

namespace FFXIVRelicTracker._03_HW._02_Awoken
{
    public class AwokenViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private AwokenModel awokenModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructors
        public AwokenViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Awoken";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    AwokenModel = selectedCharacter.HWModel.AwokenModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return awokenModel.SelectedJob; }
            set
            {
                awokenModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
                OnPropertyChanged(nameof(SelectedWeapon));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return awokenModel.AvailableJobs; }
            set
            {
                awokenModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public AwokenModel AwokenModel
        {
            get { return awokenModel; }
            set
            {
                if (value != null)
                {
                    awokenModel = value;
                    OnPropertyChanged(nameof(AwokenModel));
                }
            }
        }

        public string SelectedWeapon
        {
            //Check if saving+loading does not set this as expected
            get
            {
                if(SelectedJob==null | SelectedJob == "") { return "Animated Weapon"; }
                else { return HWInfo.ReturnAnimatedWeaponName(SelectedJob); }
            }
        }

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Awoken.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Awoken.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
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

            HWInfo.ProgressClass(selectedCharacter, tempJob.Awoken, true);

            LoadAvailableJobs();
        }
        #endregion

        #endregion


    }
}
