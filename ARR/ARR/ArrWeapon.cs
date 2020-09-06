using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FFXIVRelicTracker.Models.ARR
{
    public class ArrWeapon :ObservableObject,IEnumerable
    {
        #region Enumeration
        public void Add(System.Object Obj)
        {

        }
        public IEnumerator GetEnumerator()
        {
            return new JobEnumerator(this);
        }

        #endregion

        public ArrWeapon()
        {
            PLD = new ArrStages("PLD");
            WAR = new ArrStages("WAR");
            WHM = new ArrStages("WHM");
            SCH = new ArrStages("SCH");
            MNK = new ArrStages("MNK");
            DRG = new ArrStages("DRG");
            NIN = new ArrStages("NIN");
            BRD = new ArrStages("BRD");
            BLM = new ArrStages("BLM");
            SMN = new ArrStages("SMN");

            JobList = new List<ArrStages>
            {
               PLD,
               WAR,
               WHM,
               SCH,
               MNK,
               DRG,
               NIN,
               BRD,
               BLM,
               SMN
            };

            JobListString = new List<string>
            {
               "PLD",
               "WAR",
               "WHM",
               "SCH",
               "MNK",
               "DRG",
               "NIN",
               "BRD",
               "BLM",
               "SMN"
            };
        }

        #region Fields

        public List<ArrStages> JobList;
        public List<string> JobListString;

        private ArrStages pld;
        private ArrStages war;
        private ArrStages whm;
        private ArrStages sch;
        private ArrStages mnk;
        private ArrStages drg;
        private ArrStages nin;
        private ArrStages brd;
        private ArrStages blm;
        private ArrStages smn;

        #endregion

        #region Properties
        public ArrStages PLD
        {
            get { return pld; }
            set
            {
                if (value != null)
                {
                    pld = value;
                    OnPropertyChanged(nameof(PLD));
                }
            }
        }

        public ArrStages WAR
        {
            get{return war;}
            set
                {
                war=value;
                OnPropertyChanged(nameof(WAR));
                }
        }
        public ArrStages WHM
        {
            get{return whm;}
            set
                {
                whm = value;
                OnPropertyChanged(nameof(WHM));
                }
        }
        public ArrStages SCH
        {
            get{return sch;}
            set
                {
                sch=value;
                OnPropertyChanged(nameof(SCH));
                }
        }
        public ArrStages MNK
        {
            get{return mnk;}
            set
                {
                mnk=value;
                OnPropertyChanged(nameof(MNK));
                }
        }
        public ArrStages DRG
        {
            get{return drg;}
            set
                {
                drg=value;
                OnPropertyChanged(nameof(DRG));
                }
        }
        public ArrStages NIN
        {
            get{return nin;}
            set
                {
                nin=value;
                OnPropertyChanged(nameof(NIN));
                }
        }
        public ArrStages BRD
        {
            get{return brd;}
            set
                {
                brd=value;
                OnPropertyChanged(nameof(BRD));
                }
        }
        public ArrStages BLM
        {
            get{return blm;}
            set
                {
                blm=value;
                OnPropertyChanged(nameof(BLM));
                }
        }
        public ArrStages SMN
        {
            get { return smn; }
            set
            {
                smn = value;
                OnPropertyChanged(nameof(SMN));
            }
        }
        #endregion
    }

    public class JobEnumerator : IEnumerator
    {
        private ArrWeapon parent;
        int position = -1;

        public JobEnumerator(ArrWeapon parent)
        {
            this.parent = parent;
        }

        public bool MoveNext()
        {
            position++;
            return (position < parent.JobList.Count);
        }

        public void Reset()
        {
            position = 0;
        }

        public object Current => parent.JobList[position];
    }
}
