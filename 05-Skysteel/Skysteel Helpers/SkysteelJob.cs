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

        #endregion

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<SkysteelProgress> tempList = new List<SkysteelProgress>();

            foreach(SkysteelProgress skysteelProgress in StageList)
            {
                if (skysteelProgress == null)
                {
                    int stageIndex = StageList.IndexOf(skysteelProgress);

                    SkysteelProgress tempProgress = new SkysteelProgress(SkysteelInfo.StageListString[stageIndex],name);

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
                    }
                }
                else { tempList.Add(skysteelProgress); }
            }

            StageList = tempList;

        }
        #endregion

    }
}
