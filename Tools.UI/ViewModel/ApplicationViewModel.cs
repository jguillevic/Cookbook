using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools.UI.DataProvider;

namespace Tools.UI.ViewModel
{
    public abstract class ApplicationViewModel : ViewModelBase, IViewModelSetter
    {
        private PageViewModel _currentViewModel;
        public PageViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            private set
            {
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged("CurrentViewModel");
                }
            }
        }

        protected DataProviderManager DataProviderManager { get; private set; }

        public ApplicationViewModel()
        {
            DataProviderManager = new DataProviderManager();
        }

        public virtual void Initialize() { }

        public async Task PopulateAsync()
        {
            await DataProviderManager.PopulateAsync();
        }

        public async Task SetCurrentViewModelAsync(PageViewModel pageViewModel)
        {
            pageViewModel.SetViewModelSetter(this);
            pageViewModel.SetDataProviderManager(DataProviderManager);

            pageViewModel.Initialize();
            await pageViewModel.PopulateAsync();

            if (CurrentViewModel != null)
                CurrentViewModel.Dispose();

            CurrentViewModel = pageViewModel;
        }
    }
}
