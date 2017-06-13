using System.Globalization;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Select
{
    /// <summary>
    /// Permet la création de la clause lié à un champ requêté dans une table.
    /// </summary>
	public class QueriedFieldClause
    {
        /// <summary>
        /// Nom de la table.
        /// Null si pas nécessaire.
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Nom de la colonne.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Alias de la colonne.
        /// Null si pas nécessaire.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Le champ requêté doit-il être compté ?
        /// </summary>
        public bool IsCount { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="alias">Alias de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public QueriedFieldClause(string tableName, string columnName, string alias, bool isCount)
        {
            TableName = tableName;
            ColumnName = columnName;
            Alias = alias;
            IsCount = isCount;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            if (IsCount)
            {
                sb.Append("COUNT(");
            }

            if (TableName != null)
            {
                sb.Append(TableName);
                sb.Append(".");
            }

            sb.Append(string.Format(CultureInfo.CurrentCulture, "[{0}]", ColumnName));

            if (IsCount)
            {
                sb.Append(")");
            }

                if (Alias != null)
            {
                sb.Append(" AS ");
                sb.Append(Alias);
            }
        }
    }
}