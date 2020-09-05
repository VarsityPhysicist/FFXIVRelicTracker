using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
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

            ArrStages tempStage= SummaryModel.JobDictionary[(string)values[0]].Key;
            ArrProgress tempProgress= SummaryModel.JobDictionary[(string)values[0]].Value;

            int StageIndex = tempStage.StageList.IndexOf(tempProgress);

            if (tempProgress.Progress == ArrProgress.States.NA)
            {
                CompletePreviousStages(tempStage, StageIndex);
                this.eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Publish(SelectedCharacter.ArrProgress.Arr);
            }
            else if (tempProgress.Progress == ArrProgress.States.Completed)
            {
                InCompleteFollowingStages(tempStage, StageIndex);
                this.eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Publish(SelectedCharacter.ArrProgress.Arr);
                return;
            }
            if (tempProgress.Progress == ArrProgress.States.Initiated)
            {
                tempProgress.Progress = ArrProgress.States.Completed;
                this.eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Publish(SelectedCharacter.ArrProgress.Arr);
            }
            else
            {
                switch (StageIndex)
                {
                    case 0:
                    case 3:
                    case 6:
                    case 5:
                    case 7:
                        tempProgress.Progress++;
                        if (tempProgress.Progress == ArrProgress.States.Initiated) { IncompleteOtherJobs(tempStage, StageIndex); }
                        break;
                    case 1:
                    case 2:
                    case 4:
                        tempProgress.Progress = ArrProgress.States.Completed;
                        this.eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Publish(SelectedCharacter.ArrProgress.Arr);
                        break;
                }
            }

        }

        private void IncompleteOtherJobs(ArrStages tempStage, int StageIndex)
        {
            foreach(ArrStages Job in SelectedCharacter.ArrProgress.Arr.JobList)
            {
                if (Job != tempStage)
                {
                    ArrProgress stage = Job.StageList[StageIndex];
                    if (stage.Progress == ArrProgress.States.Initiated) { stage.Progress = ArrProgress.States.NA; }
                }
            }
        }

        private void InCompleteFollowingStages(ArrStages tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = ArrProgress.States.NA;
            }
        }

        private void CompletePreviousStages(ArrStages tempStage, int stageIndex)
        {
            for(int i=0;i<stageIndex; i++)
            {
                tempStage.StageList[i].Progress = ArrProgress.States.Completed;
            }
        }

        #endregion
    }
}
