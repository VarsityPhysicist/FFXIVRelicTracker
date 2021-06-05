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
            AugmentedResistance = new ShBProgress("AugmentedResistance", Name);
            Recollection = new ShBProgress("Recollection", Name);
            LawsOrder = new ShBProgress("LawsOrder", Name);
            AugmentedLawsOrder = new ShBProgress("AugmentedLawsOrder", Name);
            Blades = new ShBProgress("Blades", Name);
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
        private ShBProgress augmentedResistance;
        private ShBProgress recollection;
        private ShBProgress lawsOrder;
        private ShBProgress augmentedLawsOrder;
        private ShBProgress blades;

        public ShBProgress Resistance
        {
            get { return resistance; }
            set
            {
                resistance = value;
                OnPropertyChanged(nameof(Resistance));
            }
        }

        public ShBProgress AugmentedResistance
        {
            get { return augmentedResistance; }
            set
            {
                augmentedResistance = value;
                OnPropertyChanged(nameof(AugmentedResistance));
            }
        }

        public ShBProgress Recollection
        {
            get { return recollection; }
            set
            {
                recollection = value;
                OnPropertyChanged(nameof(Recollection));
            }
        }

        public ShBProgress LawsOrder
        {
            get { return lawsOrder; }
            set
            {
                lawsOrder = value;
                OnPropertyChanged(nameof(LawsOrder));
            }
        }

        public ShBProgress AugmentedLawsOrder
        {
            get { return augmentedLawsOrder; }
            set
            {
                augmentedLawsOrder = value;
                OnPropertyChanged(nameof(AugmentedLawsOrder));
            }
        }

        public ShBProgress Blades
        {
            get { return blades; }
            set
            {
                blades = value;
                OnPropertyChanged(nameof(Blades));
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

            for (int stageIndex = 0; stageIndex < StageList.Count; stageIndex++)
            {
                ShBProgress ShBProgress = new ShBProgress();
                if (StageList[stageIndex] != null) { ShBProgress = StageList[stageIndex]; }
                if (ShBProgress.Name == null)
                {
                    ShBProgress tempProgress = new ShBProgress(ShBInfo.StageListString[stageIndex], name);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Resistance = tempProgress;
                            break;
                        case 1:
                            AugmentedResistance = tempProgress;
                            break;
                        case 2:
                            Recollection = tempProgress;
                            break;
                        case 3:
                            LawsOrder = tempProgress;
                            break;
                        case 4:
                            AugmentedLawsOrder = tempProgress;
                            break;
                        case 5:
                            Blades = tempProgress;
                            break;
                    }
                }
                else { tempList.Add(ShBProgress); }
            }

            StageList = tempList;
        }
        #endregion
    }
}
