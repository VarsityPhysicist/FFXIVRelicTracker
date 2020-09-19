using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public static class HWInfo
    {
        #region Common
        public static List<string> JobListString = new List<string>
            {
               "PLD",
               "WAR",
               "DRK",
               "WHM",
               "SCH",
               "AST",
               "MNK",
               "DRG",
               "NIN",
               "BRD",
               "MCH",
               "BLM",
               "SMN"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Animated",
           "Awoken",
           "Anima",
           "Hyperconductive",
           "Reconditioned",
           "Sharpened",
           "Complete",
           "Lux"
        };

        #region Methods
        #region CompleteStages
        public static void ProgressClass(Character character, HWProgress skysteelProgress, bool CompleteBool = false)
        {
            int StageIndex = HWInfo.StageListString.IndexOf(skysteelProgress.Name);
            int JobIndex = HWInfo.JobListString.IndexOf(skysteelProgress.Job);

            HWJob tempJob = character.HWModel.HWJobList[JobIndex];

            if (skysteelProgress.Progress == HWProgress.States.NA)
            {
                CompletePreviousStages(tempJob, StageIndex);
            }
            else if (skysteelProgress.Progress == HWProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }
            if (skysteelProgress.Progress == HWProgress.States.Initiated | CompleteBool)
            {
                skysteelProgress.Progress = HWProgress.States.Completed;
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    default:
                        skysteelProgress.Progress = HWProgress.States.Completed;
                        break;
                        //case 1:
                        //case 2:
                        //    skysteelProgress.Progress++;
                        //    break;
                }
            }
        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (HWJob Job in SelectedCharacter.HWModel.HWJobList)
            {
                HWProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == HWProgress.States.Initiated)
                {
                    stage.Progress = HWProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(HWJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = HWProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(HWJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i].Progress = HWProgress.States.Completed;
            }
        }

        #endregion


        public static void ReloadJobList(ObservableCollection<string> tempList, string jobName)
        {
            //This method should be called from LoadAvailableJobs methods to add jobs back into the list to preserve their order

            int jobIndex = HWInfo.JobListString.IndexOf(jobName);
            switch (tempList.Count)
            {
                case 0:
                    tempList.Add(jobName);
                    break;
                case 1:
                    if (HWInfo.JobListString.IndexOf(tempList[0]) > jobIndex) { tempList.Insert(0, jobName); }
                    else { tempList.Add(jobName); }
                    break;
                default:
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (HWInfo.JobListString.IndexOf(tempList[i]) > jobIndex)
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

        #endregion

    }
}
