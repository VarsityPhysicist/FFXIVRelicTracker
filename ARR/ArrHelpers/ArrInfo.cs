using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FFXIVRelicTracker.ARR.ArrHelpers
{
    public static class ArrInfo
    {

        public static List<string> JobListString = new List<string>
            {
               "PLD",
               "WAR",
               "WHM",
               "SCH",
               "MNK",
               "DRG",
               "NIN",
               "BRD",
               "BLM",
               "SMN"
            };

        public static List<string> StageListString = new List<string>()
        {
           "Relic",
           "Zenith",
           "Atma",
           "Animus",
           "Novus",
           "Nexus",
           "Braves",
           "Zeta"
        };

        public static Dictionary<string, string> ArrMapImages = new Dictionary<string, string>
        {
            {"Central Shroud","../ArrMaps/CentralShroud.png"},
            {"Central Thanalan" ,"../ArrMaps/CentralThanalan.png"},
            {"Coerthas Central Highlands" ,"../ArrMaps/CoerthasCentralHighlands.png" },
            {"Eastern La Noscea" ,"../ArrMaps/EasternLaNoscea.png"},
            {"Eastern Thanalan" ,"../ArrMaps/EasternThanalan.png" },
            {"Lower La Noscea" ,"../ArrMaps/LowerLaNoscea.png" },
            {"Middle La Noscea" ,"../ArrMaps/MiddleLaNoscea.png" },
            {"Mor Dhona" ,"../ArrMaps/MorDhona.png"},
            {"North Shroud" ,"../ArrMaps/NorthShroud.png" },
            {"Northern Thanalan" ,"../ArrMaps/NorthernThanalan.png" },
            {"Outer La Noscea" ,"../ArrMaps/OuterLaNoscea.png" },
            {"South Shroud" ,"../ArrMaps/SouthShroud.png" },
            {"Southern Thanalan" ,"../ArrMaps/SouthernThanalan.png" },
            {"Upper La Noscea" ,"../ArrMaps/UpperLaNoscea.png" },
            {"Western La Noscea" ,"../ArrMaps/WesternLaNoscea.png" },
            {"Western Thanalan" ,"../ArrMaps/WesternThanalan.png" },
            {"East Shroud" ,"../ArrMaps/EastShroud.png" }
        };

        #region Novus

        public static Dictionary<string, Tuple<int, int, int, int, int, int>> MateriaPercents = new Dictionary<string, Tuple<int, int, int, int, int, int>>
        {
            {"Heaven's Eye",Tuple.Create(7,8,9,10,11,12)},
            {"Quickarm",Tuple.Create(7,8,9,10,11,12) },
            {"Savage Aim",Tuple.Create(7,8,9,10,11,12) },
            {"Piety",Tuple.Create(2,3,4,5,6,7) },
            {"Savage Might",Tuple.Create(94,88,80,70,60,50) },
            {"Quicktongue",Tuple.Create(7,8,9,10,11,12) },
            {"Battledance",Tuple.Create(7,8,9,10,11,12) }
        };

        public static Dictionary<string, Tuple<int, int, int, int, int, int>> MateriaDecayCounts = new Dictionary<string, Tuple<int, int, int, int, int, int>>
        {
            {"Heaven's Eye",Tuple.Create(7,8,9,10,11,12)},
            {"Quickarm",Tuple.Create(7,8,9,10,11,12) },
            {"Savage Aim",Tuple.Create(7,8,9,10,11,12) },
            {"Piety",Tuple.Create(2,3,4,5,6,7) },
            {"Savage Might",Tuple.Create(1,2,3,4,5,6) },
            {"Quicktongue",Tuple.Create(7,8,9,10,11,12) },
            {"Battledance",Tuple.Create(7,8,9,10,11,12) }
        };

        public static Dictionary<string, Tuple<int, int, int, int, int, int,int>> MateriaCaps = new Dictionary<string, Tuple<int, int, int, int, int, int, int>>
        {
             {"PLD",Tuple.Create(0,0,0,0,0,0,0) },
             {"WAR",Tuple.Create(0,0,0,0,0,0,0) },
             {"WHM",Tuple.Create(0,0,0,0,0,0,0) },
             {"SCH",Tuple.Create(0,0,0,0,0,0,0) },
             {"MNK",Tuple.Create(0,0,0,0,0,0,0) },
             {"DRG",Tuple.Create(44,44,44,23,31,44,44) },
             {"NIN",Tuple.Create(0,0,0,0,0,0,0) },
             {"BRD",Tuple.Create(0,0,0,0,0,0,0) },
             {"BLM",Tuple.Create(0,0,0,0,0,0,0) },
             {"SMN",Tuple.Create(0,0,0,0,0,0,0) }
        };

        public static Dictionary<string, string> WeaponNames = new Dictionary<string, string>
        {
            {"PLD","" },
            {"WAR","Bravura" },
            {"WHM","Thyrus" },
            {"SCH", "Omnilex" },
            {"MNK", "Sphairai" },
            {"DRG", "Gae Bolg" },
            {"NIN", "Yoshimitsu" },
            {"BRD", "Artemis Bow" },
            {"BLM", "Stardust Rod" },
            {"SMN", "The Veil of Wiyu" }
        };
        #endregion

        #region Relic
        public static string ReturnRelicMap(string job)
        {
            return ArrBrokenImages[job];
        }

        public static string ReturnRelicDestination(string job)
        {
            return ArrBrokenDestinations[job];
        }

        public static PointF ReturnRelicPoint(string job)
        {
            PointF tempLocation = ARRBrokenLocations[job];
            PointF tempBounds = ARRMapBounds[job];

            return new PointF((tempLocation.X - 1) / tempBounds.X, (tempLocation.Y - 1) / tempBounds.Y);
        }

        public static string ReturnClassWeapon(string job)
        {
            return ArrRelicMats[job];
        }

        public static string ReturnMateria(string job)
        {
            return ArrRelicMaterias[job];
        }

        public static List<string> ReturnBeastmen(string job)
        {
            return ArrRelicBeastmen[job];
        }

        private static readonly Dictionary<string, string> ArrBrokenImages = new Dictionary<string, string>()
        {
            {"PLD", "../ArrMaps/SouthernThanalan.png"},
            {"WAR", "../ArrMaps/OuterLaNoscea.png"},
            {"WHM", "../ArrMaps/OuterLaNoscea.png"},
            {"SCH", "../ArrMaps/WesternLaNoscea.png"},
            {"MNK", "../ArrMaps/SouthernThanalan.png"},
            {"DRG", "../ArrMaps/CoerthasCentralHighlands.png"},
            {"NIN", "../ArrMaps/WesternLaNoscea.png"},
            {"BRD", "../ArrMaps/CoerthasCentralHighlands.png"},
            {"BLM", "../ArrMaps/OuterLaNoscea.png"},
            {"SMN", "../ArrMaps/EastShroud.png"}
        };

        private static readonly Dictionary<string, string> ArrBrokenDestinations = new Dictionary<string, string>()
        {
            {"PLD", "Southern Thanalan"},
            {"WAR", "Outer La Noscea"},
            {"WHM", "Outer La Noscea"},
            {"SCH", "Western La Noscea"},
            {"MNK", "Southern Thanalan"},
            {"DRG", "Coerthas Central Highlands"},
            {"NIN", "Western La Noscea"},
            {"BRD", "Coerthas Central Highlands"},
            {"BLM", "Outer La Noscea"},
            {"SMN", "East Shroud"}
        };

        private static readonly Dictionary<string, PointF> ARRMapBounds = new Dictionary<string, PointF>
        {
            { "PLD",new PointF(41F,41F)},
            { "WAR",new PointF(41F,41F)},
            { "WHM",new PointF(41F,41F)},
            { "SCH",new PointF(41F,41F)},
            { "MNK",new PointF(41F,41F)},
            { "DRG",new PointF(41F,41F)},
            { "NIN",new PointF(41F,41F)},
            { "BRD",new PointF(41F,41F)},
            { "BLM",new PointF(41F,41F)},
            { "SMN",new PointF(41F,41F)}
        };
        private static readonly Dictionary<string, PointF> ARRBrokenLocations = new Dictionary<string, PointF>
        {
            { "PLD",new PointF(30F,19F)},
            { "WAR",new PointF(23F,10F)},
            { "WHM",new PointF(24.3F,6.4F)},
            { "SCH",new PointF(16F,17F)},
            { "MNK",new PointF(32F,18F)},
            { "DRG",new PointF(34F, 21F)},
            { "NIN",new PointF(16F,17F)},
            { "BRD",new PointF(34F, 21F)},
            { "BLM",new PointF(23F,10F)},
            { "SMN",new PointF(25F,19F)},
        };
        private static readonly Dictionary<string, string> ArrRelicMats = new Dictionary<string, string>()
        {
           {"PLD", "Aeolian Scimitar" },
           {"WAR", "Barbarian's Bardiche"},
           {"WHM", "Madman's Whispering Rod" },
           {"SCH", "Erudite's Picatrix of Healing" },
           {"MNK", "Wildling's Cesti" },
           {"DRG", "Champion's Lance" },
           {"NIN", "Vamper's Knives" },
           {"BRD", "Longarm's Composite Bow" },
           {"BLM", "Sanguine Scepter" },
           {"SMN", "Erudite's Picatrix of Casting" }
        };

        private static readonly Dictionary<string, string> ArrRelicMaterias = new Dictionary<string, string>
        {
            {"PLD", "Battle Dance Materia III" },
            {"WAR", "Battle Dance Materia III" },
            {"WHM", "Quicktongue Materia III" },
            {"SCH", "Quicktongue Materia III" },
            {"MNK", "Savage Aim Materia III" },
            {"DRG", "Savage Aim Materia III" },
            {"NIN", "Heavens Eye Materia III" },
            {"BRD", "Heavens Eye Materia III" },
            {"BLM", "Savage Might Materia III" },
            {"SMN", "Savage Might Materia III" }
        };

        private static readonly Dictionary<string, List<string>> ArrRelicBeastmen = new Dictionary<string, List<string>>
        {
             {"PLD", new List<string>{"Zahar'ak Lance","Zahar'ak Pugliest","Zahar'ak Archer" } },
             {"WAR", new List<string>{"13th Order Quarryman","13th Order Bedesman","13th Order Priest" } },
             {"WHM", new List<string>{"13th Order Quarryman","13th Order Bedesman","13th Order Priest" }},
             {"SCH", new List<string>{"Sapsa Shelfspine","Sapsa Shelfclaw", "Sapsa Shelftooth"}},
             {"MNK", new List<string>{"Zahar'ak Lance","Zahar'ak Pugliest","Zahar'ak Archer" }},
             {"DRG", new List<string>{"Natalan Boldwing","Natalan Fogcaller","Natalan Windtalon" }},
             {"NIN", new List<string>{"Sapsa Shelfspine","Sapsa Shelfclaw", "Sapsa Shelftooth"}},
             {"BRD", new List<string>{"Natalan Boldwing","Natalan Fogcaller","Natalan Windtalon" }},
             {"BLM", new List<string>{"13th Order Quarryman","13th Order Bedesman","13th Order Priest" }},
             {"SMN", new List<string>{"Violet Sigh", "Violet Screech","Violet Snarl"}} };
        #endregion

        #region Animus

        public static List<string> BookList = new List<string>
        {
            "SkyFire1Book",
            "SkyFire2Book",
            "SkyFall1Book",
            "SkyFall2Book",
            "NetherFire1Book",
            "NetherFall1Book",
            "SkyWind1Book",
            "SkyWind2Book",
            "SkyEarth1Book"
        };

        public static List<string> ReferenceBooks = new List<string>
            {
                "Book of Skyfire I",
                "Book of Skyfire II",
                "Book of Skyfall I",
                "Book of Skyfall II",
                "Book of Netherfire I",
                "Book of Netherfall I",
                "Book of Skywind I",
                "Book of Skywind II",
                "Book of Skyearth I"
            };

        public static List<string> ReturnCreatureNames(string bookName) { return BookCreatureNames[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnCreatureMaps(string bookName) { return BookCreatureMaps[ReferenceBooks.IndexOf(bookName)]; }

        public static List<PointF> ReturnCreaturePoints(string bookName) { return BookCreaturePoints[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnBookDungeons(string bookName) { return BookDungeons[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnFATENames(string bookName) { return BookFATENames[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnFATEMaps(string bookName) { return BookFATEMaps[ReferenceBooks.IndexOf(bookName)]; }
        public static List<PointF> ReturnFATEPoints(string bookName) { return BookFATEPoints[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnLeveNames(string bookName) { return BookLeveNames[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnLeveTypes(string bookName) { return BookLeveTypes[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnLevePersons(string bookName) { return BookLevePersons[ReferenceBooks.IndexOf(bookName)]; }
        public static List<string> ReturnLeveLocations(string bookName) { return BookLeveLocations[ReferenceBooks.IndexOf(bookName)]; }
        public static List<PointF> ReturnLevePoints(string bookName) { return BookLevePoints[ReferenceBooks.IndexOf(bookName)]; }

        public static List<string> ReturnCreatureNames(int bookIndex) { return BookCreatureNames[bookIndex]; }
        public static List<string> ReturnCreatureMaps(int bookIndex) { return BookCreatureMaps[bookIndex]; }
        public static List<PointF> ReturnCreaturePoints(int bookIndex) { return BookCreaturePoints[bookIndex]; }
        public static List<string> ReturnBookDungeons(int bookIndex) { return BookDungeons[bookIndex]; }
        public static List<string> ReturnFATENames(int bookIndex) { return BookFATENames[bookIndex]; }
        public static List<string> ReturnFATEMaps(int bookIndex) { return BookFATEMaps[bookIndex]; }
        public static List<PointF> ReturnFATEPoints(int bookIndex) { return BookFATEPoints[bookIndex]; }
        public static List<string> ReturnLeveNames(int bookIndex) { return BookLeveNames[bookIndex]; }
        public static List<string> ReturnLeveTypes(int bookIndex) { return BookLeveTypes[bookIndex]; }
        public static List<string> ReturnLevePersons(int bookIndex) { return BookLevePersons[bookIndex]; }
        public static List<string> ReturnLeveLocations(int bookIndex) { return BookLeveLocations[bookIndex]; }
        public static List<PointF> ReturnLevePoints(int bookIndex) { return BookLevePoints[bookIndex]; }


        public static List<List<string>> BookCreatureNames = new List<List<string>>
        {
            new List<string>{"Daring Harrier","5th Cohort Vanguard","4th Cohort Hoplomachus","Basilisk","Zanr'ak Pugilist","Milkroot Cluster","Giant Logger","Synthetic Doblyn","Shoalspine Sahagin","2nd Cohort Hoplomachus"},
            new List<string>{"Raging Harrier","Biast","Natalan Boldwing","Shoaltooth Sahagin","Shelfscale Reaver","U'Ghamaro Golem","Dullahan","Sylpheed Sigh","Zahar'ak Archer","Tempered Gladiator"},
            new List<string>{" Hexing Harrier"," Gigas Bonze"," Giant Lugger"," Wild Hog"," Sylpheed Screech"," U'Ghamaro Roundsman"," Shelfclaw Reaver"," 2nd Cohort Laquearius"," Zahar'ak Fortuneteller"," Tempered Orator"},
            new List<string>{"Mudpuppy","Lake Cobra","Giant Reader","Shelfscale Shagin","Sea Wasp","U'Ghamarro Quarryman","2nd Cohort Equite","Magitek Vanguard","Amalj'aa Lancer","Sylphlands Sentinel"},
            new List<string>{"Gigas Bhikkhu","5th Cohort Hoplomachus","Natalan Watchwolf","Sylph Bonnet","Ked","4th Cohort Laquearius","Iron Tortoise","Shelfeye Reaver","Sapsa Shelfscale","U'Ghamaro Bedesman"},
            new List<string>{" Amalj'aa Brigand"," 4th Cohort Secutor"," 5th Cohort Laquearius"," Gigas Sozu"," Snow Wolf"," Sapsa Shelfclaw"," U'Ghamaro Priest"," Violet Screech"," Ixali Windtalon"," Lesser Kalong"},
            new List<string>{"Hippogryph","5th Cohort Equite","Natalan Windtalon","Sapsa Elbst","Trenchtooth Sahagin","Elite Roundsman","2nd Cohort Secutores","Ahriman","Amalj'aa Thaumaturge","Sylpheed Snarl"},
            new List<string>{"Gigas Shramana","5th Cohort Signifer","Watchwolf","Dreamtoad","Zahar'ak Battle Drake","Amalj'aa Archer","4th Cohort Signifer","Elite Priest","Sapsa Shelftooth","Natalan Fogcaller"},
            new List<string>{"Violet Sigh","Ixali Boldwing","Amalj'aa Scavenger","Zahar'ak Pugilist","Axolotl","Elite Quarryman","2nd Cohort Signifer","Natalan Swiftbeak","5th Cohort Secutor","Hapalit"}
        };
        public static List<List<string>> BookCreatureMaps = new List<List<string>>
        {
            new List<string>{"Mor Dhona","Mor Dhona","Western Thanalan","Northern Thanalan","Southern Thanalan","East Shroud","Coerthas Central Highlands","Outer La Noscea","Western La Noscea","Eastern La Noscea"},
            new List<string>{"Mor Dhona","Coerthas Central Highlands","Coerthas Central Highlands","Western La Noscea","Western La Noscea","Outer La Noscea","North Shroud","East Shroud","Southern Thanalan","Southern Thanalan"},
            new List<string>{"Mor Dhona","Mor Dhona","Coerthas Central Highlands","South Shroud","East Shroud","Outer La Noscea","Western La Noscea","Eastern La Noscea","Southern Thanalan","Southern Thanalan"},
            new List<string>{"Mor Dhona","Mor Dhona","Coerthas Central Highlands","Western La Noscea","Western La Noscea","Outer La Noscea","Eastern La Noscea","Northern Thanalan","Southern Thanalan","East Shroud"},
            new List<string>{"Mor Dhona","Mor Dhona","Coerthas Central Highlands","East Shroud","South Shroud","Western Thanalan","Southern Thanalan","Western La Noscea","Western La Noscea","Outer La Noscea"},
            new List<string>{"Southern Thanalan","Western Thanalan","Mor Dhona","Mor Dhona","Coerthas Central Highlands","Western La Noscea","Outer La Noscea","East Shroud","North Shroud","North Shroud"},
            new List<string>{"Mor Dhona","Mor Dhona","Coerthas Central Highlands","Western La Noscea","Western La Noscea","Outer La Noscea","Eastern La Noscea","Northern Thanalan","Southern Thanalan","East Shroud"},
            new List<string>{"Mor Dhona","Mor Dhona","North Shroud","East Shroud","Southern Thanalan","Southern Thanalan","Western Thanalan","Outer La Noscea","Western La Noscea","Coerthas Central Highlands"},
            new List<string>{"East Shroud","North Shroud","Southern Thanalan","Southern Thanalan","Western La Noscea","Outer La Noscea","Eastern La Noscea","Coerthas Central Highlands","Mor Dhona","Mor Dhona"}
        };
        public static List<List<PointF>> BookCreaturePoints = new List<List<PointF>>
        {
            new List<PointF>{new PointF(16.9F,16.4F),new PointF(10.6F,15.1F),new PointF(10.5F,6F),new PointF(22.8F,22.9F),new PointF(19F,25F),new PointF(23F,16F),new PointF(13F,25F),new PointF(23F,8F),new PointF(17F,15F),new PointF(25F,21F)},
            new List<PointF>{new PointF(17F,17F),new PointF(16F,30F),new PointF(33F,18F),new PointF(17F,17F),new PointF(13F,17F),new PointF(27.5F,7.3F),new PointF(22.5F,20F),new PointF(29F,17F),new PointF(25F,21F),new PointF(21.5F,19.6F)},
            new List<PointF>{new PointF(16F,15F),new PointF(27F,9F),new PointF(13F,27F),new PointF(29F,24F),new PointF(28F,15F),new PointF(23F,7F),new PointF(13F,17F),new PointF(29F,20F),new PointF(29F,19F),new PointF(21F,20F)}
            ,new List<PointF>{new PointF(14F,11F),new PointF(26F,12F),new PointF(12F,25F),new PointF(17F,19F),new PointF(14F,17F),new PointF(23F,7F),new PointF(29F,21F),new PointF(17F,17F),new PointF(20F,20F),new PointF(20F,10F)}
            ,new List<PointF>{new PointF(33F,14F),new PointF(12F,12F),new PointF(31F,17F),new PointF(26F,13F),new PointF(31F,23F),new PointF(10F,6F),new PointF(16F,24F),new PointF(13F,17F),new PointF(14F,14F),new PointF(23F,8F)}
            ,new List<PointF>{new PointF(20F,21F),new PointF(9F,6F),new PointF(12F,12F),new PointF(29F,14F),new PointF(13F,30F),new PointF(14F,15F),new PointF(22F,8F),new PointF(24F,14F),new PointF(19F,19F),new PointF(30F,25F)}
            ,new List<PointF>{new PointF(33F,11F),new PointF(12F,12F),new PointF(34F,22F),new PointF(17F,15F),new PointF(20F,19F),new PointF(25F,8F),new PointF(25F,21F),new PointF(24F,21F),new PointF(18F,19F),new PointF(28F,17F)}
            ,new List<PointF>{new PointF(28F,13F),new PointF(10F,13F),new PointF(19F,19F),new PointF(26F,18F),new PointF(30F,19F),new PointF(20F,22F),new PointF(11F,7F),new PointF(24F,7F),new PointF(15F,15F),new PointF(32F,18F)}
            ,new List<PointF>{new PointF(24F,13F),new PointF(21F,20F),new PointF(20F,21F),new PointF(23F,21F),new PointF(14F,15F),new PointF(24F,7F),new PointF(30F,20F),new PointF(31F,17F),new PointF(10F,13F),new PointF(30F,5F)}


        };

        public static List<List<string>> BookDungeons = new List<List<string>>
        {
             new List<string>{"The Tam-Tara Deepcroft","The Stone Vigil","The Lost City of Amdapor"}
             ,new List<string>{"Brayflox's Longstop","The Wanderer's Palace","Copperbell Mines (Hard)"}
             ,new List<string>{"The Sunken Temple of Qarn","Haukke Manor (Hard)","Halatali (Hard)"}
             ,new List<string>{"Copperbell Mines","Dzemael Darkhold","Brayflox's Longstop (Hard)"}
             ,new List<string>{"The Thousand Maws of Toto-Rak","Amdapor Keep","Haukke Manor (Hard)"}
             ,new List<string>{"Cutter's Cry","Pharos Sirius","The Lost City of Amdapor"}
             ,new List<string>{"Sastasha","The Aurum Vale","Halatali (Hard)"}
             ,new List<string>{"Haukke Manor","Copperbell Mines (Hard)","Brayflox's Longstop (Hard)"}
             ,new List<string>{"Pharos Sirius","Halatali","Amdapor Keep"}
        };

        public static List<List<string>> BookFATENames = new List<List<string>>
        {
            new List<string>{"Giant Seps","Make it Rain","The Enmity of My Enemy"},
            new List<string>{"Heroes of the 2nd","Breaching South Tidegate","Air Supply"},
            new List<string>{"Another Notch on the Torch","Everything's Better","Return to Cinder"},
            new List<string>{"Bellyfull","The King's Justice","Quartz Coupling"},
            new List<string>{"Black and Nburu","Breaching North Tidegate","Breaking Dawn"},
            new List<string>{"Rude Awakening","The Ceruleum Road","The Four Winds"},
            new List<string>{"Surprise","In Spite of It All","Good to Be Bud"},
            new List<string>{"Taken","Tower of Power","What Gored Before"},
            new List<string>{"The Taste of Fear","The Big Bagoly Theory","Schism"}

        };
        public static List<List<string>> BookFATEMaps = new List<List<string>>
        {
            new List<string>{"Coerthas Central Highlands","Outer La Noscea","East Shroud"},
            new List<string>{"Southern Thanalan","Western La Noscea","North Shroud"},
            new List<string>{"Mor Dhona","East Shroud","Southern Thanalan"},
            new List<string>{"Coerthas Central Highlands","Western La Noscea","Eastern Thanalan"},
            new List<string>{"Mor Dhona","Western La Noscea","East Shroud"},
            new List<string>{"North Shroud","Northern Thanalan","Coerthas Central Highlands"},
            new List<string>{"Upper La Noscea","Central Shroud","Mor Dhona"},
            new List<string>{"Southern Thanalan","Coerthas Central Highlands","South Shroud"},
            new List<string>{"Coerthas Central Highlands","Eastern Thanalan","Outer La Noscea"}

        };
        public static List<List<PointF>> BookFATEPoints = new List<List<PointF>>
        {
            new List<PointF>{new PointF(8.6F,12F),new PointF(25F,18F),new PointF(27F,21.6F)},
            new List<PointF>{new PointF(21F,16F),new PointF(18F,22F),new PointF(19F,20F)},
            new List<PointF>{new PointF(31,y:5F),new PointF(23,y:14F),new PointF(24,y:26F)},
            new List<PointF>{new PointF(34F,14F),new PointF(14F,34F),new PointF(26F,24F)},
            new List<PointF>{new PointF(15F,13F),new PointF(21F,19F),new PointF(32F,14F)},
            new List<PointF>{new PointF(21,y:19F),new PointF(21,y:29F),new PointF(34,y:20F)},
            new List<PointF>{new PointF(26F,18F),new PointF(11F,18F),new PointF(13F,12F)},
            new List<PointF>{new PointF(18F,20F),new PointF(10F,28F),new PointF(32F,25F)},
            new List<PointF>{new PointF(4F,21F),new PointF(30F,25F),new PointF(25F,16F)}

        };

        public static List<List<string>> BookLeveNames = new List<List<string>>
        {
            new List<string>{"Necrologos Pale Oblation","An Imp Mobile","The Awry Salvages"},
            new List<string>{"Don't Forget to Cry","Yellow Is the New Black","The Museum Is Closed"},
            new List<string>{"Circling the Ceruleum","If You Put It That Way","One Big Problem Solved"},
            new List<string>{"Circling the Ceruleum","Necrologos: Whispers of the Gem","Go Home to Mama"},
            new List<string>{"Someone's in the Doghouse","Get Off Our Lake","The Area's a Bit Sketchy"},
            new List<string>{"Got a Gut Feeling about This","Subduing the Subprime","Who Writes History"},
            new List<string>{"Subduing the Subprime","Someone's Got a Big Mouth","Big, Bad Idea"},
            new List<string>{"Necrologos: Pale Oblation","The Bloodhounds of Coerthas","Put Your Stomp on It"},
            new List<string>{"Don't Forget to Cry","Necrologos: The Liminal Ones","No Big Whoop"}

        };
        public static List<List<string>> BookLeveTypes = new List<List<string>>
        {
            new List<string>{"General","Maelstrom Grand Company","Twin Adder Grand Company"},
            new List<string>{"General","Twin Adder Grand Company","Immortal Flames Grand Company"},
            new List<string>{"General","Immortal Flames Grand Company","Maelstrom Grand Company"},
            new List<string>{"General","General","Maelstrom Grand Company"},
            new List<string>{"General","Twin Adder Grand Company","General"},
            new List<string>{"General","General","Immortal Flames Grand Company"},
            new List<string>{"General","Maelstrom Grand Company","General"},
            new List<string>{"General","Twin Adder Grand Company","General"},
            new List<string>{"General","General","Immortal Flames Grand Company"}

        };
        public static List<List<string>> BookLevePersons = new List<List<string>>
        {
            new List<string>{"Rurubana","Lodille","Eidhart"},
            new List<string>{"Rurubana","Lodille","Eidhart"},
            new List<string>{"Rurubana","Lodille","Eidhart"},
            new List<string>{"Rurubana","Voilinaut","Eidhart"},
            new List<string>{"Rurubana","Eidhart","Voilinaut"},
            new List<string>{"Voilinaut","Rurubana","Eidhart"},
            new List<string>{"Rurubana","Lodille","K'leytai"},
            new List<string>{"Rurubana","Lodille","K'leytai"},
            new List<string>{"Rurubana","K'leytai","Lodille"}

        };
        public static List<List<string>> BookLeveLocations = new List<List<string>>
        {
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Mor Dhona","Coerthas Central Highlands"},
            new List<string>{"Coerthas Central Highlands","Northern Thanalan","Mor Dhona"},
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Coerthas Central Highlands","Mor Dhona"},
            new List<string>{"Northern Thanalan","Mor Dhona","Coerthas Central Highlands"}

        };
        public static List<List<PointF>> BookLevePoints = new List<List<PointF>>
        {
            new List<PointF>{new PointF(22F,29F),new PointF(11.9F,16.8F),new PointF(30F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(11F,16F),new PointF(30F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(11F,16F),new PointF(30F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(12F,16F),new PointF(30F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(30F,12F),new PointF(11F,16F)},
            new List<PointF>{new PointF(12F,16F),new PointF(22F,29F),new PointF(30F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(11F,16F),new PointF(29F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(11F,16F),new PointF(29F,12F)},
            new List<PointF>{new PointF(22F,29F),new PointF(29F,12F),new PointF(11F,16F)}
        };


        #endregion
    }
}
