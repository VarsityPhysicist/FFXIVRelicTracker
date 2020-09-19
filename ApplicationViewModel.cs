using FFXIVRelicTracker._03_HW.Main;
using FFXIVRelicTracker._05_ShB.Main;
using FFXIVRelicTracker._05_Skysteel.Main;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using FFXIVRelicTracker.Views;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FFXIVRelicTracker.ViewModels
{
    class ApplicationViewModel : ObservableObject

    {
        #region Fields

        private ICommand _changePageCommand;
        private ICommand _MainMenuCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion
        #region Constructors
        public ApplicationViewModel(IEventAggregator iEventAggregator)
        {

            this.iEventAggregator = iEventAggregator;
            SubscriptionToken subscriptionToken =
                                    this
                                        .iEventAggregator
                                        .GetEvent<PubSubEvent<Character>>()
                                        .Subscribe((details) =>
                                        {
                                            this.SelectedCharacter = details;
                                        });

            // Add available pages
            MenuViewModels.Add(new MainMenuViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new ArrViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new HWViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new ShBViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new SkysteelViewModel(Event.EventInstance.EventAggregator));

            // Set starting page
            CurrentPageViewModel = MenuViewModels[0];

        }
        #endregion


        #region Properties / Commands
        private IEventAggregator iEventAggregator;
        private Character SelectedCharacter;
        private List<IPageViewModel> _menuViewModels;

        private void performMainCloseButtonCommand(object Parameter)
        {
            Window objWindow = Parameter as Window;
            objWindow.Close();
        }

        public ICommand MainMenuPageCommand
        {
            get
            {
                if (_MainMenuCommand == null)
                {
                    _MainMenuCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p));
                }

                return _MainMenuCommand;
            }
        }


        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel & SelectedCharacter!=null);
                }

                return _changePageCommand;
            }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public List<IPageViewModel> MenuViewModels
        {
            get
            {
                if (_menuViewModels == null)
                    _menuViewModels = new List<IPageViewModel>();

                return _menuViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion

    }
}
