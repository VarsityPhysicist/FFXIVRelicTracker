using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace FFXIVRelicTracker._05_Skysteel.Main
{
    public class SkysteelModel : ObservableObject
    {
        #region Fields

        private SkysteelJob crp = new SkysteelJob("CRP");
        private SkysteelJob bsm = new SkysteelJob("BSM");
        private SkysteelJob arm = new SkysteelJob("ARM");
        private SkysteelJob gsm = new SkysteelJob("GSM");
        private SkysteelJob ltw = new SkysteelJob("LTW");
        private SkysteelJob wvr = new SkysteelJob("WVR");
        private SkysteelJob alc = new SkysteelJob("ALC");
        private SkysteelJob cul = new SkysteelJob("CUL");

        private SkysteelJob min = new SkysteelJob("MIN");
        private SkysteelJob btm = new SkysteelJob("BTM");
        private SkysteelJob fsh = new SkysteelJob("FSH");

        #endregion

        #region Constructor
        public SkysteelModel()
        {

        }

        #endregion

        #region Properties

        public List<SkysteelJob> SkysteelJobList = new List<SkysteelJob>();

        public SkysteelJob CRP {get{return crp ;} set{ crp=value; OnPropertyChanged(nameof(CRP));} }
        public SkysteelJob BSM {get{return bsm ;} set{ bsm=value; OnPropertyChanged(nameof(BSM));} }
        public SkysteelJob ARM {get{return arm ;} set{ arm=value; OnPropertyChanged(nameof(ARM));} }
        public SkysteelJob GSM {get{return gsm ;} set{ gsm=value; OnPropertyChanged(nameof(GSM));} }
        public SkysteelJob LTW {get{return ltw ;} set{ ltw=value; OnPropertyChanged(nameof(LTW));} }
        public SkysteelJob WVR {get{return wvr ;} set{ wvr=value; OnPropertyChanged(nameof(WVR));} }
        public SkysteelJob ALC {get{return alc ;} set{ alc=value; OnPropertyChanged(nameof(ALC));} }
        public SkysteelJob CUL {get{return cul ;} set{ cul=value; OnPropertyChanged(nameof(CUL));} }
                                                                                                   
        public SkysteelJob MIN {get{return min ;} set{ min=value; OnPropertyChanged(nameof(MIN));} }
        public SkysteelJob BTM {get{return btm ;} set{ btm=value; OnPropertyChanged(nameof(BTM));} }
        public SkysteelJob FSH { get { return fsh; } set { fsh = value; OnPropertyChanged(nameof(FSH)); } }

        #endregion
    }
}
