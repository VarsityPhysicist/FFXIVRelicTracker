using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Navigation;

namespace FFXIVRelicTracker.ARR.Atma
{
    class AtmaViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private Character selectedCharacter;
        private ArrWeapon arrWeapon;
        private AtmaModel atmaModel;
        #endregion

        public AtmaViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            SubscriptionToken subscriptionToken =this.eventAggregator.GetEvent<PubSubEvent<Character>>().Subscribe((details) =>{this.SelectedCharacter = details;});
            eventAggregator.GetEvent<PubSubEvent<ArrWeapon>>().Subscribe((details) => { this.ArrWeapon = details; });

        }

        #region Properties
        public string Name => "Atma";
        public ArrWeapon ArrWeapon
        {
            get { return arrWeapon; }
            set
            {
                if (value != null)
                {
                    arrWeapon = value;
                    CountAtma();
                    OnPropertyChanged(nameof(ArrWeapon));
                }
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                AtmaModel = SelectedCharacter.ArrProgress.AtmaModel;
                OnPropertyChanged(nameof(SelectedCharacter));
                if (value != null)
                {
                    ArrWeapon = SelectedCharacter.ArrProgress.Arr;

                }
            }
        }
        public AtmaModel AtmaModel
        {
            get { return atmaModel; }
            set
            {
                atmaModel = value;
                OnPropertyChanged(nameof(AtmaModel));
            }
        }
        public int NeededAtmas
        {
            get { return AtmaModel.neededAtmas; }
            set
            {
                atmaModel.neededAtmas = value;
                OnPropertyChanged(nameof(NeededAtmas));
            }
        }
        #endregion

        private void CountAtma()
        {
            int atmaCount = 0;
            foreach (ArrStages job in arrWeapon)
            {
                if (job.Atma.Progress != ArrProgress.States.Completed)
                {
                    atmaCount++;
                }
            }
            NeededAtmas = atmaCount;
        }
    }
}
