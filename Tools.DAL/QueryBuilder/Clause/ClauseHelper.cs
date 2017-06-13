using System.Globalization;
using System.Linq;

namespace Tools.DAL.QueryBuilder.Clause
{
    /// <summary>
    /// Contient des méthodes d'aide pour les différentes clauses.
    /// </summary>
    public static class ClauseHelper
    {
        private static int _aliasCount = 0;

        /// <summary>
        /// Retourne un alias à partir du nom de la table <paramref name="tableName"/>.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <returns>Alias généré à partir du nom de la table <paramref name="tableName"/>.</returns>
        public static string GetAliasFromTableName(string tableName)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}{1}", tableName.Split('.').Last(), _aliasCount++);
        }
    }
}
