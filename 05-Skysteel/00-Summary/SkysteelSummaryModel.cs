using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_Skysteel._00_Summary
{
    class SkysteelSummaryModel : ObservableObject
    {
        private Character selectedCharacter;

        public SkysteelSummaryModel(Character selectedCharacter)
        {
            this.selectedCharacter = selectedCharacter;
        }
    }
}
