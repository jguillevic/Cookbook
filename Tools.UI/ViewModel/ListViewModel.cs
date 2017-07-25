using Tools.UI.Common;
using Tools.UI.ViewData;

namespace Tools.UI.ViewModel
{
    public abstract class ListViewModel<T> : PageViewModel
        where T : ViewDataBase
    {
        public ObservableRangeCollection<T> Items { get; private set; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                }
            }
        }

        public ListViewModel() : base()
        {
            Items = new ObservableRangeCollection<T>();
        }
    }
}
