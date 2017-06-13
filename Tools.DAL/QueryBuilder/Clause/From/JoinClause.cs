using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Data.Common;

namespace Tools.DAL.QueryBuilder.Clause.From
{
    using Database;
    using Enum;

    /// <summary>
    /// Permet la construction d'une jointure JOIN.
    /// </summary>
    public class JoinClause
    {
        /// <summary>
        /// Type de jointure.
        /// </summary>
        private JoinType _joinType;

        /// <summary>
        /// Nom de la table d'origine ou de son alias.
        /// </summary>
        private string _fromTableOrAliasName;

        /// <summary>
        /// Noms des colonnes d'origine.
        /// </summary>
        private List<string> _fromColumnNames;

        /// <summary>
        /// Opérateur de comparaison.
        /// </summary>
        private Comparison _comparison;

        /// <summary>
        /// Nom de la table de destination.
        /// </summary>
        private string _toTableName;

        /// <summary>
        /// Nom de l'alias de la table de destination.
        /// </summary>
        private string _toAliasName;

        /// <summary>
        /// Noms des colonnes de destination.
        /// </summary>
        private List<string> _toColumnNames;

        /// <summary>
        /// Nom des colonnes.
        /// </summary>
        private List<string> _columnNames;

        /// <summary>
        /// Nom des paramètres.
        /// </summary>
        private List<List<string>> _parameterNames;

        /// <summary>
        /// Valeurs.
        /// </summary>
        private List<List<object>> _values;

        /// <summary>
        /// Nom de la table de destination.
        /// </summary>
        public string ToTableName
        {
            get { return _toTableName; }
        }

        /// <summary>
        /// Nom de l'alias de la table de destination.
        /// </summary>
        public string ToAliasName
        {
            get { return _toAliasName; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableOrAliasName">Nom de la table d'origine ou de son alias.</param>
        /// <param name="fromColumnName">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        public JoinClause(JoinType join, string fromTableOrAliasName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, IEnumerable<string> toColumnNames)
            : this(join, fromTableOrAliasName, fromColumnNames, comparison, toTableName, null, toColumnNames) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableOrAliasName">Nom de la table d'origine ou de son alias.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        public JoinClause(JoinType join, string fromTableOrAliasName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, string toAliasName, IEnumerable<string> toColumnNames)
            : this(join, fromTableOrAliasName, fromColumnNames, comparison, toTableName, toAliasName, toColumnNames, null, null) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableOrAliasName">Nom de la table d'origine ou son alias.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="columnNames">Nom des colonnes.</param>
        /// <param name="values">Valeurs.</param>
        public JoinClause(JoinType join, string fromTableOrAliasName, IEnumerable<string> fromColumnNames, string toAliasName, IEnumerable<string> columnNames, IEnumerable<List<object>> values)
            : this(join, fromTableOrAliasName, fromColumnNames, Comparison.Equals, null, toAliasName, null, columnNames, values) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableOrAliasName">Nom de la table d'origine ou de son alias.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        /// <param name="columnNames">Nom des colonnes.</param>
        /// <param name="values">Valeurs.</param>
        public JoinClause(JoinType join, string fromTableOrAliasName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, string toAliasName, IEnumerable<string> toColumnNames, IEnumerable<string> columnNames, IEnumerable<List<object>> values)
        {
            _joinType = join;
            _fromTableOrAliasName = fromTableOrAliasName;
            _comparison = comparison;  
            _toTableName = toTableName;
            _toAliasName = toAliasName;

            if (fromColumnNames != null)
                _fromColumnNames = new List<string>(fromColumnNames);
            else
                _fromColumnNames = null;

            if (toColumnNames != null)
                _toColumnNames = new List<string>(toColumnNames);
            else
                _toColumnNames = null;

            if (columnNames != null)
                _columnNames = new List<string>(columnNames);
            else
                _columnNames = null;

            if (_values != null)
                _values = new List<List<object>>(values);
            else
                _values = null;

            _parameterNames = new List<List<string>>();
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        internal void AppendQuery(StringBuilder sb)
        {
            sb.AppendLine();

            QueryBuilderEnumHelper.AppendJointType(_joinType, sb);

            if (_columnNames == null)
            {
                sb.Append(_toTableName);

                if (string.IsNullOrWhiteSpace(_toAliasName))
                {
                    sb.Append(" ");
                    sb.Append(_toAliasName);
                }
            }
            else
            {
                sb.AppendLine("(");
                AppendQueryValues(sb);
                sb.Append(") ");
                sb.Append(_toAliasName);
                sb.Append(" ");
                sb.Append("(");
                AppendQueryTempTable(sb);
                sb.Append(")");
            }

            sb.Append(" ON ");
            AppendQueryOnConditions(sb);
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction 
        /// des valeurs.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        private void AppendQueryValues(StringBuilder sb)
        {
            sb.AppendLine("VALUES");

            for (int i = 0; i < _values.Count; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine(", ");
                }

                sb.Append("(");

                for (int j = 0; j < _values[i].Count; j++)
                {
                    if (j > 0)
                    {
                        sb.Append(", ");
                    }

                    _parameterNames.Add(new List<string>());
                    _parameterNames[i].Add(string.Format(CultureInfo.CurrentCulture, "{0}{1}", ParameterHelper.CreateParameterName(_columnNames[j]), i));

                    sb.Append(_parameterNames[i][j]);
                }

                sb.Append(")");
            }
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction 
        /// de la table temporaire.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        private void AppendQueryTempTable(StringBuilder sb)
        {
            for (int i = 0; i < _columnNames.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(_columnNames[i]);
            }
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction 
        /// des conditions de jointure.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        private void AppendQueryOnConditions(StringBuilder sb)
        {
            for (int i = 0; i < _fromColumnNames.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" AND ");
                }

                sb.Append(
                    string.Format(
                        CultureInfo.CurrentCulture
                        , " {0}.{1} {2} {3}.{4}"
                        , _fromTableOrAliasName
                        , _fromColumnNames[i]
                        , QueryBuilderEnumHelper.GetComparison(_comparison)
                        , _toAliasName ?? _toTableName
                        , _toColumnNames[i]));
            }
        }

        /// <summary>
        /// Ajoute les paramètre à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        internal void AddParameters(DbCommand command)
        {
            for (int i = 0; i < _parameterNames.Count; i++)
            {
                for (int j = 0; j < _parameterNames[i].Count; j++)
                {
                    command.AddParameter(_parameterNames[i][j], _values[i][j]);
                }
            }
        }
    }
}