using FFXIVRelicTracker._05_ShB._01_Resistance;
using FFXIVRelicTracker._05_ShB._02_AugmentedResistance;
using FFXIVRelicTracker._05_ShB._03_Recollection;
using FFXIVRelicTracker._05_ShB._04_LawsOrder;
using FFXIVRelicTracker._05_ShB._05_AugmentedLawsOrder;
using FFXIVRelicTracker._05_ShB._06_Blades;
using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._05_ShB.Main
{
    public class ShBModel : ObservableObject
    {


        #region Fields

        private ShBJob pld = new ShBJob("PLD");
        private ShBJob war = new ShBJob("WAR");
        private ShBJob drk = new ShBJob("DRK");
        private ShBJob gnb = new ShBJob("GNB");
        private ShBJob whm = new ShBJob("WHM");
        private ShBJob sch = new ShBJob("SCH");
        private ShBJob ast = new ShBJob("AST");
        private ShBJob mnk = new ShBJob("MNK");
        private ShBJob drg = new ShBJob("DRG");
        private ShBJob nin = new ShBJob("NIN");
        private ShBJob sam = new ShBJob("SAM");
        private ShBJob brd = new ShBJob("BRD");
        private ShBJob mch = new ShBJob("MCH");
        private ShBJob dnc = new ShBJob("DNC");
        private ShBJob blm = new ShBJob("BLM");
        private ShBJob smn = new ShBJob("SMN");
        private ShBJob rdm = new ShBJob("RDM");

        private ResistanceModel resistanceModel = new ResistanceModel();
        private AugmentedResistanceModel augmentedResistanceModel = new AugmentedResistanceModel();
        private RecollectionModel recollectionModel = new RecollectionModel();
        private LawsOrderModel lawsOrderModel = new LawsOrderModel();
        private AugmentedLawsOrderModel augmentedLawsOrderModel = new AugmentedLawsOrderModel();
        private BladesModel bladesModel = new BladesModel();

        #endregion

        #region Constructors
        public ShBModel()
        {
        }

        #endregion

        #region Properties
        public List<ShBJob> ShbJobList = new List<ShBJob>();
        public ShBJob PLD { get { return pld; } set { pld = value; OnPropertyChanged(nameof(PLD)); } }
        public ShBJob WAR { get { return war; } set { war = value; OnPropertyChanged(nameof(WAR)); } }
        public ShBJob DRK { get { return drk; } set { drk = value; OnPropertyChanged(nameof(DRK)); } }
        public ShBJob GNB { get { return gnb; } set { gnb = value; OnPropertyChanged(nameof(GNB)); } }
        public ShBJob WHM { get { return whm; } set { whm = value; OnPropertyChanged(nameof(WHM)); } }
        public ShBJob SCH { get { return sch; } set { sch = value; OnPropertyChanged(nameof(SCH)); } }
        public ShBJob AST { get { return ast; } set { ast = value; OnPropertyChanged(nameof(AST)); } }
        public ShBJob MNK { get { return mnk; } set { mnk = value; OnPropertyChanged(nameof(MNK)); } }
        public ShBJob DRG { get { return drg; } set { drg = value; OnPropertyChanged(nameof(DRG)); } }
        public ShBJob NIN { get { return nin; } set { nin = value; OnPropertyChanged(nameof(NIN)); } }
        public ShBJob SAM { get { return sam; } set { sam = value; OnPropertyChanged(nameof(SAM)); } }
        public ShBJob BRD { get { return brd; } set { brd = value; OnPropertyChanged(nameof(BRD)); } }
        public ShBJob MCH { get { return mch; } set { mch = value; OnPropertyChanged(nameof(MCH)); } }
        public ShBJob DNC { get { return dnc; } set { dnc = value; OnPropertyChanged(nameof(DNC)); } }
        public ShBJob BLM { get { return blm; } set { blm = value; OnPropertyChanged(nameof(BLM)); } }
        public ShBJob SMN { get { return smn; } set { smn = value; OnPropertyChanged(nameof(SMN)); } }
        public ShBJob RDM { get { return rdm; } set { rdm = value; OnPropertyChanged(nameof(RDM)); } }

        public ResistanceModel ResistanceModel { get { return resistanceModel; } set { resistanceModel = value; OnPropertyChanged(nameof(ResistanceModel)); } }
        public AugmentedResistanceModel AugmentedResistanceModel { get { return augmentedResistanceModel; } set { augmentedResistanceModel = value; OnPropertyChanged(nameof(AugmentedResistanceModel)); } }
        public RecollectionModel RecollectionModel { get { return recollectionModel; } set { recollectionModel = value; OnPropertyChanged(nameof(RecollectionModel)); } }
        public LawsOrderModel LawsOrderModel { get { return lawsOrderModel; } set { lawsOrderModel = value; OnPropertyChanged(nameof(LawsOrderModel)); } }
        public AugmentedLawsOrderModel AugmentedLawsOrderModel { get { return augmentedLawsOrderModel; } set { augmentedLawsOrderModel = value; OnPropertyChanged(nameof(AugmentedLawsOrderModel)); } }
        public BladesModel BladesModel { get { return bladesModel; } set { bladesModel = value; OnPropertyChanged(nameof(BladesModel)); } }
        #endregion

    }

}
