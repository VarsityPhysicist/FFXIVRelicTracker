using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Text;
using System.Windows.Documents;
using System.Windows.Input;

namespace FFXIVRelicTracker.ARR.Summary
{
    class SummaryViewModel : ObservableObject, IPageViewModel
    {
        private IEventAggregator eventAggregator;

        public SummaryViewModel(IEventAggregator eventAggregator)
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

        private SummaryModel summaryModel;
        public SummaryModel SummaryModel
        {
            get { return summaryModel; }
            set
            {
                summaryModel = value;
                OnPropertyChanged(nameof(SummaryModel));
            }
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                SummaryModel = new SummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }

        public string SummaryLayout
        {
            get { return selectedCharacter.ArrLayout; }
            set { selectedCharacter.ArrLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        public string Name {get{return "Summary";}}

        #region Expose Job Objects to VM
        public ArrJobs PLD { get { return SelectedCharacter.ArrProgress.ArrWeapon.PLD; } }
        public ArrJobs WAR { get { return SelectedCharacter.ArrProgress.ArrWeapon.WAR; } }
        public ArrJobs WHM { get { return SelectedCharacter.ArrProgress.ArrWeapon.WHM; } }
        public ArrJobs SCH { get { return SelectedCharacter.ArrProgress.ArrWeapon.SCH; } }
        public ArrJobs MNK { get { return SelectedCharacter.ArrProgress.ArrWeapon.MNK; } }
        public ArrJobs DRG { get { return SelectedCharacter.ArrProgress.ArrWeapon.DRG; } }
        public ArrJobs NIN { get { return SelectedCharacter.ArrProgress.ArrWeapon.NIN; } }
        public ArrJobs BRD { get { return SelectedCharacter.ArrProgress.ArrWeapon.BRD; } }
        public ArrJobs BLM { get { return SelectedCharacter.ArrProgress.ArrWeapon.BLM; } }
        public ArrJobs SMN { get { return SelectedCharacter.ArrProgress.ArrWeapon.SMN; } }
        #endregion

        #region ArrButton Command
        private ICommand _SummaryClick;
        public ICommand SummaryClick
        {
            get
            {
                if (_SummaryClick == null)
                {
                    _SummaryClick = new RelayCommand(
                        param => this.ArrCommand(param),
                        param => this.ArrCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool ArrCan() { return true; }
        private void ArrCommand(object param)
        {
            //Object[] values = (object[])param;

            //string jobInfo = (string)values[0];

            //ArrJobs tempJob = SummaryModel.JobDictionary[jobInfo].Key;
            //ArrProgress tempProgress= SummaryModel.JobDictionary[jobInfo].Value;

            ArrProgress tempProgress = (ArrProgress)param;

            ArrStageCompleter.ProgressClass(selectedCharacter,tempProgress);

        }

        public void LoadAvailableJobs()
        {
        }

        #endregion
    }
}
