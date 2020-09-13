﻿using FFXIVRelicTracker._05_ShB.Main;
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

        #endregion
        public Character()
        {
            Name = "Default Name";
            Server = "Default Server";

            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
        }

        public Character(string name, string server )
        {
            Name = name;
            Server = server;
            ArrProgress = new ArrModel();
            ShBModel = new ShBModel();
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
