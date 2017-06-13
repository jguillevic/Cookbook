using System.Data.Common;

namespace Tools.DAL.Database
{
    public interface IDbConnectionProvider
    {
        DbConnection GetOpenedDbConnection();
    }
}
