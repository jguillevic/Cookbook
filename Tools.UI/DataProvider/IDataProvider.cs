using System.Threading.Tasks;

namespace Tools.UI.DataProvider
{
    public interface IDataProvider
    {
        Task PopulateAsync();
    }
}
