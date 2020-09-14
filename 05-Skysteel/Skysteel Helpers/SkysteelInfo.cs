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
               "BTM",
               "FSH"
            };

        public static List<string> StageListString = new List<string>()
        {
           "BaseTool",
           "BasePlus1",
           "Dragonsung"
        };

        #region Methods

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
    }
}
