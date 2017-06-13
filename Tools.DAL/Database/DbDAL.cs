namespace Tools.DAL.Database
{
    public class DbDAL<TDbConnectionProvider>
        where TDbConnectionProvider : IDbConnectionProvider, new()
    {
        protected TDbConnectionProvider DefaultConnectProvider { get; set; }

        protected DbDAL()
        {
            DefaultConnectProvider = new TDbConnectionProvider();
        }
    }
}
