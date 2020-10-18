using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._03_HW.HWHelpers
{
    public static class HWStageCompleter
    {
        #region CompleteStages
        public static void ProgressClass(Character character, HWProgress hWProgress, bool CompleteBool = false)
        {
            int StageIndex = HWInfo.StageListString.IndexOf(hWProgress.Name);
            int JobIndex = HWInfo.JobListString.IndexOf(hWProgress.Job);

            HWJob tempJob = character.HWModel.HWJobList[JobIndex];

            if (hWProgress.Progress == HWProgress.States.NA)
            {
                CompletePreviousStages(character, tempJob, StageIndex);
            }
            else if (hWProgress.Progress == HWProgress.States.Completed)
            {
                InCompleteFollowingStages(tempJob, StageIndex);
                return;
            }
            if (hWProgress.Progress == HWProgress.States.Initiated | CompleteBool)
            {
                hWProgress.Progress = HWProgress.States.Completed;
                SelectStage(character, StageIndex);
            }
            else
            {
                IncompleteOtherJobs(character, StageIndex);
                switch (StageIndex)
                {
                    default:
                        hWProgress.Progress = HWProgress.States.Completed;
                        break;
                        //case 1:
                        //case 2:
                        //    hWProgress.Progress++;
                        //    break;
                }
                SelectStage(character, StageIndex);
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
        private static void CompletePreviousStages(Character character, HWJob tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {
                tempStage.StageList[i].Progress = HWProgress.States.Completed;
                SelectStage(character, i);
            }
        }
        #endregion

        #region Stage Specifics
        private static void SelectStage(Character character, int stageInt)
        {
            switch (stageInt)
            {
                case 0:
                    AnimatedStage(character);
                    break;
                case 1:
                    break;
                case 2:
                    AnimaStage(character);
                    break;
                default:
                    break;
            }
        }
        private static void AnimatedStage(Character character)
        {
            character.HWModel.AnimatedModel.WindCount -= 1;
            character.HWModel.AnimatedModel.FireCount -= 1;
            character.HWModel.AnimatedModel.LightningCount -= 1;
            character.HWModel.AnimatedModel.IceCount -= 1;
            character.HWModel.AnimatedModel.EarthCount -= 1;
            character.HWModel.AnimatedModel.WaterCount -= 1;
        }

        private static void AnimaStage(Character character)
        {
            character.HWModel.AnimaModel.UnidentifiableBone -= 10;
            character.HWModel.AnimaModel.UnidentifiableShell -= 10;
            character.HWModel.AnimaModel.UnidentifiableOre -= 10;
            character.HWModel.AnimaModel.UnidentifiableSeeds -= 10;

            character.HWModel.AnimaModel.Francesca -= 4;
            character.HWModel.AnimaModel.Mirror -= 4;
            character.HWModel.AnimaModel.Arrow -= 4;
            character.HWModel.AnimaModel.Kingcake -= 4;

        }
        #endregion
    }
}
