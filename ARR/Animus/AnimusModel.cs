using FFXIVRelicTracker.Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows;

namespace FFXIVRelicTracker.ARR.Animus
{
    
    public class AnimusModel : ObservableObject
    {
        public List<string> leves{ get; set;}
        public List<string> fates{ get; set;}
        public List<string> creatures{ get; set;}
        public bool showBookItems{ get; set;}
        public List<string> observableUniqueMaps { get; set; }
        public List<string> observableAllMaps{ get; set;}

        public bool completedDungeon1{ get; set;}
        public bool completedDungeon2{ get; set;}
        public bool completedDungeon3{ get; set;}

        public bool possibleChecked1{ get; set;}
        public bool possibleChecked2{ get; set;}
        public bool possibleChecked3{ get; set;}
        public bool possibleChecked4{ get; set;}
        public bool possibleChecked5{ get; set;}
        public bool possibleChecked6{ get; set;}
        public bool possibleChecked7{ get; set;}
        public bool possibleChecked8{ get; set;}
        public bool possibleChecked9{ get; set;}
        public bool possibleChecked10{ get; set;}

        public bool possibleVisibility1{ get; set;}
        public bool possibleVisibility2{ get; set;}
        public bool possibleVisibility3{ get; set;}
        public bool possibleVisibility4{ get; set;}
        public bool possibleVisibility5{ get; set;}
        public bool possibleVisibility6{ get; set;}
        public bool possibleVisibility7{ get; set;}
        public bool possibleVisibility8{ get; set;}
        public bool possibleVisibility9{ get; set;}
        public bool possibleVisibility10{ get; set;}

        public string possibleMap1{ get; set;}
        public string possibleMap2{ get; set;}
        public string possibleMap3{ get; set;}
        public string possibleMap4{ get; set;}
        public string possibleMap5{ get; set;}
        public string possibleMap6{ get; set;}
        public string possibleMap7{ get; set;}
        public string possibleMap8{ get; set;}
        public string possibleMap9{ get; set;}
        public string possibleMap10{ get; set;}

        public string selectedAnimusMap{ get; set;}
        public string selectedAnimusImage{ get; set;}
       
        public string dungeon1{ get; set;}
        public string dungeon2{ get; set;}
        public string dungeon3{ get; set;}
        
        public bool skyFire1Book{ get; set;}
        public bool skyFire2Book{ get; set;}
        public bool skyFall1Book{ get; set;}
        public bool skyFall2Book{ get; set;}
        public bool netherFire1Book{ get; set;}
        public bool netherFall1Book{ get; set;}
        public bool skyWind1Book{ get; set;}
        public bool skyEarth1Book{ get; set;}
        public int bookSelection{ get; set;}

        public AnimusObject Leve1{ get; set;}
        public AnimusObject Leve2{ get; set;}
        public AnimusObject Leve3{ get; set;}

        public AnimusObject DisplayLeve{ get; set;}

        public AnimusObject Fate1{ get; set;}
        public AnimusObject Fate2{ get; set;}
        public AnimusObject Fate3{ get; set;}

        public AnimusObject DisplayFate{ get; set;}

        public AnimusObject Creature1{ get; set;}
        public AnimusObject Creature2{ get; set;}
        public AnimusObject Creature3{ get; set;}
        public AnimusObject Creature4{ get; set;}
        public AnimusObject Creature5{ get; set;}
        public AnimusObject Creature6{ get; set;}
        public AnimusObject Creature7{ get; set;}
        public AnimusObject Creature8{ get; set;}
        public AnimusObject Creature9{ get; set;}
        public AnimusObject Creature10{ get; set;}

        public AnimusObject DisplayCreature1{ get; set;}
        public AnimusObject DisplayCreature2{ get; set;}
        public AnimusObject DisplayCreature3{ get; set;}

        public string CurrentBook { get; set; }
        public string CurrentAnimus { get; set; }


        public ObservableCollection<string> animusBooks = new ObservableCollection<string>
            {
                "Book of Skyfire I",
                "Book of Skyfire II",
                "Book of Skyfall I",
                "Book of Skyfall II",
                "Book of Netherfire I",
                "Book of Netherfall I",
                "Book of Skywind I",
                "Book of Skyearth I"
            };
        public ObservableCollection<string> availableAnimusobs { get; set; }
       
        public AnimusModel()
        {
            Leve1 = new AnimusObject();
            Leve2 = new AnimusObject();
            Leve3 = new AnimusObject();

            DisplayLeve = new AnimusObject();

            Fate1 = new AnimusObject();
            Fate2 = new AnimusObject();
            Fate3 = new AnimusObject();

            DisplayFate = new AnimusObject();

            Creature1 = new AnimusObject();
            Creature2 = new AnimusObject();
            Creature3 = new AnimusObject();
            Creature4 = new AnimusObject();
            Creature5 = new AnimusObject();
            Creature6 = new AnimusObject();
            Creature7 = new AnimusObject();
            Creature8 = new AnimusObject();
            Creature9 = new AnimusObject();
            Creature10 = new AnimusObject();

            DisplayCreature1 = new AnimusObject();
            DisplayCreature2 = new AnimusObject();
            DisplayCreature3 = new AnimusObject();
        }

    }
}
