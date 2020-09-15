using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers
{
    public static class SkysteelInfo
    {
        public static List<string> JobListString = new List<string>
            {
               "CRP",
               "BSM",
               "ARM",
               "GSM",
               "LTW",
               "WVR",
               "ALC",
               "CUL",
               "MIN",
               "BTN",
               "FSH"
            };

        public static List<string> StageListString = new List<string>()
        {
           "BaseTool",
           "BasePlus1",
           "Dragonsung"
        };

        private static Dictionary<string, string> JobToToolDict= new Dictionary<string, string>
        {
            {"CRP", "Saw" },
            {"BSM", "Cross-pein Hammer" },
            {"ARM", "Raising Hammer"},
            {"GSM", "Lapidary Hammer"},
            {"LTW", "Round Knife"},
            {"WVR", "Needle"},
            {"ALC", "Alembic"},
            {"CUL", "Frypan"},
            {"MIN", "Pickaxe" },
            {"BTN", "Hatchet"},
            {"FSH", "Fishing Rod"}
        };

        #region Methods

        public static string ReturnToolName(string job)
        {
            return JobToToolDict[job];
        }

        #region CompleteStages
        public static void ProgressClass(Character character, SkysteelProgress skysteelProgress, bool CompleteBool = false)
        {
            int StageIndex = SkysteelInfo.StageListString.IndexOf(skysteelProgress.Name);
            int JobIndex = SkysteelInfo.JobListString.IndexOf(skysteelProgress.Job);

            SkysteelJob tempJob = character.SkysteelModel.SkysteelJobList[JobIndex];

            if (skysteelProgress.Progress == SkysteelProgress.States.NA)
            {
                CompletePreviousStages(tempJob, StageIndex);
            }
            else if (skysteelProgress.Progress == SkysteelProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }
            if (skysteelProgress.Progress == SkysteelProgress.States.Initiated | CompleteBool)
            {
                skysteelProgress.Progress = SkysteelProgress.States.Completed;
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    case 0:
                        skysteelProgress.Progress = SkysteelProgress.States.Completed;
                        break;
                    case 1:
                    case 2:
                        skysteelProgress.Progress++;
                        break;
                }
            }
        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (SkysteelJob Job in SelectedCharacter.SkysteelModel.SkysteelJobList)
            {
                SkysteelProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == SkysteelProgress.States.Initiated)
                {
                    stage.Progress = SkysteelProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(SkysteelJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = SkysteelProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(SkysteelJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i].Progress = SkysteelProgress.States.Completed;
            }
        }

        #endregion

        public static void ReloadJobList(ObservableCollection<string> tempList, string jobName)
        {
            //This method should be called from LoadAvailableJobs methods to add jobs back into the list to preserve their order

            int jobIndex = SkysteelInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (SkysteelInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (SkysteelInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
                        {
                            tempList.Insert(i, jobName);
                            break;
                        }
                    }
                    if (!tempList.Contains(jobName)) { tempList.Add(jobName); }
                    break;
            }
        }

        #endregion

        #region BasePlus1

        public static Tuple<string, string, string, string> ReturnJobTuple(string job)
        {
            return new Tuple<string, string, string, string>(JobToToolDict[job], JobToPlus1Craft[job], JobToFirstPlus1Mat[job], JobToSecondPlus1Mat[job]);
        }

        private static Dictionary<string, string> JobToPlus1Craft = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Petrified Orb" },
            {"BSM", "Oddly Specific Rivets" },
            {"ARM", "Oddly Specific Wire"},
            {"GSM", "Oddly Specific Whetstone"},
            {"LTW", "Oddly Specific Leather"},
            {"WVR", "Oddly Specific Moonbeam Silk"},
            {"ALC", "Oddly Specific Synthetic Resin"},
            {"CUL", "Oddly Specific Seed Extract"},
            {"MIN", "" },
            {"BTN", ""},
            {"FSH", ""}
        };

        private static Dictionary<string, string> JobToFirstPlus1Mat = new Dictionary<string, string>
        {
            {"CRP", "Oddly Specific Petrified Log" },
            {"BSM", "Oddly Specific Iron Sand" },
            {"ARM", "Oddly Specific Iron Ore"},
            {"GSM", "Oddly Specific Uncut Gemstone"},
            {"LTW", "Oddly Specific Skin"},
            {"WVR", "Oddly Specific Cotton"},
            {"ALC", "Oddly Specific Quartz"},
            {"CUL", "Oddly Specific Seeds"},
            {"MIN", "Oddly Specific Obsidian (Coerthas Western Highlands/The Sea of Clouds)" },
            {"BTN", "Oddly Specific Latex (The Dravanian Forelands/The Sea of Clouds)"},
            {"FSH", "Thinker's Coral (The Dravanian Forelands)"}
        };

        private static Dictionary<string, string> JobToSecondPlus1Mat = new Dictionary<string, string>
        {
            {"CRP", "White Ash Log (The Rak'tola Greatwood)" },
            {"BSM", "Manasilver Sand (The Rak'tola Greatwood)" },
            {"ARM", "Manasilver Sand (The Rak'tola Greatwood)"},
            {"GSM", "Manasilver Sand (The Rak'tola Greatwood)"},
            {"LTW", "Atrociraptor Skin (Bicolor Gemstone)"},
            {"WVR", "Pixie Floss Boll (The Rak'tola Greatwood)"},
            {"ALC", "Vampire Vine Sap (Bicolor Gemstone)"},
            {"CUL", "Highland Spring Water (Il Mheg)"},
            {"MIN", "Oddly Specific Mineral Sand (Coerthas Western Highlands/The Sea of Clouds)" },
            {"BTN", "Oddly Specific Fossil Dust (The Dravanian Forelands/The Sea of Clouds)"},
            {"FSH", ""}
        };

        #endregion
    }
}
