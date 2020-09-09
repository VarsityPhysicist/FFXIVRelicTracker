using FFXIVRelicTracker.ARR.Animus;
using FFXIVRelicTracker.ARR.Atma;
using FFXIVRelicTracker.ARR.Braves;
using FFXIVRelicTracker.ARR.Nexus;
using FFXIVRelicTracker.ARR.Novus;
using FFXIVRelicTracker.ARR.Relic;
using FFXIVRelicTracker.ARR.Summary;
using FFXIVRelicTracker.ARR.Zeta;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker.ViewModels
{
    public class ArrViewModel : ObservableObject, IPageViewModel
    {

		#region Constructors

		public ArrViewModel(IEventAggregator iEventAggregator)
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

            PageViewModels.Add(new SummaryViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new RelicViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new AtmaViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new AnimusViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new NovusViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new NexusViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new BravesViewModel(Event.EventInstance.EventAggregator));
            PageViewModels.Add(new ZetaViewModel(Event.EventInstance.EventAggregator));

            CurrentPageViewModel = PageViewModels[0];

            this.Subscribe = new DelegateCommand(
			() =>
			{
				subscriptionToken =
					this
						.iEventAggregator
						.GetEvent<PubSubEvent<Character>>()
						.Subscribe((details) =>
						{
							this.SelectedCharacter = details;
						});
			});

			this.Unsubscribe = new DelegateCommand(
				() => {

					this
						.iEventAggregator
						.GetEvent<PubSubEvent<Character>>()
						.Unsubscribe(subscriptionToken);
				});


		}

        #endregion

        #region PageView Region

        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion
        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
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

            viewModel.LoadAvailableJobs();

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public void LoadAvailableJobs()
        {
        }

        #endregion
        #endregion

        public ICommand Unsubscribe { get; set; }

		public ICommand Subscribe { get; set; }

		public string Name { get { return "ARR View"; } }

        private Character selectedCharacter;
        private IEventAggregator iEventAggregator;

        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
            }
        }

    }
}
