using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Tools.DAL.Database;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Ancêtre des constructeurs des requêtes SELECT.
    /// </summary>
    public abstract class ReadQueryBuilder : QueryBuilder
    {
        /// <summary>
        /// Lit les données.
        /// </summary>
        /// <typeparam name="TEntity">Type d'éléments à retourner.</typeparam>
        /// <typeparam name="TContainer">Type de conteneur.</typeparam>
        /// <param name="connectionProvider">Objet gérant la connexion à la base de données.</param>
        /// <param name="selector">Fonction contenant la logique de transformation d'un <see cref="IDataRecord"/> 
        /// en <typeparamref name="TEntity"/></param>
        /// <returns>Ensemble de <typeparamref name="TEntity"/>.</returns>
        public TContainer Read<TEntity, TContainer>(IDbConnectionProvider connectionProvider, Func<IDataRecord, TEntity> selector)
            where TContainer : IEnumerable<TEntity>, ICollection<TEntity>, new()
        {
            TContainer readEntities = default(TContainer);

            using (TransactionScope scope = TransactionScopeHelper.GetTransactionScope())
            {
                using (DbConnection connection = connectionProvider.GetOpenedDbConnection())
                {
                    using (DbCommand command = BuildCommand(connection))
                    {
                        using (DbDataReader reader = command.ExecuteReader())
                        {
                            readEntities = new TContainer();

                            while (reader.Read())
                            {
                                readEntities.Add(selector(reader));
                            }
                        }
                    }
                }

                scope.Complete();
            }

            return readEntities;
        }
    }
}
