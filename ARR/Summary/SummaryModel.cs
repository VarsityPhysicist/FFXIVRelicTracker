using FFXIVRelicTracker.ARR.ARR;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.ARR;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Summary
{
    class SummaryModel : ObservableObject
    {
        private Character selectedCharacter;

        public SummaryModel(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
            JobDictionary = new Dictionary<string, KeyValuePair<ArrStages, ArrProgress>>();

            JobDictionary.Add("PLD_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Relic));
            JobDictionary.Add("PLD_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Zenith));
            JobDictionary.Add("PLD_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Atma));
            JobDictionary.Add("PLD_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Animus));
            JobDictionary.Add("PLD_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Novus));
            JobDictionary.Add("PLD_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Nexus));
            JobDictionary.Add("PLD_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Braves));
            JobDictionary.Add("PLD_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.PLD, selectedCharacter.ArrProgress.Arr.PLD.Zeta));

            JobDictionary.Add("WAR_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Relic));
            JobDictionary.Add("WAR_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Zenith));
            JobDictionary.Add("WAR_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Atma));
            JobDictionary.Add("WAR_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Animus));
            JobDictionary.Add("WAR_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Novus));
            JobDictionary.Add("WAR_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Nexus));
            JobDictionary.Add("WAR_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Braves));
            JobDictionary.Add("WAR_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WAR, selectedCharacter.ArrProgress.Arr.WAR.Zeta));

            JobDictionary.Add("WHM_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Relic));
            JobDictionary.Add("WHM_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Zenith));
            JobDictionary.Add("WHM_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Atma));
            JobDictionary.Add("WHM_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Animus));
            JobDictionary.Add("WHM_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Novus));
            JobDictionary.Add("WHM_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Nexus));
            JobDictionary.Add("WHM_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Braves));
            JobDictionary.Add("WHM_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.WHM, selectedCharacter.ArrProgress.Arr.WHM.Zeta));

            JobDictionary.Add("SCH_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Relic));
            JobDictionary.Add("SCH_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Zenith));
            JobDictionary.Add("SCH_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Atma));
            JobDictionary.Add("SCH_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Animus));
            JobDictionary.Add("SCH_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Novus));
            JobDictionary.Add("SCH_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Nexus));
            JobDictionary.Add("SCH_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Braves));
            JobDictionary.Add("SCH_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SCH, selectedCharacter.ArrProgress.Arr.SCH.Zeta));

            JobDictionary.Add("MNK_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Relic));
            JobDictionary.Add("MNK_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Zenith));
            JobDictionary.Add("MNK_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Atma));
            JobDictionary.Add("MNK_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Animus));
            JobDictionary.Add("MNK_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Novus));
            JobDictionary.Add("MNK_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Nexus));
            JobDictionary.Add("MNK_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Braves));
            JobDictionary.Add("MNK_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.MNK, selectedCharacter.ArrProgress.Arr.MNK.Zeta));

            JobDictionary.Add("DRG_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Relic));
            JobDictionary.Add("DRG_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Zenith));
            JobDictionary.Add("DRG_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Atma));
            JobDictionary.Add("DRG_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Animus));
            JobDictionary.Add("DRG_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Novus));
            JobDictionary.Add("DRG_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Nexus));
            JobDictionary.Add("DRG_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Braves));
            JobDictionary.Add("DRG_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.DRG, selectedCharacter.ArrProgress.Arr.DRG.Zeta));

            JobDictionary.Add("NIN_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Relic));
            JobDictionary.Add("NIN_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Zenith));
            JobDictionary.Add("NIN_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Atma));
            JobDictionary.Add("NIN_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Animus));
            JobDictionary.Add("NIN_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Novus));
            JobDictionary.Add("NIN_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Nexus));
            JobDictionary.Add("NIN_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Braves));
            JobDictionary.Add("NIN_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.NIN, selectedCharacter.ArrProgress.Arr.NIN.Zeta));

            JobDictionary.Add("BRD_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Relic));
            JobDictionary.Add("BRD_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Zenith));
            JobDictionary.Add("BRD_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Atma));
            JobDictionary.Add("BRD_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Animus));
            JobDictionary.Add("BRD_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Novus));
            JobDictionary.Add("BRD_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Nexus));
            JobDictionary.Add("BRD_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Braves));
            JobDictionary.Add("BRD_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BRD, selectedCharacter.ArrProgress.Arr.BRD.Zeta));

            JobDictionary.Add("BLM_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Relic));
            JobDictionary.Add("BLM_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Zenith));
            JobDictionary.Add("BLM_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Atma));
            JobDictionary.Add("BLM_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Animus));
            JobDictionary.Add("BLM_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Novus));
            JobDictionary.Add("BLM_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Nexus));
            JobDictionary.Add("BLM_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Braves));
            JobDictionary.Add("BLM_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.BLM, selectedCharacter.ArrProgress.Arr.BLM.Zeta));

            JobDictionary.Add("SMN_Relic", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Relic));
            JobDictionary.Add("SMN_Zenith", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Zenith));
            JobDictionary.Add("SMN_Atma", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Atma));
            JobDictionary.Add("SMN_Animus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Animus));
            JobDictionary.Add("SMN_Novus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Novus));
            JobDictionary.Add("SMN_Nexus", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Nexus));
            JobDictionary.Add("SMN_Braves", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Braves));
            JobDictionary.Add("SMN_Zeta", new KeyValuePair<ArrStages, ArrProgress>(selectedCharacter.ArrProgress.Arr.SMN, selectedCharacter.ArrProgress.Arr.SMN.Zeta));
        }

        private Dictionary<string, KeyValuePair<ArrStages, ArrProgress>> jobDictionary;
        public Dictionary<string, KeyValuePair<ArrStages, ArrProgress>> JobDictionary
        {
            get { return jobDictionary; }
            set
            {
                jobDictionary = value;
                OnPropertyChanged(nameof(JobDictionary));
            }
        }
    }
}
