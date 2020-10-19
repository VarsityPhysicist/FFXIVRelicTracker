using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
                reconditionedModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));
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
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Commands

        #endregion
    }
}
