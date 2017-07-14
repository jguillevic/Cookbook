using Tools.UI.ViewData;

namespace Tools.UI.ViewModel
{
    public abstract class EntityViewModel<T> : PageViewModel
        where T : ViewDataBase, new()
    {
        public T Item { get; private set; }

        public EntityViewModel() : base()
        {
            Item = new T();
        }
    }
}
