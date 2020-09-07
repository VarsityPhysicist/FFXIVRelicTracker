using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    public static class ArrStageCompleter
    {

        public static void ProgressClass(Character character, ArrJobs Job, ArrProgress arrProgress, bool CompleteBool=false )
        {


            int StageIndex = Job.StageList.IndexOf(arrProgress);


            if (arrProgress.Progress == ArrProgress.States.NA)
            {
                CompletePreviousStages(Job, StageIndex);
            }
            else if (arrProgress.Progress == ArrProgress.States.Completed)
            {
                InCompleteFollowingStages(Job, StageIndex);
                return;
            }
            if (arrProgress.Progress == ArrProgress.States.Initiated | CompleteBool)
            {
                arrProgress.Progress = ArrProgress.States.Completed;
            }

            else
            {
                switch (StageIndex)
                {
                    case 0:
                    case 3:
                    case 6:
                    case 5:
                    case 7:
                        arrProgress.Progress++;
                        if (arrProgress.Progress == ArrProgress.States.Initiated) { IncompleteOtherJobs(character, Job, StageIndex); }
                        break;
                    case 1:
                    case 2:
                    case 4:
                        arrProgress.Progress = ArrProgress.States.Completed;
                        break;
                }
            }
        }



        private static void IncompleteOtherJobs(Character SelectedCharacter, ArrJobs tempStage, int StageIndex)
        {
            foreach (ArrJobs Job in SelectedCharacter.ArrProgress.ArrWeapon.JobList)
            {
                if (Job != tempStage)
                {
                    ArrProgress stage = Job.StageList[StageIndex];
                    if (stage.Progress == ArrProgress.States.Initiated)
                    {
                        stage.Progress = ArrProgress.States.NA;
                    }

                }
            }
        }

        private static void InCompleteFollowingStages(ArrJobs tempStage, int stageIndex)
        {


            for (int i = stageIndex; i < tempStage.StageList.Count; i++)
            {

                tempStage.Animus.Progress = ArrProgress.States.NA;
                tempStage.StageList[i].Progress = ArrProgress.States.NA;
            }
        }

        private static void CompletePreviousStages(ArrJobs tempStage, int stageIndex)
        {
            for (int i = 0; i < stageIndex; i++)
            {

                tempStage.StageList[i].Progress = ArrProgress.States.Completed;
            }
        }

    }
}
