using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_ShB.ShBHelpers
{
    public static class ShBStageCompleter
    {
        public static void ProgressClass(Character character, ShBProgress shbProgress, bool CompleteBool = false)
        {
            int StageIndex = ShBInfo.StageListString.IndexOf(shbProgress.Name);
            int JobIndex = ShBInfo.JobListString.IndexOf(shbProgress.Job);

            ShBJob tempJob = character.ShBModel.ShbJobList[JobIndex];

            if (shbProgress.Progress == ShBProgress.States.NA)
            {
                CompletePreviousStages(tempJob, StageIndex);
            }
            else if (shbProgress.Progress == ShBProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }
            if (shbProgress.Progress == ShBProgress.States.Initiated | CompleteBool)
            {
                shbProgress.Progress = ShBProgress.States.Completed;
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    case 0:
                        shbProgress.Progress = ShBProgress.States.Completed;
                        break;
                    case 1:
                        shbProgress.Progress++;
                        break;
                }
            }
        }
        private static void IncompleteOtherJobs(Character SelectedCharacter, int StageIndex)
        {
            foreach (ShBJob Job in SelectedCharacter.ShBModel.ShbJobList)
            {
                ShBProgress stage = Job.StageList[StageIndex];
                if (stage.Progress == ShBProgress.States.Initiated)
                {
                    stage.Progress = ShBProgress.States.NA;
                }
            }
        }
        private static void InCompleteFollowingStages(ShBJob tempStage, int stageIndex)
        {
            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {
                tempStage.StageList[i].Progress = ShBProgress.States.NA;
            }
        }
        private static void CompletePreviousStages(ShBJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i].Progress = ShBProgress.States.Completed;
            }
        }

    }
}
