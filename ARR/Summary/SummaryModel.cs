
using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;

using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Summary
{
    
    public class SummaryModel : ObservableObject
    {

        public SummaryModel(Character selectedCharacter)
        {
            if (selectedCharacter != null)
            {
                JobDictionary = new Dictionary<string, KeyValuePair<ArrJobs, ArrProgress>>();

                JobDictionary.Add("PLD_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Relic));
                JobDictionary.Add("PLD_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Zenith));
                JobDictionary.Add("PLD_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Atma));
                JobDictionary.Add("PLD_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Animus));
                JobDictionary.Add("PLD_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Novus));
                JobDictionary.Add("PLD_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Nexus));
                JobDictionary.Add("PLD_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Braves));
                JobDictionary.Add("PLD_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.PLD, selectedCharacter.ArrProgress.ArrWeapon.PLD.Zeta));

                JobDictionary.Add("WAR_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Relic));
                JobDictionary.Add("WAR_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Zenith));
                JobDictionary.Add("WAR_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Atma));
                JobDictionary.Add("WAR_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Animus));
                JobDictionary.Add("WAR_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Novus));
                JobDictionary.Add("WAR_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Nexus));
                JobDictionary.Add("WAR_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Braves));
                JobDictionary.Add("WAR_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WAR, selectedCharacter.ArrProgress.ArrWeapon.WAR.Zeta));

                JobDictionary.Add("WHM_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Relic));
                JobDictionary.Add("WHM_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Zenith));
                JobDictionary.Add("WHM_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Atma));
                JobDictionary.Add("WHM_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Animus));
                JobDictionary.Add("WHM_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Novus));
                JobDictionary.Add("WHM_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Nexus));
                JobDictionary.Add("WHM_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Braves));
                JobDictionary.Add("WHM_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.WHM, selectedCharacter.ArrProgress.ArrWeapon.WHM.Zeta));

                JobDictionary.Add("SCH_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Relic));
                JobDictionary.Add("SCH_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Zenith));
                JobDictionary.Add("SCH_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Atma));
                JobDictionary.Add("SCH_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Animus));
                JobDictionary.Add("SCH_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Novus));
                JobDictionary.Add("SCH_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Nexus));
                JobDictionary.Add("SCH_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Braves));
                JobDictionary.Add("SCH_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SCH, selectedCharacter.ArrProgress.ArrWeapon.SCH.Zeta));

                JobDictionary.Add("MNK_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Relic));
                JobDictionary.Add("MNK_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Zenith));
                JobDictionary.Add("MNK_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Atma));
                JobDictionary.Add("MNK_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Animus));
                JobDictionary.Add("MNK_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Novus));
                JobDictionary.Add("MNK_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Nexus));
                JobDictionary.Add("MNK_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Braves));
                JobDictionary.Add("MNK_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.MNK, selectedCharacter.ArrProgress.ArrWeapon.MNK.Zeta));

                JobDictionary.Add("DRG_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Relic));
                JobDictionary.Add("DRG_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Zenith));
                JobDictionary.Add("DRG_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Atma));
                JobDictionary.Add("DRG_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Animus));
                JobDictionary.Add("DRG_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Novus));
                JobDictionary.Add("DRG_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Nexus));
                JobDictionary.Add("DRG_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Braves));
                JobDictionary.Add("DRG_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.DRG, selectedCharacter.ArrProgress.ArrWeapon.DRG.Zeta));

                JobDictionary.Add("NIN_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Relic));
                JobDictionary.Add("NIN_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Zenith));
                JobDictionary.Add("NIN_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Atma));
                JobDictionary.Add("NIN_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Animus));
                JobDictionary.Add("NIN_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Novus));
                JobDictionary.Add("NIN_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Nexus));
                JobDictionary.Add("NIN_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Braves));
                JobDictionary.Add("NIN_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.NIN, selectedCharacter.ArrProgress.ArrWeapon.NIN.Zeta));

                JobDictionary.Add("BRD_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Relic));
                JobDictionary.Add("BRD_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Zenith));
                JobDictionary.Add("BRD_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Atma));
                JobDictionary.Add("BRD_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Animus));
                JobDictionary.Add("BRD_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Novus));
                JobDictionary.Add("BRD_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Nexus));
                JobDictionary.Add("BRD_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Braves));
                JobDictionary.Add("BRD_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BRD, selectedCharacter.ArrProgress.ArrWeapon.BRD.Zeta));

                JobDictionary.Add("BLM_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Relic));
                JobDictionary.Add("BLM_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Zenith));
                JobDictionary.Add("BLM_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Atma));
                JobDictionary.Add("BLM_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Animus));
                JobDictionary.Add("BLM_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Novus));
                JobDictionary.Add("BLM_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Nexus));
                JobDictionary.Add("BLM_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Braves));
                JobDictionary.Add("BLM_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.BLM, selectedCharacter.ArrProgress.ArrWeapon.BLM.Zeta));

                JobDictionary.Add("SMN_Relic", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Relic));
                JobDictionary.Add("SMN_Zenith", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Zenith));
                JobDictionary.Add("SMN_Atma", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Atma));
                JobDictionary.Add("SMN_Animus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Animus));
                JobDictionary.Add("SMN_Novus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Novus));
                JobDictionary.Add("SMN_Nexus", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Nexus));
                JobDictionary.Add("SMN_Braves", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Braves));
                JobDictionary.Add("SMN_Zeta", new KeyValuePair<ArrJobs, ArrProgress>(selectedCharacter.ArrProgress.ArrWeapon.SMN, selectedCharacter.ArrProgress.ArrWeapon.SMN.Zeta));
            }
            
        }

        private Dictionary<string, KeyValuePair<ArrJobs, ArrProgress>> jobDictionary;
        public Dictionary<string, KeyValuePair<ArrJobs, ArrProgress>> JobDictionary
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
