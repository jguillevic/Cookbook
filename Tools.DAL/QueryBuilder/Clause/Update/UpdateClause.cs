using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Update
{
    /// <summary>
    /// Permet la construction d'une clause UPDATE.
    /// </summary>
    public class UpdateClause
    {
        /// <summary>
        /// Nom de la table sur laquelle UPDATE s'applique.
        /// </summary>
        private string _tableName;

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append("UPDATE ");

            sb.Append(_tableName);
        }

        /// <summary>
        /// Défini le nom de la table sur laquelle porte le UPDATE.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        public void SetTableName(string tableName)
        {
            _tableName = tableName;
        }
    }
}
