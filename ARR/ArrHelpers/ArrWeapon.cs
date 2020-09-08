
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    public class ArrWeapon :ObservableObject
    {
       
        public ArrWeapon()
        {
        }

        #region Fields

        public List<ArrJobs> JobList = new List<ArrJobs>();

        private ArrJobs pld= new ArrJobs("PLD");
        private ArrJobs war= new ArrJobs("WAR");
        private ArrJobs whm= new ArrJobs("WHM");
        private ArrJobs sch= new ArrJobs("SCH");
        private ArrJobs mnk= new ArrJobs("MNK");
        private ArrJobs drg= new ArrJobs("DRG");
        private ArrJobs nin= new ArrJobs("NIN");
        private ArrJobs brd= new ArrJobs("BRD");
        private ArrJobs blm= new ArrJobs("BLM");
        private ArrJobs smn = new ArrJobs("SMN");

        #endregion

        #region Properties
        public ArrJobs PLD
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

        public ArrJobs WAR
        {
            get{return war;}
            set
                {
                war=value;
                OnPropertyChanged(nameof(WAR));
                }
        }
        public ArrJobs WHM
        {
            get{return whm;}
            set
                {
                whm = value;
                OnPropertyChanged(nameof(WHM));
                }
        }
        public ArrJobs SCH
        {
            get{return sch;}
            set
                {
                sch=value;
                OnPropertyChanged(nameof(SCH));
                }
        }
        public ArrJobs MNK
        {
            get{return mnk;}
            set
                {
                mnk=value;
                OnPropertyChanged(nameof(MNK));
                }
        }
        public ArrJobs DRG
        {
            get{return drg;}
            set
                {
                drg=value;
                OnPropertyChanged(nameof(DRG));
                }
        }
        public ArrJobs NIN
        {
            get{return nin;}
            set
                {
                nin=value;
                OnPropertyChanged(nameof(NIN));
                }
        }
        public ArrJobs BRD
        {
            get{return brd;}
            set
                {
                brd=value;
                OnPropertyChanged(nameof(BRD));
                }
        }
        public ArrJobs BLM
        {
            get{return blm;}
            set
                {
                blm=value;
                OnPropertyChanged(nameof(BLM));
                }
        }
        public ArrJobs SMN
        {
            get { return smn; }
            set
            {
                smn = value;
                OnPropertyChanged(nameof(SMN));
            }
        }

        public int Count => ((ICollection<ArrJobs>)JobList).Count;

        public bool IsReadOnly => ((ICollection<ArrJobs>)JobList).IsReadOnly;
        #endregion
    }
}
