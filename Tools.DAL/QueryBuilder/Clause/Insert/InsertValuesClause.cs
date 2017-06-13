using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Tools.DAL.Database;

namespace Tools.DAL.QueryBuilder.Clause.Insert
{
    /// <summary>
    /// Permet la création de la clause lié à des valeurs ajoutées dans une table.
    /// </summary>
    public class InsertValuesClause
    {
        private StringBuilder _sb;

        /// <summary>
        /// Nom des paramètres.
        /// </summary>
        private List<string> _parameterNames;

        /// <summary>
        /// Valeurs.
        /// </summary>
        private List<object> _values;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public InsertValuesClause()
        {
            _sb = new StringBuilder();
            _parameterNames = new List<string>();
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="values">Valeurs.</param>
        public InsertValuesClause(IEnumerable<object> values) : this()
        {
            _values = new List<object>(values);
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre les paramètres.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        /// <param name="baseParameterNames">Nom des paramètres.</param>
        /// <param name="index">Index des paramètres.</param>
        public void AppendQuery(StringBuilder sb, IList<string> baseParameterNames, int index)
        {
            sb.AppendLine();

            for (int i = 0; i < baseParameterNames.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("(");
                }
                else
                {
                    sb.Append(", ");
                }

                _parameterNames.Add(string.Format(CultureInfo.CurrentCulture, "{0}{1}", baseParameterNames[i], index));
                sb.Append(ParameterHelper.CreateParameterName(_parameterNames[i]));

                if (i == baseParameterNames.Count - 1)
                {
                    sb.Append(")");
                }
            }
        }

        /// <summary>
        /// Ajoute les paramètres à la commande <paramref name="command"/>.
        /// Les valeurs associées sont <see cref="Values"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        public void AddParameters(DbCommand command)
        {
            for (int i = 0; i < _parameterNames.Count; i++)
            {
                command.AddParameter(_parameterNames[i], _values[i]);
            }
        }
    }
}
