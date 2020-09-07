using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker.ARR.Novus
{
    
    public class NovusModel
    {
        public NovusModel()
        {
            PLDNovus = false;
            NonPLDNovus = true;
        }

        internal int HeavenEyeCount ;
        internal int QuickarmCount ;
        internal int SavageAimCount ;
        internal int BattledanceCount ;
        internal int QuicktongueCount ;
        internal int PietyCount ;
        internal int SavageMightCount ;

        internal int MateriaSum ;
        internal int HeavenEyeMax ;

        internal int QuickarmMax ;
        internal int SavageAimMax ;
        internal int PietyMax ;
        internal int SavageMightMax ;
        internal int QuicktongueMax ;
        internal int BattledanceMax ;

        internal string CurrentNovus;
        internal bool PLDNovus;
        internal bool NonPLDNovus;
        internal int BattledanceSwordCount;
        internal int QuicktongueSwordCount;
        internal int SavageMightSwordCount;
        internal int PietySwordCount;
        internal int SavageAimSwordCount;
        internal int QuickarmSwordCount;
        internal int HeavenEyeSwordCount;
        internal int MateriaSwordSum;
        internal int BattledanceShieldCount;
        internal int QuicktongueShieldCount;
        internal int SavageMightShieldCount;
        internal int PietyShieldCount;
        internal int SavageAimShieldCount;
        internal int QuickarmShieldCount;
        internal int HeavenEyeShieldCount;
        internal int MateriaShieldSum;
        internal string WeaponName;
        internal bool KnownWeapon;
        internal bool ShowContent=false;
    }
}
