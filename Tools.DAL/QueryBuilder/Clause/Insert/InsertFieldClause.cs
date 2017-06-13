using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Insert
{
    /// <summary>
    /// Permet la création de la clause lié à un champ ajouté dans une table.
    /// </summary>
    public class InsertFieldClause
    {
        private StringBuilder _sb;

        /// <summary>
        /// Noms des colonnes.
        /// </summary>
        public List<string> ColumnNames { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public InsertFieldClause()
        {
            _sb = new StringBuilder();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="columnNames">Noms des colonnes.</param>
        public InsertFieldClause(IEnumerable<string> columnNames) : this()
        {
            ColumnNames = new List<string>(columnNames);
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.AppendLine();

            for (int i = 0; i < ColumnNames.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("(");
                }
                else
                {
                    sb.Append(", ");
                }

                sb.Append(string.Format(CultureInfo.CurrentCulture, "[{0}]", ColumnNames[i]));

                if (i == ColumnNames.Count - 1)
                {
                    sb.Append(")");
                }
            }
        }
    }
}
