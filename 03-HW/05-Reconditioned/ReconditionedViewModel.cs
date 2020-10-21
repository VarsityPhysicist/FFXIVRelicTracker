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

namespace FFXIVRelicTracker._03_HW._05_Reconditioned
{
    public class ReconditionedViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private ReconditionedModel reconditionedModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public ReconditionedViewModel()
        {
        }
        public ReconditionedViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Reconditioned";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    reconditionedModel = selectedCharacter.HWModel.ReconditionedModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }

        public string SelectedJob
        {
            get { return reconditionedModel.SelectedJob; }
            set
            {
                if (value != SelectedJob) { CurrentPoints = 0; Recalculate(); }
                reconditionedModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));

                OnPropertyChanged(nameof(AnimaWeapon));
                OnPropertyChanged(nameof(ReconditionedWeapon));
            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return reconditionedModel.AvailableJobs; }
            set
            {
                reconditionedModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public ReconditionedModel ReconditionedModel
        {
            get { return reconditionedModel; }
            set
            {
                if (value != null)
                {
                    reconditionedModel = value;
                    OnPropertyChanged(nameof(ReconditionedModel));
                }
            }
        }
        public string AnimaWeapon
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedJob)) { return "Anima Weapon"; }
                else { return HWInfo.ReturnAnimaWeaponName(SelectedJob); }
            }
        }
        public string ReconditionedWeapon
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedJob)) { return "Reconditioned Weapon"; }
                else { return "Reconditioned " + AnimaWeapon; }
            }
        }
        public int CurrentSand
        {
            get 
            {
                if (reconditionedModel.CurrentSand < 0) { CurrentSand = 0; }
                return reconditionedModel.CurrentSand; 
            }
            set
            {
                if (value < 0) { reconditionedModel.CurrentSand = 0; }
                else { reconditionedModel.CurrentSand = value; }
            }
        }
        public int CurrentUmbrite
        {
            get 
            {
                if (reconditionedModel.CurrentUmbrite < 0) { CurrentUmbrite = 0; }
                return reconditionedModel.CurrentUmbrite; 
            }
            set
            {
                if (value < 0) { reconditionedModel.CurrentUmbrite = 0; }
                else { reconditionedModel.CurrentUmbrite = value; }
            }
        }
        public int CurrentPoints 
        { 
            get 
            {
                if (reconditionedModel.CurrentPoints < 0) { CurrentPoints = 0; }
                else if (reconditionedModel.CurrentPoints > 240) { CurrentPoints = 240; }
                return reconditionedModel.CurrentPoints; 
            }
            set
            {
                if (value < 0) { reconditionedModel.CurrentPoints = 0; }
                else if (value > 240) { reconditionedModel.CurrentPoints = 240; }
                else { reconditionedModel.CurrentPoints = value; }
                OnPropertyChanged(nameof(CurrentPoints));
            }
        }

        public int TotalPoints
        {
            get
            {
                if (AvailableJobs == null) { LoadAvailableJobs(); }
                return (AvailableJobs.Count * 240) - CurrentPoints;
            }
        }

        public int CurrentMinUmbrite => Math.Max(0, CurrentMinPoints - CurrentUmbrite);
        public int CurrentMaxUmbrite => Math.Max(0, CurrentMaxPoints - CurrentUmbrite);
        public int CurrentMinSand => Math.Max(0, CurrentMinPoints - CurrentSand);
        public int CurrentMaxSand => Math.Max(0, CurrentMaxPoints - CurrentSand);

        public int CurrentMinPoints => (int)Math.Ceiling((240 - CurrentPoints) / 4.0);
        public int CurrentMaxPoints => (int)Math.Ceiling((240 - CurrentPoints) / 3.0);

        public int MinUmbrite => (int)Math.Ceiling(TotalPoints / 4.0) - CurrentUmbrite;
        public int MaxUmbrite => (int)Math.Ceiling(TotalPoints / 3.0) - CurrentUmbrite; 
        public int MinSands => (int)Math.Ceiling(TotalPoints / 4.0) - CurrentSand;
        public int MaxSands => (int)Math.Ceiling(TotalPoints / 3.0) - CurrentSand;

        public int MinPoetics => 75 * MinUmbrite;
        public int MaxPoetics => 75 * MaxUmbrite;
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            if (AvailableJobs == null) { AvailableJobs = new ObservableCollection<string>(); }
            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                if (job.Reconditioned.Progress == BaseProgressClass.States.Completed & AvailableJobs.Contains(job.Name))
                {
                    AvailableJobs.Remove(job.Name);
                }
                if (job.Reconditioned.Progress != BaseProgressClass.States.Completed & !AvailableJobs.Contains(job.Name))
                {
                    HWInfo.ReloadJobList(AvailableJobs, job.Name);
                }
            }
            OnPropertyChanged(nameof(AvailableJobs));
            OnPropertyChanged(nameof(AnimaWeapon));
            OnPropertyChanged(nameof(ReconditionedWeapon));
            Recalculate();
        }
        private void Recalculate()
        {
            OnPropertyChanged(nameof(CurrentSand));
            OnPropertyChanged(nameof(CurrentUmbrite));
            OnPropertyChanged(nameof(CurrentPoints));
            OnPropertyChanged(nameof(TotalPoints));
            OnPropertyChanged(nameof(MinUmbrite));
            OnPropertyChanged(nameof(MaxUmbrite)); 
            OnPropertyChanged(nameof(MinSands));
            OnPropertyChanged(nameof(MaxSands));
            OnPropertyChanged(nameof(MinPoetics));
            OnPropertyChanged(nameof(MaxPoetics));
            OnPropertyChanged(nameof(CurrentMinPoints));
            OnPropertyChanged(nameof(CurrentMaxPoints));
            OnPropertyChanged(nameof(CurrentMinUmbrite));
            OnPropertyChanged(nameof(CurrentMaxUmbrite));
            OnPropertyChanged(nameof(CurrentMinSand));
            OnPropertyChanged(nameof(CurrentMaxSand));
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

            HWStageCompleter.ProgressClass(selectedCharacter, tempJob.Reconditioned, true);

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
            string materialType = (string)param;
            switch (materialType)
            {
                case "Sand":
                    CurrentSand += 1;
                    break;
                case "Umbrite":
                    CurrentUmbrite += 1;
                    break;
                case "Point3":
                    CurrentPoints += 3;
                    CurrentSand -= 1;
                    CurrentUmbrite -= 1;
                    break;
                case "Point4":
                    CurrentPoints += 4;
                    CurrentSand -= 1;
                    CurrentUmbrite -= 1;
                    break;
                case "Point5":
                    CurrentPoints += 5;
                    CurrentSand -= 1;
                    CurrentUmbrite -= 1;
                    break;
                case "Point6":
                    CurrentPoints += 6;
                    CurrentSand -= 1;
                    CurrentUmbrite -= 1;
                    break;
            }
            Recalculate();
        }

        #endregion
        #endregion
    }
}
