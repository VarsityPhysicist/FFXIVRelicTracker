using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.Helpers
{
    public class BoolObject : ObservableObject
    {
        private bool internalBool;
        internal string Name;
        public BoolObject()
        {
            internalBool = false;
        }
        public BoolObject(string name) 
            :this()
        {
            Name = name;
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
