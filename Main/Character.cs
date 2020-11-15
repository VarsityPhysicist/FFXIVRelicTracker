using FFXIVRelicTracker._03_HW.Main;
using FFXIVRelicTracker._04_SB.Main;
using FFXIVRelicTracker._05_ShB.Main;
using FFXIVRelicTracker._05_Skysteel.Main;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace FFXIVRelicTracker.Models
{
    
    
    public class Character : ObservableObject
    {
        private string name;
        private string server;
        private ArrModel arrProgress;
        private ShBModel shbModel;
        private SkysteelModel skysteelModel;
        private HWModel hwModel;
        private SBModel sbModel;

        #region Properties

        public string ArrLayout { get; set; }
        public string HWLayout { get; set; }
        public string SBLayout { get; set; }
        public string ShBLayout { get; set; }
        public string SkysteelLayout { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Server
        {
            get { return server; }
            set
            {
                if (server != value)
                {
                    server = value;
                    OnPropertyChanged(nameof(Server));
                }
            }
        }
        public ArrModel ArrProgress
        {
            get { return arrProgress; }
            set
            {
                if (value != null)
                {
                    arrProgress = value;
                    OnPropertyChanged(nameof(ArrProgress));
                }
            }
        }
        public HWModel HWModel
        {
            get { return hwModel; }
            set
            {
                if (value != null)
                {
                    hwModel = value;
                    OnPropertyChanged(nameof(HWModel));
                }
            }
        }
        public SBModel SBModel
        {
            get { return sbModel; }
            set
            {
                if (value != null)
                {
                    sbModel = value;
                    OnPropertyChanged(nameof(SBModel));
                }
            }
        }
        public ShBModel ShBModel
        {
            get { return shbModel; }
            set
            {
                if (value != null)
                {
                    shbModel = value;
                    OnPropertyChanged(nameof(ShBModel));
                }
            }
        }
        public SkysteelModel SkysteelModel
        {
            get { return skysteelModel; }
            set
            {
                if (value != null)
                {
                    skysteelModel = value;
                    OnPropertyChanged(nameof(SkysteelModel));
                }
            }
        }
        #endregion
        public Character()
        {
            Name = "Default Name";
            Server = "Default Server";

            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
            SkysteelModel = new SkysteelModel();
            HWModel = new HWModel();
        }

        public Character(string name, string server )
        {
            Name = name;
            Server = server;
            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
            HWModel = new HWModel();
            SBModel = new SBModel();
            SkysteelModel = new SkysteelModel();

            ArrLayout = "Horizontal";
            HWLayout = "Horizontal";
            SBLayout = "Horizontal";
            ShBLayout = "Horizontal";
            SkysteelLayout = "Horizontal";

        }

        public Character(Character oldCharacter)
            :this()
        {
            foreach (PropertyInfo propertyInfo in oldCharacter.GetType().GetProperties())
            {

                if (propertyInfo.GetValue(oldCharacter) != null)
                {
                    propertyInfo.SetValue(this, propertyInfo.GetValue(oldCharacter));
                }
            }

            if (string.IsNullOrEmpty(ArrLayout)) { ArrLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(HWLayout)) { HWLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(SBLayout)) { SBLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(ShBLayout)) { ShBLayout = "Horizontal"; }
            if (string.IsNullOrEmpty(SkysteelLayout)) { SkysteelLayout = "Horizontal"; }

            //Manually set ARR jobs to progresses since I merged the functionality in version v5.35.3.0 but ARR progress classes were not storing Job names with the progress and
            // the stage progressing method is being made consistent across expansions

            this.ArrProgress.ArrWeapon.PLD.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Relic, "Relic", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Zenith, "Zenith", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Atma, "Atma", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Animus, "Animus", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Novus, "Novus", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Nexus, "Nexus", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Braves, "Braves", "PLD");
            this.ArrProgress.ArrWeapon.PLD.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.PLD.Zeta, "Zeta", "PLD");

            this.ArrProgress.ArrWeapon.WAR.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Relic, "Relic", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Zenith, "Zenith", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Atma, "Atma", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Animus, "Animus", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Novus, "Novus", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Nexus, "Nexus", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Braves, "Braves", "WAR");
            this.ArrProgress.ArrWeapon.WAR.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WAR.Zeta, "Zeta", "WAR");

            this.ArrProgress.ArrWeapon.WHM.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Relic, "Relic", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Zenith, "Zenith", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Atma, "Atma", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Animus, "Animus", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Novus, "Novus", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Nexus, "Nexus", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Braves, "Braves", "WHM");
            this.ArrProgress.ArrWeapon.WHM.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.WHM.Zeta, "Zeta", "WHM");

            this.ArrProgress.ArrWeapon.SCH.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Relic, "Relic", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Zenith, "Zenith", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Atma, "Atma", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Animus, "Animus", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Novus, "Novus", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Nexus, "Nexus", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Braves, "Braves", "SCH");
            this.ArrProgress.ArrWeapon.SCH.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SCH.Zeta, "Zeta", "SCH");

            this.ArrProgress.ArrWeapon.MNK.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Relic, "Relic", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Zenith, "Zenith", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Atma, "Atma", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Animus, "Animus", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Novus, "Novus", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Nexus, "Nexus", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Braves, "Braves", "MNK");
            this.ArrProgress.ArrWeapon.MNK.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.MNK.Zeta, "Zeta", "MNK");

            this.ArrProgress.ArrWeapon.DRG.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Relic, "Relic", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Zenith, "Zenith", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Atma, "Atma", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Animus, "Animus", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Novus, "Novus", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Nexus, "Nexus", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Braves, "Braves", "DRG");
            this.ArrProgress.ArrWeapon.DRG.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.DRG.Zeta, "Zeta", "DRG");

            this.ArrProgress.ArrWeapon.NIN.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Relic, "Relic", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Zenith, "Zenith", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Atma, "Atma", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Animus, "Animus", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Novus, "Novus", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Nexus, "Nexus", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Braves, "Braves", "NIN");
            this.ArrProgress.ArrWeapon.NIN.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.NIN.Zeta, "Zeta", "NIN");

            this.ArrProgress.ArrWeapon.BRD.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Relic, "Relic", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Zenith, "Zenith", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Atma, "Atma", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Animus, "Animus", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Novus, "Novus", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Nexus, "Nexus", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Braves, "Braves", "BRD");
            this.ArrProgress.ArrWeapon.BRD.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BRD.Zeta, "Zeta", "BRD");

            this.ArrProgress.ArrWeapon.BLM.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Relic, "Relic", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Zenith, "Zenith", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Atma, "Atma", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Animus, "Animus", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Novus, "Novus", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Nexus, "Nexus", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Braves, "Braves", "BLM");
            this.ArrProgress.ArrWeapon.BLM.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.BLM.Zeta, "Zeta", "BLM");

            this.ArrProgress.ArrWeapon.SMN.Relic = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Relic, "Relic", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Zenith = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Zenith, "Zenith", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Atma = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Atma, "Atma", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Animus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Animus, "Animus", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Novus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Novus, "Novus", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Nexus = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Nexus, "Nexus", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Braves = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Braves, "Braves", "SMN");
            this.ArrProgress.ArrWeapon.SMN.Zeta = new ARR.ArrHelpers.ArrProgress(oldCharacter.ArrProgress.ArrWeapon.SMN.Zeta, "Zeta", "SMN");




        }

        public override string ToString()
        {
            return name + " : " + server;
        }
    }
}
