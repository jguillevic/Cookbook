using System;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.OrderBy
{
    using Enum;
    using System.Globalization;

    /// <summary>
    /// Permet la construction d'un élément de la clause ORDER BY.
    /// </summary>
    public class OrderByElementClause
    {
        /// <summary>
        /// Nom du champ.
        /// </summary>
        public string ColumnName;

        /// <summary>
        /// Opérateur de tri.
        /// </summary>
        public Sorting Sorting;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public OrderByElementClause() : this(null, Sorting.Ascending) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="columnName">Nom du champ.</param>
        /// <param name="sorting">Opérateur de tri.</param>
        public OrderByElementClause(string columnName, Sorting sorting)
        {
            ColumnName = columnName;
            Sorting = sorting;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.AppendFormat(CultureInfo.CurrentCulture, "[{0}]",  ColumnName);

            QueryBuilderEnumHelper.AppendSorting(Sorting, sb);
        }
    }
}