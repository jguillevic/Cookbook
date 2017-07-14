using System.Threading.Tasks;
using Tools.UI.DataProvider;

namespace Tools.UI.ViewModel
{
    public abstract class PageViewModel : ViewModelBase
    {
        protected IViewModelSetter Setter { get; private set; }
        protected DataProviderManager DataProviderManager { get; private set; }

        public PageViewModel() : base() { }

        public void SetViewModelSetter(IViewModelSetter setter)
        {
            Setter = setter;
        }

        public void SetDataProviderManager(DataProviderManager dpManager)
        {
            DataProviderManager = dpManager;
        }

        public virtual void Initialize() { }
        public virtual Task PopulateAsync()
        {
            return Task.FromResult(default(object));
        }
    }
}
