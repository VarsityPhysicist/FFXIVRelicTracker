using FFXIVRelicTracker._05_Skysteel._00_Summary;
using FFXIVRelicTracker.Models;
using FFXIVRelicTracker.Models.Helpers;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FFXIVRelicTracker._05_Skysteel.Main
{
    public class SkysteelViewModel : ObservableObject, IPageViewModel
    {
        #region Fields
        private IEventAggregator iEventAggregator;
        private Character selectedCharacter;

        #endregion

        #region Constructor
        public SkysteelViewModel(IEventAggregator iEventAggregator)
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

			PageViewModels.Add(new SkysteelSummaryViewModel(Event.EventInstance.EventAggregator));

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

		#region Properties
		public string Name => "Skysteel Tools";
        public Character SelectedCharacter
        {
            get { return selectedCharacter; }
            set
            {
                selectedCharacter = value;
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

        #endregion

        #region Methods
        public void LoadAvailableJobs()
        {
        }
        #endregion

        #region Commands
        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
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

                    OnPropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }
        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            viewModel.LoadAvailableJobs();

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public ICommand Unsubscribe { get; set; }

        public ICommand Subscribe { get; set; }

        #endregion

    }
}
