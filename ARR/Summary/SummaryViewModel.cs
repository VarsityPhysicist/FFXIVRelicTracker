using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
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

        public string Name {get{return "Summary";}}

        #region ArrButton Command
        private ICommand _ArrButton;
        public ICommand ArrButton
        {
            get
            {
                if (_ArrButton == null)
                {
                    _ArrButton = new RelayCommand(
                        param => this.ArrCommand(param),
                        param => this.ArrCan()
                        );
                }
                return _ArrButton;
            }
        }

        private bool ArrCan() { return true; }
        private void ArrCommand(object param)
        {
            Object[] values = (object[])param;

            string jobInfo = (string)values[0];

            ArrJobs tempJob = SummaryModel.JobDictionary[jobInfo].Key;
            ArrProgress tempProgress= SummaryModel.JobDictionary[jobInfo].Value;

            ArrStageCompleter.ProgressClass(selectedCharacter, tempJob, tempProgress);

        }

        #endregion
    }
}
