using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFXIVRelicTracker._03_HW.Main
{
    public class HWModel : ObservableObject
    {
        #region Fields

        private HWJob pld = new HWJob("PLD");
        private HWJob war = new HWJob("WAR");
        private HWJob drk = new HWJob("DRK");
        private HWJob whm = new HWJob("WHM");
        private HWJob sch = new HWJob("SCH");
        private HWJob ast = new HWJob("AST");
        private HWJob mnk = new HWJob("MNK");
        private HWJob drg = new HWJob("DRG");
        private HWJob nin = new HWJob("NIN");
        private HWJob brd = new HWJob("BRD");
        private HWJob mch = new HWJob("MCH");
        private HWJob blm = new HWJob("BLM");
        private HWJob smn = new HWJob("SMN");

        //private AnimatedModel animatedModel = new AnimatedModel();
        //private AwokenModel awokenModel = new AwokenModel();
        //private AnimaModel animaModel = new AnimaModel();
        //private HyperconductiveModel hyperconductiveModel = new HyperconductiveModel();
        //private ReconditionedModel reconditionedModel = new ReconditionedModel();
        //private SharpenedModel sharpenedModel = new SharpenedModel();
        //private CompleteModel completeModel = new CompleteModel();
        //private LuxModel luxModel = new LuxModel();

        #endregion

        #region Constructors
        public HWModel()
        {
        }

        #endregion

        #region Properties
        public List<HWJob> HWJobList = new List<HWJob>();
        public HWJob PLD { get { return pld; } set { pld = value; OnPropertyChanged(nameof(PLD)); } }
        public HWJob WAR { get { return war; } set { war = value; OnPropertyChanged(nameof(WAR)); } }
        public HWJob DRK { get { return drk; } set { drk = value; OnPropertyChanged(nameof(DRK)); } }
        public HWJob WHM { get { return whm; } set { whm = value; OnPropertyChanged(nameof(WHM)); } }
        public HWJob SCH { get { return sch; } set { sch = value; OnPropertyChanged(nameof(SCH)); } }
        public HWJob AST { get { return ast; } set { ast = value; OnPropertyChanged(nameof(AST)); } }
        public HWJob MNK { get { return mnk; } set { mnk = value; OnPropertyChanged(nameof(MNK)); } }
        public HWJob DRG { get { return drg; } set { drg = value; OnPropertyChanged(nameof(DRG)); } }
        public HWJob NIN { get { return nin; } set { nin = value; OnPropertyChanged(nameof(NIN)); } }
        public HWJob BRD { get { return brd; } set { brd = value; OnPropertyChanged(nameof(BRD)); } }
        public HWJob MCH { get { return mch; } set { mch = value; OnPropertyChanged(nameof(MCH)); } }
        public HWJob BLM { get { return blm; } set { blm = value; OnPropertyChanged(nameof(BLM)); } }
        public HWJob SMN { get { return smn; } set { smn = value; OnPropertyChanged(nameof(SMN)); } }

        //public AnimatedModel AnimatedModel { get { return animatedModel; } set { animatedModel = value; OnPropertyChanged(nameof(AnimatedModel)); } }
        //public AwokenModel AwokenModel { get { return awokenModel; } set { awokenModel = value; OnPropertyChanged(nameof(AwokenModel)); } }
        //public AnimaModel AnimaModel { get { return animaModel; } set { animaModel = value; OnPropertyChanged(nameof(AnimaModel)); } }
        //public HyperconductiveModel HyperconductiveModel { get { return hyperconductiveModel; } set { hyperconductiveModel = value; OnPropertyChanged(nameof(HyperconductiveModel)); } }
        //public ReconditionedModel ReconditionedModel { get { return reconditionedModel; } set { reconditionedModel = value; OnPropertyChanged(nameof(ReconditionedModel)); } }
        //public SharpenedModel SharpenedModel { get { return sharpenedModel; } set { sharpenedModel = value; OnPropertyChanged(nameof(SharpenedModel)); } }
        //public CompleteModel CompleteModel { get { return completeModel; } set { completeModel = value; OnPropertyChanged(nameof(CompleteModel)); } }
        //public LuxModel LuxModel { get { return luxModel; } set { luxModel = value; OnPropertyChanged(nameof(LuxModel)); } }
        #endregion

    }
}
