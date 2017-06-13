using System.Data.Common;

namespace Tools.DAL.Database
{
    public static class DbCommandHelper
    {
        public static DbCommand GetCommand(DbConnection connect)
        {
            var command = connect.CreateCommand();
            command.Connection = connect;
            command.CommandTimeout = 15;
            return command;
        }
    }
}