using FFXIVRelicTracker.ARR.ARR;
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
        internal List<string> leves;
        internal List<string> fates;
        internal List<string> creatures;
        internal bool showBookItems;
        internal List<string> observableUniqueMaps;
        internal List<string> observableAllMaps;

        internal bool completedDungeon1;
        internal bool completedDungeon2;
        internal bool completedDungeon3;

        internal bool possibleChecked1;
        internal bool possibleChecked2;
        internal bool possibleChecked3;
        internal bool possibleChecked4;
        internal bool possibleChecked5;
        internal bool possibleChecked6;
        internal bool possibleChecked7;
        internal bool possibleChecked8;
        internal bool possibleChecked9;
        internal bool possibleChecked10;

        internal bool possibleVisibility1;
        internal bool possibleVisibility2;
        internal bool possibleVisibility3;
        internal bool possibleVisibility4;
        internal bool possibleVisibility5;
        internal bool possibleVisibility6;
        internal bool possibleVisibility7;
        internal bool possibleVisibility8;
        internal bool possibleVisibility9;
        internal bool possibleVisibility10;

        internal string possibleMap1;
        internal string possibleMap2;
        internal string possibleMap3;
        internal string possibleMap4;
        internal string possibleMap5;
        internal string possibleMap6;
        internal string possibleMap7;
        internal string possibleMap8;
        internal string possibleMap9;
        internal string possibleMap10;

        internal string selectedAnimusMap;
        internal string selectedAnimusImage;
       
        internal string dungeon1;
        internal string dungeon2;
        internal string dungeon3;
        
        internal bool skyFire1Book;
        internal bool skyFire2Book;
        internal bool skyFall1Book;
        internal bool skyFall2Book;
        internal bool netherFire1Book;
        internal bool netherFall1Book;
        internal bool skyWind1Book;
        internal bool skyEarth1Book;
        internal int bookSelection;

        internal AnimusObject Leve1 = new AnimusObject();
        internal AnimusObject Leve2 = new AnimusObject();
        internal AnimusObject Leve3 = new AnimusObject();

        internal AnimusObject DisplayLeve = new AnimusObject();

        internal AnimusObject Fate1 = new AnimusObject();
        internal AnimusObject Fate2 = new AnimusObject();
        internal AnimusObject Fate3 = new AnimusObject();

        internal AnimusObject DisplayFate = new AnimusObject();

        internal AnimusObject Creature1 = new AnimusObject();
        internal AnimusObject Creature2 = new AnimusObject();
        internal AnimusObject Creature3 = new AnimusObject();
        internal AnimusObject Creature4 = new AnimusObject();
        internal AnimusObject Creature5 = new AnimusObject();
        internal AnimusObject Creature6 = new AnimusObject();
        internal AnimusObject Creature7 = new AnimusObject();
        internal AnimusObject Creature8 = new AnimusObject();
        internal AnimusObject Creature9 = new AnimusObject();
        internal AnimusObject Creature10 = new AnimusObject();

        internal AnimusObject DisplayCreature1 = new AnimusObject();
        internal AnimusObject DisplayCreature2 = new AnimusObject();
        internal AnimusObject DisplayCreature3 = new AnimusObject();


        internal ObservableCollection<string> animusBooks = new ObservableCollection<string>
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
        internal ObservableCollection<string> availableAnimusobs;

        public AnimusModel()
        {

        }
        public string CurrentBook { get; internal set; }
        public string CurrentAnimus { get; internal set; }
    }
}
