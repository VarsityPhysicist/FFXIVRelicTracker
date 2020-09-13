using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_ShB.ShBHelpers
{
    public class ShBJob : ObservableObject
    {
        #region Constructors
        public ShBJob()
        {

        }

        public ShBJob(string name)
        {
            this.name = name;
            Resistance = new ShBProgress("Resistance",Name);
        }

        #endregion

        #region Fields
        private string name;
        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public List<ShBProgress> StageList = new List<ShBProgress>();
        private ShBProgress resistance;

        public ShBProgress Resistance
        {
            get { return resistance; }
            set
            {
                resistance = value;
                OnPropertyChanged(nameof(Resistance));
            }
        }
        #endregion
    }
}
