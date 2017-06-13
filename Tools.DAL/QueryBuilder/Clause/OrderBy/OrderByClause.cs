using System.Collections.Generic;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.OrderBy
{
    using Enum;

    /// <summary>
    /// Permet la construction d'une clause ORDER BY.
    /// </summary>
    public class OrderByClause
    {
        /// <summary>
        /// Collection de constructeurs d'éléments de la clause ORDER BY.
        /// </summary>
        private List<OrderByElementClause> _orderByElementClauses;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public OrderByClause()
        {
            _orderByElementClauses = new List<OrderByElementClause>();
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            for (int i = 0; i < _orderByElementClauses.Count; i++)
            {
                if (i == 0)
                {
                    sb.AppendLine();

                    sb.Append("ORDER BY ");
                }
                else
                {
                    sb.Append(", ");
                }

                _orderByElementClauses[i].AppendQuery(sb);
            }
        }

        /// <summary>
        /// Ajoute un champ dans la clause ORDER BY.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="sortOrder">Opérateur de tri.</param>
        public void AddOrderBy(string columnName, Sorting sortOrder)
        {
            _orderByElementClauses.Add(new OrderByElementClause(columnName, sortOrder));
        }
    }
}