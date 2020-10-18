using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW._04_Hyperconductive
{
    public class HyperconduvtiveViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private HyperconductiveModel hyperconductiveModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;
        #endregion

        #region Constructor
        public HyperconduvtiveViewModel()
        {
        }
        public HyperconduvtiveViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Hyperconductive";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    HyperconductiveModel = selectedCharacter.HWModel.HyperconductiveModel;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }

            }
        }

        public string SelectedJob
        {
            get { return HyperconductiveModel.SelectedJob; }
            set
            {
                HyperconductiveModel.SelectedJob = value;
                OnPropertyChanged(nameof(SelectedJob));

            }
        }

        public ObservableCollection<string> AvailableJobs
        {
            get { return HyperconductiveModel.AvailableJobs; }
            set
            {
                HyperconductiveModel.AvailableJobs = value;
                OnPropertyChanged(nameof(AvailableJobs));
            }
        }

        public HyperconductiveModel HyperconductiveModel
        {
            get { return HyperconductiveModel; }
            set
            {
                if (value != null)
                {
                    HyperconductiveModel = value;
                    OnPropertyChanged(nameof(HyperconductiveModel));
                }
            }
        }
        
        public int OilCount { get { return hyperconductiveModel.OilCount; } set { if (value >= 0) { hyperconductiveModel.OilCount = value; } else { hyperconductiveModel.OilCount = 0; } OilChange(); } }
        #endregion

        #region Methods

        private void OilChange()
        {
            OnPropertyChanged(nameof(OilCount));
        }

        public void LoadAvailableJobs()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Commands

        #endregion


    }
}
