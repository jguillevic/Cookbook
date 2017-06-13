using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Update
{
    /// <summary>
    /// Permet la construction d'une clause SET.
    /// </summary>
    public class SetClause
    {
        private string _fromTableOrAliasName;

        private string _toTableOrAliasName;

        /// <summary>
        /// Noms des colonnes.
        /// </summary>
        private List<string> _columnNames;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public SetClause()
        {
            _columnNames = new List<string>();
        }

        /// <summary>
        /// Ajoute les champs dont les valeurs sont à mettre à jour.
        /// </summary>
        /// <param name="columnNames">Nom des colonnes.</param>
        public void AddSettedFields(IEnumerable<string> columnNames, string fromTableOrAliasName, string toTableOrAliasName)
        {
            _columnNames.AddRange(columnNames);
            _fromTableOrAliasName = fromTableOrAliasName;
            _toTableOrAliasName = toTableOrAliasName;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.AppendLine();

            sb.Append("SET ");

            for (int i = 0; i < _columnNames.Count; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine();
                    sb.Append(", ");
                }

                sb.Append(string.Format(CultureInfo.CurrentCulture, "{0}.[{1}] = {2}.[{3}]", _fromTableOrAliasName, _columnNames[i], _toTableOrAliasName, _columnNames[i]));
            }
        }
    }
}
