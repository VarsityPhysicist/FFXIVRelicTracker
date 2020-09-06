using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFXIVRelicTracker.ARR.ARR
{

    public class ArrStages : ObservableObject
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public ArrStages()
        {

        }
        public ArrStages(string name)
            :this()
        {

            this.Name = name;

            Relic = new ArrProgress();
            Zenith = new ArrProgress();
            Atma = new ArrProgress();
            Animus = new ArrProgress();
            Novus = new ArrProgress();
            Nexus = new ArrProgress();
            Braves = new ArrProgress();
            Zeta = new ArrProgress();

            StageList = new List<ArrProgress>
            {
                Relic,
                Zenith,
                Atma,
                Animus,
                Novus,
                Nexus,
                Braves,
                Zeta
            };
        }
        public List<ArrProgress> StageList;

        private ArrProgress relic;
        private ArrProgress zenith;
        private ArrProgress atma;
        private ArrProgress animus;
        private ArrProgress novus;
        private ArrProgress nexus;
        private ArrProgress braves;
        private ArrProgress zeta;

        public ArrProgress Relic
        {
            get { return relic; }
            set
            {relic = value; OnPropertyChanged(nameof(Relic)); }
        }
        public ArrProgress Zenith
        {
            get { return zenith; }
            set { zenith = value; OnPropertyChanged(nameof(Zenith)); }
        }
        public ArrProgress Atma
        {
            get { return atma; }
            set { atma = value; OnPropertyChanged(nameof(Atma)); }
        }
        public ArrProgress Animus
        {
            get { return animus; }
            set { animus = value; OnPropertyChanged(nameof(Animus)); }
        }
        public ArrProgress Novus
        {
            get { return novus; }
            set { novus = value; OnPropertyChanged(nameof(Novus)); }
        }
        public ArrProgress Nexus
        {
            get { return nexus; }
            set { nexus = value; OnPropertyChanged(nameof(Nexus)); }
        }
        public ArrProgress Braves
        {
            get { return braves; }
            set { braves = value; OnPropertyChanged(nameof(Braves)); }
        }
        public ArrProgress Zeta
        {
            get { return zeta; }
            set { zeta = value; OnPropertyChanged(nameof(Zeta)); }
        }
    }
}
