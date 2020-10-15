using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_ShB._03_Recollection
{
    public class RecollectionModel : ObservableObject
    {
        #region Constructor
        public RecollectionModel()
        {

        }
        #endregion

        public ObservableCollection<string> AvailableJobs { get; set; }
        public string CurrentRecollection { get; set; }
        public int MemoryCount { get; set; }
    }
}
