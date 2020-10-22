using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._03_HW._00_Summary
{
    public class HWSummaryModel : ObservableObject
    {

        public HWSummaryModel(Character SelectedCharacter)
        {
            this.SelectedCharacter = SelectedCharacter;
        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                if (value != null)
                {
                    selectedCharacter = value;
                    OnPropertyChanged(nameof(SelectedCharacter));
                }
            }
        }
    }
}
