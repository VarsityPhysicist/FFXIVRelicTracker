using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Relic
{
    class RelicViewModel : ObservableObject, IPageViewModel
    {
        public string Name
        {
            get
            {
                return "Relic";
            }
        }
        private IEventAggregator eventAggregator;

        public RelicViewModel(IEventAggregator eventAggregator)
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
            eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Subscribe((details) => { this.ArrWeapon = details; });
        }

        public RelicModel RelicModel
        {
            get { return relicModel; }
            set
            {
                relicModel = value;
                OnPropertyChanged(nameof(RelicModel));
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
                    RelicModel = selectedCharacter.ArrProgress.RelicModel;
                    ArrWeapon = SelectedCharacter.ArrProgress.ArrWeapon;
                    LoadAvailableJobs();
                }
            }
        }
        public string CurrentRelic
        {
            get { return relicModel.CurrentRelic; }
            set
            {
                relicModel.CurrentRelic = value;
                
                if (value != null)
                {
                    RelicMap = ArrInfo.ReturnRelicMap(CurrentRelic);
                    RelicDestination = ArrInfo.ReturnRelicDestination(CurrentRelic);
                    RelicPoint = ArrInfo.ReturnRelicPoint(CurrentRelic);
                    RelicMateria = ArrInfo.ReturnMateria(CurrentRelic);
                    RelicClassWeapon = ArrInfo.ReturnClassWeapon(CurrentRelic);

                    List<string> templist = ArrInfo.ReturnBeastmen(CurrentRelic);

                    RelicBeastmen1 = templist[0];
                    RelicBeastmen2 = templist[1];
                    RelicBeastmen3 = templist[2];

                    RelicVisibility = Visibility.Visible;
                }
                else
                {
                    RelicVisibility = Visibility.Hidden;

                }

                OnPropertyChanged(nameof(CurrentRelic));
                OnPropertyChanged(nameof(CompleteButton));
            }
        }
        public int RelicIndex
        {
            get { return relicModel.RelicIndex; }
            set
            {
                relicModel.RelicIndex = value;
                OnPropertyChanged(nameof(RelicIndex));
            }
        }
        public Visibility RelicVisibility
        {
            get { return relicModel.RelicVisibility; }
            set
            {
                relicModel.RelicVisibility = value;
                OnPropertyChanged(nameof(RelicVisibility));
            }
        }
        public string RelicDestination
        {
            get { return relicModel.RelicDestination; }
            set
            {
                relicModel.RelicDestination = value;
                OnPropertyChanged(nameof(RelicDestination));
            }
        }
        public string RelicClassWeapon
        {
            get { return relicModel.RelicClassWeapon; }
            set
            {
                relicModel.RelicClassWeapon = value;
                OnPropertyChanged(nameof(RelicClassWeapon));
            }
        }
        public string RelicBeastmen1
        {
            get { return relicModel.RelicBeastmen1; }
            set
            {
                relicModel.RelicBeastmen1 = value;
                OnPropertyChanged(nameof(RelicBeastmen1));
            }
        }
        public string RelicBeastmen2
        {
            get { return relicModel.RelicBeastmen2; }
            set
            {
                relicModel.RelicBeastmen2 = value;
                OnPropertyChanged(nameof(RelicBeastmen2));
            }
        }
        public string RelicBeastmen3
        {
            get { return relicModel.RelicBeastmen3; }
            set
            {
                relicModel.RelicBeastmen3 = value;
                OnPropertyChanged(nameof(RelicBeastmen3));
            }
        }
        public string RelicMap
        {
            get { return relicModel.RelicMap; }
            set
            {
                relicModel.RelicMap = value;
                OnPropertyChanged(nameof(RelicMap));
            }
        }
        public PointF RelicPoint
        {
            get { return relicModel.RelicPoint; }
            set
            {
                relicModel.RelicPoint = value;
                OnPropertyChanged(nameof(RelicPoint));
            }
        }
        public string RelicMateria
        {
            get { return relicModel.RelicMateria; }
            set
            {
                relicModel.RelicMateria = value;
                OnPropertyChanged(nameof(RelicMateria));
            }
        }
        public ObservableCollection<string> AvailableRelicJobs
        {
            get { return relicModel.availableRelicJobs; }
            set
            {
                relicModel.availableRelicJobs = value;
                OnPropertyChanged(nameof(AvailableRelicJobs));
            }
        }
        public ArrWeapon ArrWeapon
        {
            get { return arrWeapon; }
            set
            {
                if (value != null)
                {
                    //LoadAvailableJobs();

                    arrWeapon = value;
                    OnPropertyChanged(nameof(ArrWeapon));
                    LoadAvailableJobs();
                }
            }
        }
        public List<ArrProgress> JobRelics
        {
            get { return relicModel.JobRelics; }
            set
            {
                relicModel.JobRelics = value;
                OnPropertyChanged(nameof(JobRelics));
            }
        }

        private void LoadAvailableJobs()
        {
            AvailableRelicJobs=new ObservableCollection<string>();
            {

                foreach(ArrJobs job in arrWeapon.JobList)
                {
                    if (job.Relic.Progress != ArrProgress.States.Completed)
                    {
                        AvailableRelicJobs.Add(job.Name);
                    }
                }
            }
        }
        
        private ICommand _RelicButton;
        private ArrWeapon arrWeapon;
        private Character selectedCharacter;
        private RelicModel relicModel;

        #region CompleteRelic Command
        public ICommand CompleteButton
        {
            get
            {
                if (_RelicButton == null)
                {
                    _RelicButton = new RelayCommand(
                        param => this.RelicCommand(),
                        param => this.RelicCan()
                        );
                }
                return _RelicButton;
            }
        }

        private bool RelicCan() { return CurrentRelic!=null; }
        private void RelicCommand()
        {
            ArrJobs tempJob = ArrWeapon.JobList[ArrInfo.JobListString.IndexOf(CurrentRelic)];

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempJob.Relic, true);

            AvailableRelicJobs.RemoveAt(RelicIndex);
            RelicIndex = 0;
        }
        #endregion

    }
}
