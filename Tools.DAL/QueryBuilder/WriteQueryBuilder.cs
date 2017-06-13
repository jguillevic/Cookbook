using System.Data.Common;
using System.Transactions;
using Tools.DAL.Database;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Ancêtre des constructeurs des requêtes INSERT, UPDATE et DELETE.
    /// </summary>
    public abstract class WriteQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Exécute la requête pour la chaîne de connexion <paramref name="connectionString"/>.
        /// </summary>
        /// <param name="connectionProvider">Objet gérant la connexion à la base de données.</param>
        public int Execute(IDbConnectionProvider connectionProvider)
        {
            int result = -1;

            using (TransactionScope scope = TransactionScopeHelper.GetTransactionScope())
            {
                using (DbConnection connection = connectionProvider.GetOpenedDbConnection())
                {
                    using (DbCommand command = BuildCommand(connection))
                    {
                        result = command.ExecuteNonQuery();
                    }
                }

                scope.Complete();
            }

            return result;
        }
    }
}
