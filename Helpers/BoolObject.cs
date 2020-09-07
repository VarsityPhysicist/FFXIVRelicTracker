using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.Helpers
{
    
    public class BoolObject : ObservableObject
    {
        private bool internalBool;
        public BoolObject()
        {
            internalBool = false;
        }
        public bool Bool
        {
            get { return internalBool; }
            set
            {
                internalBool = value;
                OnPropertyChanged(nameof(Bool));
            }
        }
    }
}
