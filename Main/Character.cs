using FFXIVRelicTracker._03_HW.Main;
using FFXIVRelicTracker._05_ShB.Main;
using FFXIVRelicTracker._05_Skysteel.Main;
using FFXIVRelicTracker.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace FFXIVRelicTracker.Models
{
    
    
    public class Character : ObservableObject
    {
        private string name;
        private string server;
        private ArrModel arrProgress;
        private ShBModel shbModel;
        private SkysteelModel skysteelModel;
        private HWModel hwModel;

        #region Properties

        public string Name
        {
            get { return name; }
            set
            {
                if(name!=value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string Server
        {
            get { return server; }
            set
            {
                if (server != value)
                {
                    server = value;
                    OnPropertyChanged(nameof(Server));
                }
            }
        }
        public ArrModel ArrProgress
        {
            get { return arrProgress; }
            set
            {
                if (value != null)
                {
                    arrProgress = value;
                    OnPropertyChanged(nameof(ArrProgress));
                }
            }
        }
        public HWModel HWModel
        {
            get { return hwModel; }
            set
            {
                if (value != null)
                {
                    hwModel = value;
                    OnPropertyChanged(nameof(HWModel));
                }
            }
        }
        public ShBModel ShBModel
        {
            get { return shbModel; }
            set
            {
                if (value != null)
                {
                    shbModel = value;
                    OnPropertyChanged(nameof(ShBModel));
                }
            }
        }
        public SkysteelModel SkysteelModel
        {
            get { return skysteelModel; }
            set
            {
                if (value != null)
                {
                    skysteelModel = value;
                    OnPropertyChanged(nameof(SkysteelModel));
                }
            }
        }
        #endregion
        public Character()
        {
            Name = "Default Name";
            Server = "Default Server";

            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
            SkysteelModel = new SkysteelModel();
            HWModel = new HWModel();
        }

        public Character(string name, string server )
        {
            Name = name;
            Server = server;
            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
            HWModel = new HWModel();
            SkysteelModel = new SkysteelModel();
        }

        public Character(Character oldCharacter)
            :this()
        {
            foreach (PropertyInfo propertyInfo in oldCharacter.GetType().GetProperties())
            {

                if (propertyInfo.GetValue(oldCharacter) != null)
                {
                    propertyInfo.SetValue(this, propertyInfo.GetValue(oldCharacter));
                }
            }
        }

        public override string ToString()
        {
            return name + " : " + server;
        }
    }
}
