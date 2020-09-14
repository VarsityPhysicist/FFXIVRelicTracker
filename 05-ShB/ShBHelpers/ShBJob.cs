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

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<ShBProgress> tempList = new List<ShBProgress>();

            foreach (ShBProgress skysteelProgress in StageList)
            {
                if (skysteelProgress == null)
                {
                    int stageIndex = StageList.IndexOf(skysteelProgress);

                    ShBProgress tempProgress = new ShBProgress(ShBInfo.StageListString[stageIndex], name);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Resistance = tempProgress;
                            break;
                    }
                }
                else { tempList.Add(skysteelProgress); }
            }

            StageList = tempList;

        }
        #endregion
    }
}
