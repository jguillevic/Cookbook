using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.From
{
    using Enum;

    /// <summary>
    /// Permet la construction d'une clause FROM.
    /// </summary>
    public class FromClause
    {
        /// <summary>
        /// Collection de constructeurs de jointures.
        /// </summary>
        private List<JoinClause> _joinClauses;

        /// <summary>
        /// La clause FROM a-t-elle été déclarée ?
        /// </summary>
        private bool _hasFromClause;

        /// <summary>
        /// Nom de la table sur laquelle FROM s'applique.
        /// </summary>
        private string _tableName;

        /// <summary>
        /// Nom de l'alias de la table.
        /// </summary>
        private string _aliasName;

        /// <summary>
        /// Nom de la table sur laquelle FROM s'applique.
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
        }

        /// <summary>
        /// Nom de l'alias de la table.
        /// </summary>
        public string AliasName
        {
            get { return _aliasName; }
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public FromClause()
        {
            _joinClauses = new List<JoinClause>();
            _hasFromClause = false;
            _tableName = null;
            _aliasName = null;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        internal void AppendQuery(StringBuilder sb)
        {
            sb.AppendLine();

            sb.Append("FROM ");

            sb.Append(_tableName);

            if (!string.IsNullOrWhiteSpace(_aliasName))
            {
                sb.Append(string.Format(CultureInfo.CurrentCulture, " {0}", _aliasName));
            }

            foreach (var joinClause in _joinClauses)
            {
                joinClause.AppendQuery(sb);
            }
        }

        /// <summary>
        /// Ajoute la clause FROM.
        /// Nécessaire d'appeler cette méthode avant d'ajouter une autre jointure.
        /// </summary>
        /// <param name="tableName">Nom de la table sur laquelle FROM s'applique.</param>
        public void AddFrom(string tableName)
        {
            AddFrom(tableName, null);
        }

        /// <summary>
        /// Ajoute la clause FROM.
        /// Nécessaire d'appeler cette méthode avant d'ajouter une autre jointure.
        /// </summary>
        /// <param name="tableName">Nom de la table sur laquelle FROM s'applique.</param>
        /// <param name="aliasName">Nom de l'alias de la table.</param>
        public void AddFrom(string tableName, string aliasName)
        {
            _tableName = tableName;
            _aliasName = aliasName;
            _hasFromClause = true;
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une nouvelle table.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        public void AddJoin(JoinType join, string fromTableName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, IEnumerable<string> toColumnNames)
        {
            AddJoin(join, fromTableName, fromColumnNames, Comparison.Equals, toTableName, null, toColumnNames, null, null);
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une nouvelle table.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        public void AddJoin(JoinType join, string fromTableName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, string toAliasName, IEnumerable<string> toColumnNames)
        {
            AddJoin(join, fromTableName, fromColumnNames, Comparison.Equals, toTableName, toAliasName, toColumnNames, null, null);
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une table de valeurs.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine ou de son alias.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="columnNames">Nom des colonnes.</param>
        /// <param name="values">Valeurs.</param>
        public void AddJoin(JoinType join, string fromTableName, IEnumerable<string> fromColumnNames, string toAliasName, IEnumerable<string> columnNames, IEnumerable<List<object>> values)
        {
            AddJoin(join, fromTableName, fromColumnNames, Comparison.Equals, null, toAliasName, null, columnNames, values);
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une nouvelle table/table de valeurs.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine.</param>
        /// <param name="fromColumnNames">Noms des colonnes d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="toColumnNames">Noms des colonnes de destination.</param>
        /// <param name="columnNames">Nom des colonnes.</param>
        /// <param name="values">Valeurs.</param>
        private void AddJoin(JoinType join, string fromTableName, IEnumerable<string> fromColumnNames, Comparison comparison, string toTableName, string toAliasName, IEnumerable<string> toColumnNames, IEnumerable<string> columnNames, IEnumerable<List<object>> values)
        {
            if (!_hasFromClause)
            {
                throw new Exception("Un 1er appel à la méthode AddFrom est nécessaire.");
            }

            var fromTableOrAliasName = fromTableName;

            if (_tableName == fromTableName && !string.IsNullOrWhiteSpace(_aliasName))
            {
                fromTableOrAliasName = _aliasName;
            }
            else if (_joinClauses.Count > 0)
            {
                var joinClause = _joinClauses.Find(item => item.ToTableName == fromTableName);

                if (joinClause != null && !string.IsNullOrWhiteSpace(joinClause.ToAliasName))
                {
                    fromTableOrAliasName = joinClause.ToAliasName;
                }
            }

            _joinClauses.Add(new JoinClause(join, fromTableOrAliasName, fromColumnNames, comparison, toTableName, toAliasName, toColumnNames, columnNames, values));
        }

        /// <summary>
        /// Ajoute les paramètre à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        internal void AddParameters(DbCommand command)
        {
            for (int i = 0; i < _joinClauses.Count; i++)
            {
                _joinClauses[i].AddParameters(command);
            }
        }
    }
}