using Tools.DAL.QueryBuilder.Enum;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Where
{
    /// <summary>
    /// Permet la construction d'une clause WHERE.
    /// </summary>
    public class WhereClause
    {
        /// <summary>
        /// Collection de conditions de la clause WHERE.
        /// </summary>
        private List<SearchConditionClause> _searchConditions;

        /// <summary>
        /// Collection d'opérateurs logiques.
        /// </summary>
        private List<LogicOperator> _logicOperators;

        /// <summary>
        /// La clause WHERE a-t-elle été déclarée ?
        /// </summary>
        private bool _hasWhereClause;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public WhereClause()
        {
            _searchConditions = new List<SearchConditionClause>();
            _logicOperators = new List<LogicOperator>();
            _hasWhereClause = false;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            for (int i = 0; i < _searchConditions.Count; i++)
            {
                // Dans le cas où il s'agit de la 1ère condition, ajout de l'opérateur WHERE.
                if (i == 0)
                {
                    sb.AppendLine();

                    sb.Append("WHERE ");
                }
                // Ajout de l'opérateur logique dans le cas où il ne s'agit pas de la 1ère condition.
                else
                {
                    // Passage à la ligne pour rendre la requête plus lisible.
                    sb.AppendLine();

                    QueryBuilderEnumHelper.AppendLogicOperator(_logicOperators[i - 1], sb);
                }

                // Ajout de la condition.
                _searchConditions[i].AppendQuery(sb);
            }
        }

        /// <summary>
        /// Ajoute la clause WHERE.
        /// Nécessaire d'appeler cette méthode avant d'ajouter une autre condition.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="value">Valeur.</param>
        public void AddWhere(string columnName, Comparison comparison, object value)
        {
            _searchConditions.Add(new SearchConditionClause(columnName, comparison, value));
            _hasWhereClause = true;
        }

        /// <summary>
        /// Ajoute une condition.
        /// </summary>
        /// <exception cref="Exception">
        /// Exception levée si <see cref="AddWhere(string, Comparison, object)"/> n'a pas d'abord été appelée.
        /// </exception>
        /// <param name="logicOperator">Opérateur logique.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="value">Valeur.</param>
        public void AddCondition(LogicOperator logicOperator, string columnName, Comparison comparison, object value)
        {
            if (!_hasWhereClause)
            {
                throw new Exception("Un 1er appel à la méthode AddWhere est nécessaire.");
            }

            AddWhere(columnName, comparison, value);
            _logicOperators.Add(logicOperator);
        }

        /// <summary>
        /// Ajoute les paramètre à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        internal void AddParameters(DbCommand command)
        {
            foreach (var searchCondition in _searchConditions)
            {
                searchCondition.AddParameter(command);
            }
        }
    }
}