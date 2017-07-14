using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tools.UI.DataProvider
{
    public abstract class DataProviderBase<T> : IDataProvider
    {
        public List<T> Items { get; private set; }

        public DataProviderBase()
        {
            Items = new List<T>();
        }

        public abstract Task PopulateAsync();
    }
}
