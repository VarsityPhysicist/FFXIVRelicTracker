using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    public class ArrJobs : ObservableObject
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
        public ArrJobs()
        {
        }


        public ArrJobs(string name)
        {
            this.Name = name;

            relic = new ArrProgress("Relic",name);
            zenith = new ArrProgress("Zenith", name);
            atma = new ArrProgress("Atma", name);
            animus = new ArrProgress("Animus", name);
            novus = new ArrProgress("Novus", name);
            nexus = new ArrProgress("Nexus", name);
            braves = new ArrProgress("Braves", name);
            zeta = new ArrProgress("Zeta", name);
        }
        public List<ArrProgress> StageList = new List<ArrProgress>();

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
