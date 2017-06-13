using System.Data.Common;
using System.Text;
using Tools.DAL.Database;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Ancêtre des constructeurs des requêtes INSERT, UPDATE, DELETE et SELECT.
    /// </summary>
    public abstract class QueryBuilder
    {
        /// <summary>
        /// Construit la requête.
        /// </summary>
        /// <returns>Requête.</returns>
        protected abstract StringBuilder BuildQuery();

        /// <summary>
        /// Ajoute les paramètres à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        protected abstract void AddParameters(DbCommand command);

        /// <summary>
        /// Construit la commande.
        /// </summary>
        /// <param name="connection">Connexion ouverte.</param>
        /// <returns>Commande.</returns>
        protected DbCommand BuildCommand(DbConnection connection)
        {
            DbCommand command = DbCommandHelper.GetCommand(connection);

            // Construction de la requête.
            StringBuilder query = BuildQuery();

            // Affectation de la requête.
            command.CommandText = query.ToString();

            // Ajout des paramètres.
            AddParameters(command);

            return command;
        }
    }
}
