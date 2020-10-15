using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._02_AugmentedResistance
{
    public class AugmentedResistanceModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Constructor
        public AugmentedResistanceModel()
        {

        }

        public string CurrentAugmentedResistance { get; set; }
        public ObservableCollection<string> AvailableJobs { get;  set; }
        public int HarrowingCount { get;  set; }
        public int TorturedCount { get;  set; }
        public int SorrowfulCount { get;  set; }
        #endregion

        #region Properties

        #endregion
    }
}
