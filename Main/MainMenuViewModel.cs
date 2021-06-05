﻿using FFXIVRelicTracker._03_HW.HWHelpers;
using FFXIVRelicTracker._05_ShB.ShBHelpers;
using FFXIVRelicTracker._05_Skysteel.Skysteel_Helpers;
using FFXIVRelicTracker.ARR.ArrHelpers;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using FFXIVRelicTracker.Views;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FFXIVRelicTracker.ViewModels
{
    public class MainMenuViewModel: ObservableObject, IPageViewModel
    {

        protected IEventAggregator _eventAggregator;

        public MainMenuViewModel()
        {
            AddCharacter("Default Name", "Default Server");
        }

        public MainMenuViewModel(IEventAggregator _eventAggregator)
        {
            this._eventAggregator = _eventAggregator;

        }

        private Character selectedCharacter;
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
                CommandManager.InvalidateRequerySuggested();

                if (value != null)
                {
                    ConfigureARRLists();
                    ConfigureHWLists();
                    ConfigureShBLists();
                    ConfigureSkysteelLists();
                }

                CharacterInt = CharacterList.IndexOf(SelectedCharacter);
                OnPropertyChanged(nameof(SelectedCharacter));
                this._eventAggregator.GetEvent<PubSubEvent<Character>>().Publish(this.SelectedCharacter);
            }
        }

        private int characterInt;
        public int CharacterInt
        {
            get { return characterInt; }
            set
            {
                characterInt = value;
                OnPropertyChanged(nameof(CharacterInt));
            }
        }

        private ObservableCollection<Character> characterList = new ObservableCollection<Character>();
        public ObservableCollection<Character> CharacterList
        {
            get { return characterList; }
            set
            {
                characterList = value;
                OnPropertyChanged(nameof(CharacterList));
            }
        }

        #region Configure Job Lists
        //Job list provides an easy way of accessing the individual models for tracking progress
        //Re-instantiating them here is required as loading the character list unlinks the 
        //  objects within the list from the objects they're supposed to refer to

        //Additionally, with ShB and Skysteel tracking, as new stages of progression are created
        //  a procedure for generating them within existing object is required, otherwise they will generate
        //  null reference errors

        private void ConfigureARRLists()
        {
            List<ArrJobs> arrStages = new List<ArrJobs>()
            {
                selectedCharacter.ArrProgress.ArrWeapon.PLD,
                selectedCharacter.ArrProgress.ArrWeapon.WAR,
                selectedCharacter.ArrProgress.ArrWeapon.WHM,
                selectedCharacter.ArrProgress.ArrWeapon.SCH,
                selectedCharacter.ArrProgress.ArrWeapon.MNK,
                selectedCharacter.ArrProgress.ArrWeapon.DRG,
                selectedCharacter.ArrProgress.ArrWeapon.NIN,
                selectedCharacter.ArrProgress.ArrWeapon.BRD,
                selectedCharacter.ArrProgress.ArrWeapon.BLM,
                selectedCharacter.ArrProgress.ArrWeapon.SMN
            };

            selectedCharacter.ArrProgress.ArrWeapon.JobList = arrStages;

            foreach(ArrJobs job in selectedCharacter.ArrProgress.ArrWeapon.JobList)
            {
                List<ArrProgress> arrProgresses = new List<ArrProgress>()
                {
                    job.Relic,
                    job.Zenith,
                    job.Atma,
                    job.Animus,
                    job.Novus,
                    job.Nexus,
                    job.Braves,
                    job.Zeta
                };
                job.StageList = arrProgresses;
            }
        }
        private void ConfigureHWLists()
        {
            List<HWJob> hwStages = new List<HWJob>()
            {
                selectedCharacter.HWModel.PLD,
                selectedCharacter.HWModel.WAR,
                selectedCharacter.HWModel.DRK,
                selectedCharacter.HWModel.WHM,
                selectedCharacter.HWModel.SCH,
                selectedCharacter.HWModel.AST,
                selectedCharacter.HWModel.MNK,
                selectedCharacter.HWModel.DRG,
                selectedCharacter.HWModel.NIN,
                selectedCharacter.HWModel.BRD,
                selectedCharacter.HWModel.MCH,
                selectedCharacter.HWModel.BLM,
                selectedCharacter.HWModel.SMN,
            };

            selectedCharacter.HWModel.HWJobList = hwStages;

            foreach (HWJob job in selectedCharacter.HWModel.HWJobList)
            {
                List<HWProgress> hwProgresses = new List<HWProgress>()
                {
                    job.Animated,
                    job.Awoken,
                    job.Anima,
                    job.Hyperconductive,
                    job.Reconditioned,
                    job.Sharpened,
                    job.Complete,
                    job.Lux
                };
                job.StageList = hwProgresses;
                job.CheckObject();
            }
        }
        private void ConfigureShBLists()
        {
            List<ShBJob> shbStages = new List<ShBJob>()
            {
                selectedCharacter.ShBModel.PLD,
                selectedCharacter.ShBModel.WAR,
                selectedCharacter.ShBModel.DRK,
                selectedCharacter.ShBModel.GNB,
                selectedCharacter.ShBModel.WHM,
                selectedCharacter.ShBModel.SCH,
                selectedCharacter.ShBModel.AST,
                selectedCharacter.ShBModel.MNK,
                selectedCharacter.ShBModel.DRG,
                selectedCharacter.ShBModel.NIN,
                selectedCharacter.ShBModel.SAM,
                selectedCharacter.ShBModel.BRD,
                selectedCharacter.ShBModel.MCH,
                selectedCharacter.ShBModel.DNC,
                selectedCharacter.ShBModel.BLM,
                selectedCharacter.ShBModel.SMN,
                selectedCharacter.ShBModel.RDM
            };

            selectedCharacter.ShBModel.ShbJobList = shbStages;

            foreach (ShBJob job in selectedCharacter.ShBModel.ShbJobList)
            {
                List<ShBProgress> shbProgresses = new List<ShBProgress>()
                {
                    job.Resistance,
                    job.AugmentedResistance,
                    job.Recollection,
                    job.LawsOrder,
                    job.AugmentedLawsOrder,
                    job.Blades
                };
                job.StageList = shbProgresses;
                job.CheckObject();
            }
        }

        private void ConfigureSkysteelLists()
        {
            List<SkysteelJob> skysteelStages = new List<SkysteelJob>()
            {
                selectedCharacter.SkysteelModel.CRP,
                selectedCharacter.SkysteelModel.BSM,
                selectedCharacter.SkysteelModel.ARM,
                selectedCharacter.SkysteelModel.GSM,
                selectedCharacter.SkysteelModel.LTW,
                selectedCharacter.SkysteelModel.WVR,
                selectedCharacter.SkysteelModel.ALC,
                selectedCharacter.SkysteelModel.CUL,
                selectedCharacter.SkysteelModel.MIN,
                selectedCharacter.SkysteelModel.BTN,
                selectedCharacter.SkysteelModel.FSH
            };

            selectedCharacter.SkysteelModel.SkysteelJobList = skysteelStages;

            foreach (SkysteelJob job in selectedCharacter.SkysteelModel.SkysteelJobList)
            {
                List<SkysteelProgress> skysteelProgresses = new List<SkysteelProgress>()
                {
                    job.BaseTool,
                    job.BasePlus1,
                    job.Dragonsung,
                    job.AugmentedDragonsung,
                    job.Skysung
                };
                job.StageList = skysteelProgresses;
                job.CheckObject();
            }
        }


        #endregion


        #region Add Character
        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(
                        param => this.AddObject(),
                        param => this.CanAdd()
                        );
                }
                return _AddCommand;
            }
        }

        private bool CanAdd()
        {
            return true;
        }
        private void AddObject()
        {
            AddCharacterWindow AddingWindow = new AddCharacterWindow();
            AddingWindow.Closing += new CancelEventHandler(Add_Closing);
            AddingWindow.ShowInTaskbar = false;
            AddingWindow.Owner = Application.Current.MainWindow;
            AddingWindow.Show();
        }

        void Add_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            AddCharacterWindow AddingWindow = sender as AddCharacterWindow;

            string Name = AddingWindow.NewChar.Text;
            string Server = AddingWindow.NewServer.Text;

            if (Name == "" | Server == "")
            {
                return;
            }
            AddCharacter(Name, Server);
        }

        private void AddCharacter(string name, string server)
        {
            Character newChar = new Character(name, server);
            characterList.Insert(0, newChar);
        }

        #endregion

        #region Remove Character
        private ICommand _RemoveCommand;
        public ICommand RemoveCommand
        {
            get
            {
                if (_RemoveCommand == null)
                {
                    _RemoveCommand = new RelayCommand(
                        param => this.RemoveObject(),
                        param => this.CanRemove()
                        );
                }
                return _RemoveCommand;
            }
        }

        private bool CanRemove()
        {
            return selectedCharacter != null;
        }

        private void RemoveObject()
        {
            characterList.RemoveAt(CharacterInt);
            if (characterList.Count == 0) { SelectedCharacter = null; }
            //OnPropertyChanged(nameof(selectedCharacter));
        }

        #endregion

        #region Load Region
        private ICommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new RelayCommand(
                        param => this.LoadObject(),
                        param => this.CanLoad()
                        );
                }
                return _LoadCommand;
            }
        }

        private bool CanLoad()
        {
            return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Characters.txt"); 
        }

        private ObservableCollection<Character> tempCharacterList;
        private void LoadObject()
        {
            CharacterList = new ObservableCollection<Character>();

            string jsonString;
            jsonString = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Characters.txt");
            tempCharacterList = JsonSerializer.Deserialize<ObservableCollection<Character>>(jsonString);

            foreach (Character oldCharacter in tempCharacterList)
            {
                Character newCharacter = new Character(oldCharacter);
                CharacterList.Add(newCharacter);
            }
        }
        #endregion

        #region Save Region
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(
                        param => this.SaveObject(),
                        param => this.CanSave()
                        );
                }
                return _SaveCommand;
            }
        }

        public string Name => "Main Menu";

        private bool CanSave()
        {
            return CharacterList.Count>0;
        }
        private void SaveObject()
        {
            string jsonString;
            jsonString = JsonSerializer.Serialize(CharacterList);
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Characters.txt", jsonString);
        }

        public void LoadAvailableJobs()
        {
        }
        #endregion


    }
}
