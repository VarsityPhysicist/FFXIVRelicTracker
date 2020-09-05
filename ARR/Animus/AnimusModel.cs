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


        internal BoolObject completedFate1 = new BoolObject();
        internal BoolObject completedFate2 = new BoolObject();
        internal BoolObject completedFate3 = new BoolObject();

        internal bool completedCreature1;
        internal bool completedCreature2;
        internal bool completedCreature3;
        internal bool completedCreature4;
        internal bool completedCreature5;
        internal bool completedCreature6;
        internal bool completedCreature7;
        internal bool completedCreature8;
        internal bool completedCreature9;
        internal bool completedCreature10;

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

        internal Visibility creatureVisibility1;
        internal string creatureActive1;
        internal double creatureX1;
        internal double creatureY1;
        internal Visibility creatureVisibility2;
        internal string creatureActive2;
        internal Visibility creatureVisibility3;
        internal string creatureActive3;
        internal Visibility creatureVisibility4;
        internal string creatureActive4;
        internal Visibility creatureVisibility5;
        internal string creatureActive5;
        internal double creatureY5;
        internal double creatureX5;
        internal double creatureY4;
        internal double creatureX4;
        internal double creatureY3;
        internal double creatureX3;
        internal double creatureY2;
        internal double creatureX2;
        internal Visibility fateVisibility1;
        internal string fateActive1;
        internal double fateX1;
        internal double fateY1;
        internal Visibility leveVisibility1;
        internal string leveActive1;
        internal double leveX1;
        internal double leveY1;
        internal string selectedAnimusMap;
        internal string selectedAnimusImage;
        internal PointF levePoint3;
        internal PointF levePoint2;
        internal PointF levePoint1;
        internal string leveType3;
        internal string leveType2;
        internal string leveType1;
        internal string levePerson3;
        internal string levePerson2;
        internal string levePerson1;
        internal string leveMap3;
        internal string leveMap2;
        internal string leveMap1;
        internal string leveName3;
        internal string leveName2;
        internal string leveName1;
        internal string creature1;
        internal string creature2;
        internal string creature3;
        internal string creature4;
        internal string creature5;
        internal string creature6;
        internal string creature7;
        internal string creature8;
        internal string creature9;
        internal string creature10;
        internal string creatureMap1;
        internal string creatureMap2;
        internal string creatureMap3;
        internal string creatureMap4;
        internal string creatureMap5;
        internal string creatureMap6;
        internal string creatureMap7;
        internal string creatureMap8;
        internal string creatureMap9;
        internal string creatureMap10;
        internal PointF creaturePoint1;
        internal PointF creaturePoint2;
        internal PointF creaturePoint3;
        internal PointF creaturePoint4;
        internal PointF creaturePoint5;
        internal PointF creaturePoint6;
        internal PointF creaturePoint7;
        internal PointF creaturePoint8;
        internal PointF creaturePoint9;
        internal PointF creaturePoint10;
        internal string dungeon1;
        internal string dungeon2;
        internal string dungeon3;
        internal string fateName1;
        internal string fateName2;
        internal string fateName3;
        internal string fateMap1;
        internal string fateMap2;
        internal string fateMap3;
        internal PointF fatePoint1;
        internal PointF fatePoint2;
        internal PointF fatePoint3;
        //internal ObservableCollection<string> animusBooks;
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
