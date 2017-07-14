using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tools.UI.DataProvider
{
    public sealed class DataProviderManager
    {
        private Dictionary<int, IDataProvider> _dataProviders;

        public DataProviderManager()
        {
            _dataProviders = new Dictionary<int, IDataProvider>();
        }

        public async Task PopulateAsync()
        {
            List<Task> results = new List<Task>();

            foreach (var dp in _dataProviders.Values)
                results.Add(dp.PopulateAsync());

            await Task.WhenAll(results);
        }

        public void AddDataProvider(int key, IDataProvider dataProvider)
        {
            _dataProviders.Add(key, dataProvider);
        }

        public IDataProvider GetDataProvider(int key)
        {
            return _dataProviders[key];
        }
    }
}
