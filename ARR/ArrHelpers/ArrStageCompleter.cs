using FFXIVRelicTracker.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    public static class ArrStageCompleter
    {

        public static void ProgressClass(Character character,  ArrProgress arrProgress, bool CompleteBool=false )
        {
            int StageIndex = ArrInfo.StageListString.IndexOf(arrProgress.Name);
            int JobIndex = ArrInfo.JobListString.IndexOf(arrProgress.Job);

            ArrJobs Job = character.ArrProgress.ArrWeapon.JobList[JobIndex];

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
                        arrProgress.Progress = ArrProgress.States.Completed;
                        break;
                    case 4:
                        NovusCompleter(character,Job.Name);
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

        private static void NovusCompleter(Character character, string Job)
        {
            if (character.ArrProgress.NovusModel.CurrentNovus == Job)
            {
                int subtractAlexandrite = 75;
                subtractAlexandrite -= character.ArrProgress.NovusModel.MateriaShieldSum + character.ArrProgress.NovusModel.MateriaSwordSum + character.ArrProgress.NovusModel.MateriaSum;

                if (subtractAlexandrite >= character.ArrProgress.NovusModel.AlexandriteCount) { character.ArrProgress.NovusModel.AlexandriteCount = 0; }
                else { character.ArrProgress.NovusModel.AlexandriteCount -= subtractAlexandrite; }

                character.ArrProgress.NovusModel.CurrentNovus = "";
            }
        }

    }
}
