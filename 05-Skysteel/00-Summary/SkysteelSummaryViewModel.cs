using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel._00_Summary
{
    class SkysteelSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private SkysteelSummaryModel summaryModel;
        private Character selectedCharacter;
        private IEventAggregator eventAggregator;

        #endregion

        #region Constructors
        public SkysteelSummaryViewModel(IEventAggregator eventAggregator)
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
        public string Name => "Summary";
        public SkysteelSummaryModel SummaryModel
        {
            get { return summaryModel; }
            set
            {
                summaryModel = value;
                OnPropertyChanged(nameof(SummaryModel));
            }
        }
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                SummaryModel = new SkysteelSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        #region Expose Jobs to VM
        public SkysteelJob CRP{ get { return SelectedCharacter.SkysteelModel.CRP; } }
        public SkysteelJob BSM{ get { return SelectedCharacter.SkysteelModel.BSM; } }
        public SkysteelJob ARM{ get { return SelectedCharacter.SkysteelModel.ARM; } }
        public SkysteelJob GSM{ get { return SelectedCharacter.SkysteelModel.GSM; } }
        public SkysteelJob LTW{ get { return SelectedCharacter.SkysteelModel.LTW; } }
        public SkysteelJob WVR{ get { return SelectedCharacter.SkysteelModel.WVR; } }
        public SkysteelJob ALC{ get { return SelectedCharacter.SkysteelModel.ALC; } }
        public SkysteelJob CUL{ get { return SelectedCharacter.SkysteelModel.CUL; } }
        public SkysteelJob MIN{ get { return SelectedCharacter.SkysteelModel.MIN; } }
        public SkysteelJob BTM{ get { return SelectedCharacter.SkysteelModel.BTM; } }
        public SkysteelJob FSH{ get { return SelectedCharacter.SkysteelModel.FSH; } }

        #endregion
        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
        }
        #endregion

        #region Commands
        private ICommand _SkysteelButton;
        public ICommand ShBButton
        {
            get
            {
                if (_SkysteelButton == null)
                {
                    _SkysteelButton = new RelayCommand(
                        param => this.SkysteelCommand(param),
                        param => this.SkysteelCan()
                        );
                }
                return _SkysteelButton;
            }
        }

        private bool SkysteelCan() { return true; }
        private void SkysteelCommand(object param)
        {
            object[] values = (object[])param;

            //ShBJob tempJob = (ShBJob)values[0];
            SkysteelProgress tempProgress = (SkysteelProgress)values[0];

            SkysteelInfo.ProgressClass(selectedCharacter, tempProgress);

        }
        #endregion


    }
}
