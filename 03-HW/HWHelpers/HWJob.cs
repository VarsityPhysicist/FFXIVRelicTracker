using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public class HWJob : ObservableObject
    {
        #region Constructors
        public HWJob()
        {

        }

        public HWJob(string name)
        {
            this.name = name;

            Animated = new HWProgress("Animated", Name);
            Awoken = new HWProgress("Awoken", Name);
            Anima = new HWProgress("Anima", Name);
            Hyperconductive = new HWProgress("Hyperconductive", Name);
            Reconditioned = new HWProgress("Reconditioned", Name);
            Sharpened = new HWProgress("Sharpened", Name);
            Complete = new HWProgress("Complete", Name);
            Lux = new HWProgress("Lux", Name);
        }

        #endregion

        #region Fields
        private string name;
        private HWProgress animated;
        private HWProgress awoken;
        private HWProgress anima;
        private HWProgress hyperconductive;
        private HWProgress reconditioned;
        private HWProgress sharpened;
        private HWProgress complete;
        private HWProgress lux;
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

        public List<HWProgress> StageList = new List<HWProgress>();

        public HWProgress Animated
        {
            get { return animated; }
            set
            {
                animated = value;
                OnPropertyChanged(nameof(Animated));
            }
        }
        public HWProgress Awoken
        {
            get { return awoken; }
            set
            {
                awoken = value;
                OnPropertyChanged(nameof(Awoken));
            }
        }
        public HWProgress Anima
        {
            get { return anima; }
            set
            {
                anima = value;
                OnPropertyChanged(nameof(Anima));
            }
        }
        public HWProgress Hyperconductive
        {
            get { return hyperconductive; }
            set
            {
                hyperconductive = value;
                OnPropertyChanged(nameof(Hyperconductive));
            }
        }
        public HWProgress Reconditioned
        {
            get { return reconditioned; }
            set
            {
                reconditioned = value;
                OnPropertyChanged(nameof(Reconditioned));
            }
        }
        public HWProgress Sharpened
        {
            get { return sharpened; }
            set
            {
                sharpened = value;
                OnPropertyChanged(nameof(Sharpened));
            }
        }
        public HWProgress Complete
        {
            get { return complete; }
            set
            {
                complete = value;
                OnPropertyChanged(nameof(Complete));
            }
        }
        public HWProgress Lux
        {
            get { return lux; }
            set
            {
                lux = value;
                OnPropertyChanged(nameof(Lux));
            }
        }
        #endregion

        #region Methods
        public void CheckObject()
        {
            //This method is used to fix the situation where an existing character is loaded and a new stage was added since the character was initially created
            //Without checking and replacing the Progress lists and objects, the Progress object is null, regardless of the initiator being in the class constructor
            //  or in the field

            List<HWProgress> tempList = new List<HWProgress>();

            foreach (HWProgress skysteelProgress in StageList)
            {
                if (skysteelProgress == null)
                {
                    int stageIndex = StageList.IndexOf(skysteelProgress);

                    HWProgress tempProgress = new HWProgress(HWInfo.StageListString[stageIndex], name);

                    tempList.Add(tempProgress);

                    switch (stageIndex)
                    {
                        case 0:
                            Animated = tempProgress;
                            break;
                        case 1:
                            Awoken = tempProgress;
                            break;
                        case 2:
                            Anima = tempProgress;
                            break;
                        case 3:
                            Hyperconductive = tempProgress;
                            break;
                        case 4:
                            Reconditioned = tempProgress;
                            break;
                        case 5:
                            Sharpened = tempProgress;
                            break;
                        case 6:
                            Complete = tempProgress;
                            break;
                        case 7:
                            Lux = tempProgress;
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
