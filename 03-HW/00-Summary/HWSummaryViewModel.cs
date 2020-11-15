using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._03_HW._00_Summary
{
    public class HWSummaryViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator eventAggregator;
        private HWSummaryModel summaryModel;
        private Character selectedCharacter;
        #endregion

        #region Constructor

        public HWSummaryViewModel(IEventAggregator eventAggregator)
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
        public HWSummaryModel SummaryModel
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
                SummaryModel = new HWSummaryModel(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
            }
        }
        public string SummaryLayout
        {
            get { return selectedCharacter.HWLayout; }
            set { selectedCharacter.HWLayout = value; OnPropertyChanged(nameof(SummaryLayout)); }
        }
        public List<string> Summaries { get { return new List<string> { "Horizontal", "Vertical" }; } }

        #region Expose Job Objects to VM
        public HWJob PLD { get { return SelectedCharacter.HWModel.PLD; } }
        public HWJob WAR { get { return SelectedCharacter.HWModel.WAR; } }
        public HWJob DRK { get { return SelectedCharacter.HWModel.DRK; } }
        public HWJob WHM { get { return SelectedCharacter.HWModel.WHM; } }
        public HWJob SCH { get { return SelectedCharacter.HWModel.SCH; } }
        public HWJob AST { get { return SelectedCharacter.HWModel.AST; } }
        public HWJob MNK { get { return SelectedCharacter.HWModel.MNK; } }
        public HWJob DRG { get { return SelectedCharacter.HWModel.DRG; } }
        public HWJob NIN { get { return SelectedCharacter.HWModel.NIN; } }
        public HWJob BRD { get { return SelectedCharacter.HWModel.BRD; } }
        public HWJob MCH { get { return SelectedCharacter.HWModel.MCH; } }
        public HWJob BLM { get { return SelectedCharacter.HWModel.BLM; } }
        public HWJob SMN { get { return SelectedCharacter.HWModel.SMN; } }
        #endregion
        #endregion

        #region Methods

        public void LoadAvailableJobs()
        {
        }
        #endregion

        #region Commands
        private ICommand _SummaryClick;
        public ICommand SummaryClick
        {
            get
            {
                if (_SummaryClick == null)
                {
                    _SummaryClick = new RelayCommand(
                        param => this.HWCommand(param),
                        param => this.HWCan()
                        );
                }
                return _SummaryClick;
            }
        }

        private bool HWCan() { return true; }
        private void HWCommand(object param)
        {
            //HWProgress values = (HWProgress)param;

            //HWJob tempJob = (HWJob)values[0];
            HWProgress tempProgress = (HWProgress)param;

            HWInfo.ProgressClass(selectedCharacter, tempProgress);

        }
        #endregion
    }
}