namespace Tools.UI.ViewModel
{
    public class ApplicationViewModel : ViewModelBase, IViewModelSetter
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

        public void SetCurrentViewModel(PageViewModel pageViewModel)
        {
            pageViewModel.SetViewModelSetter(this);
            pageViewModel.Initialize();
            pageViewModel.Populate();

            CurrentViewModel = pageViewModel;
        }
    }
}
