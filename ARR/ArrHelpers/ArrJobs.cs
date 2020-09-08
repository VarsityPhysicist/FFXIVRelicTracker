using FFXIVRelicTracker.Models.Helpers;
using Newtonsoft.Json;
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
            :this()
        {
                     
            this.Name = name;

        }
        public List<ArrProgress> StageList = new List<ArrProgress>();

        private ArrProgress relic = new ArrProgress();
        private ArrProgress zenith = new ArrProgress();
        private ArrProgress atma = new ArrProgress();
        private ArrProgress animus = new ArrProgress();
        private ArrProgress novus = new ArrProgress();
        private ArrProgress nexus = new ArrProgress();
        private ArrProgress braves = new ArrProgress();
        private ArrProgress zeta = new ArrProgress();

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
