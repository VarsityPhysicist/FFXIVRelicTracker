using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public class SkysteelJob :ObservableObject
    {
        #region Fields
        private string name;
        private SkysteelProgress baseTool;
        private SkysteelProgress basePlus1;
        private SkysteelProgress dragonsung;
        private SkysteelProgress augmentedDragonsung;
        private SkysteelProgress skysung;
        #endregion


        #region Constructors
        public SkysteelJob()
        {
        }

        public SkysteelJob(string name)
        {
            this.Name = name;

            BaseTool = new SkysteelProgress("BaseTool", name);
            BasePlus1 = new SkysteelProgress("BasePlus1", name);
            Dragonsung = new SkysteelProgress("Dragonsung", name);
            AugmentedDragonsung = new SkysteelProgress("AugmentedDragonsung", name);
            Skysung = new SkysteelProgress("Skysung", name);
        }
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

        public List<SkysteelProgress> StageList = new List<SkysteelProgress>();

        public SkysteelProgress BaseTool
        {
            get { return baseTool; }
            set
            {
                baseTool = value;
                OnPropertyChanged(nameof(BaseTool));
            }
        }
        public SkysteelProgress BasePlus1
        {
            get { return basePlus1; }
            set
            {
                basePlus1 = value;
                OnPropertyChanged(nameof(BasePlus1));
            }
        }
        public SkysteelProgress Dragonsung
        {
            get { return dragonsung; }
            set
            {
                dragonsung = value;
                OnPropertyChanged(nameof(Dragonsung));
            }
        }
        public SkysteelProgress AugmentedDragonsung
        {
            get { return augmentedDragonsung; }
            set
            {
                augmentedDragonsung = value;
                OnPropertyChanged(nameof(AugmentedDragonsung));
            }
        }
        public SkysteelProgress Skysung
        {
            get { return skysung; }
            set
            {
                skysung = value;
                OnPropertyChanged(nameof(Skysung));
            }
        }

        #endregion

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field


            List<SkysteelProgress> tempList = new List<SkysteelProgress>();

            for (int stageIndex = 0; stageIndex < StageList.Count; stageIndex++)
            {
                SkysteelProgress SkysteelProgress = new SkysteelProgress();
                if (StageList[stageIndex] != null) { SkysteelProgress = StageList[stageIndex]; }
                if (SkysteelProgress.Name == null)
                {
                    SkysteelProgress tempProgress = new SkysteelProgress(SkysteelInfo.StageListString[stageIndex], name);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            BaseTool = tempProgress;
                            break;
                        case 1:
                            BasePlus1 = tempProgress;
                            break;
                        case 2:
                            Dragonsung = tempProgress;
                            break;
                        case 3:
                            AugmentedDragonsung = tempProgress;
                            break;
                        case 4:
                            Skysung = tempProgress;
                            break;
                    }
                }
                else { tempList.Add(SkysteelProgress); }
            }
            StageList = tempList;

        }
        #endregion

    }
}
