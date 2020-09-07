using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Braves
{
    public class BravesViewModel : ObservableObject, IPageViewModel
    {
        private IEventAggregator iEventAggregator;
        private BravesModel bravesModel;
        private Character selectedCharacter;
        private ArrWeapon arrWeapon;
        private ObservableCollection<string> availableBravesJobs;

        public BravesViewModel(IEventAggregator iEventAggregator)
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

        public string Name => "Zodiac Braves";

        #region Properties

        #region ViewModel Properties
        public BravesModel BravesModel
        {
            get { return bravesModel; }
            set
            {
                bravesModel = value;
                OnPropertyChanged(nameof(BravesModel));
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
                    BravesModel = SelectedCharacter.ArrProgress.BravesModel;
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
                    CalculateTotals();
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

        public ObservableCollection<string> AvailableBravesJobs
        {
            get { return availableBravesJobs; }
            set
            {
                availableBravesJobs = value;
                OnPropertyChanged(nameof(AvailableBravesJobs));
            }
        }

        public string CurrentBraves
        {
            get { return BravesModel.CurrentBraves; }
            set
            {
                if (BravesModel.CurrentBraves != value) { ResetBools(); }
                

                BravesModel.CurrentBraves = value;
                OnPropertyChanged(nameof(CurrentBraves));
            }
        }

        public int RemainingGil
        {
            get { return BravesModel.RemainingGil; }
            set
            {
                BravesModel.RemainingGil = value;
                OnPropertyChanged(nameof(RemainingGil));
            }
        }

        public int RemainingSeals
        {
            get { return BravesModel.RemainingSeals; }
            set
            {
                BravesModel.RemainingSeals = value;
                OnPropertyChanged(nameof(RemainingSeals));
            }
        }

        public int RemainingPoetics
        {
            get { return BravesModel.RemainingPoetics; }
            set
            {
                BravesModel.RemainingPoetics = value;
                OnPropertyChanged(nameof(RemainingPoetics));
            }
        }


        #region Bools
        public bool FirstQuest
        {
            get { return BravesModel.FirstQuest; }
            set
            {
                BravesModel.FirstQuest = value;
                OnPropertyChanged(nameof(FirstQuest));
                CalculateTotals();
            }
        }
        public bool SecondQuest
        {
            get { return BravesModel.SecondQuest; }
            set
            {
                BravesModel.SecondQuest = value;
                OnPropertyChanged(nameof(SecondQuest));
                CalculateTotals();
            }
        }
        public bool ThirdQuest
        {
            get { return BravesModel.ThirdQuest; }
            set
            {
                BravesModel.ThirdQuest = value;
                OnPropertyChanged(nameof(ThirdQuest));
                CalculateTotals();
            }
        }
        public bool FourthQuest
        {
            get { return BravesModel.FourthQuest; }
            set
            {
                BravesModel.FourthQuest = value;
                OnPropertyChanged(nameof(FourthQuest));
                CalculateTotals();
            }
        }
        public bool FifthQuest
        {
            get { return BravesModel.FifthQuest; }
            set
            {
                BravesModel.FifthQuest = value;
                OnPropertyChanged(nameof(FifthQuest));
            }
        }

        #endregion
        #endregion

        #endregion
        #region Methods

        private int CountTrues(params bool[] booleans)
        {
            int result = 0;
            foreach (bool b in booleans)
            {
                if (!b) result++;
            }

            return result;
        }
        private void CalculateTotals()
        {
            int count = CountTrues(FirstQuest , SecondQuest , ThirdQuest , FourthQuest);

            RemainingGil = count * 100000;
            RemainingSeals = count * 20000;
            RemainingPoetics = count * 200;
        }

        private void ResetBools()
        {
            FirstQuest = false;
            SecondQuest = false;
            ThirdQuest = false;
            FourthQuest = false;
            FifthQuest = false;
        }

        private void LoadAvailableJobs()
        {
            AvailableBravesJobs = new ObservableCollection<string>();
            {

                foreach (ArrJobs job in ArrWeapon.JobList)
                {
                    if (job.Braves.Progress != ArrProgress.States.Completed)
                    {
                        AvailableBravesJobs.Add(job.Name);
                    }
                }
            }
        }
        #endregion

        #region Commands

        

        private ICommand _CompleteButton;

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

        private bool CanComplete() { return CurrentBraves != null; }
        private void CompleteCommand(object param)
        {

            ArrJobs tempJob = ArrWeapon.JobList[ArrWeapon.JobListString.IndexOf(CurrentBraves)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Braves,true);



            ResetBools();
            LoadAvailableJobs();
        }
        #endregion
    }
}
