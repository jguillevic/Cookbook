namespace Tools.UI.ViewModel
{
    public abstract class PageViewModel : ViewModelBase
    {
        protected IViewModelSetter Setter { get; private set; }

        public void SetViewModelSetter(IViewModelSetter setter)
        {
            Setter = setter;
        }

        public virtual void Initialize() { }
        public virtual void Populate() { }
    }
}
