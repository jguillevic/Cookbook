namespace Tools.UI.ViewData
{
    public class EntityViewData<T> : ViewDataBase
        where T : new()
    {
        public T Entity { get; set; }

        public EntityViewData()
        {
            Entity = new T();
        }
    }
}
