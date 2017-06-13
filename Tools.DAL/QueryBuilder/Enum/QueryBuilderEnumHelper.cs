using System.Text;

namespace Tools.DAL.QueryBuilder.Enum
{
    /// <summary>
    /// Contient des méthodes d'aide liés aux énumérés utilisés par QueryBuilder.
    /// </summary>
    public static class QueryBuilderEnumHelper
    {
        /// <summary>
        /// Retourne la valeur associée à <paramref name="comparison"/>.
        /// </summary>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <returns>Valeur associée à <paramref name="comparison"/>.</returns>
        public static string GetComparison(Comparison comparison)
        {
            string value = string.Empty;

            switch (comparison)
            {
                case Comparison.Equals:
                    value = " = ";
                    break;
                case Comparison.GreaterOrEquals:
                    value = " >= ";
                    break;
                case Comparison.GreaterThan:
                    value = " > ";
                    break;
                case Comparison.In:
                    value = " IN ";
                    break;
                case Comparison.NotIn:
                    value = " NOT IN ";
                    break;
                case Comparison.LessOrEquals:
                    value = " <= ";
                    break;
                case Comparison.LessThan:
                    value = " < ";
                    break;
                case Comparison.Like:
                    value = " LIKE ";
                    break;
                case Comparison.NotEquals:
                    value = " <> ";
                    break;
                case Comparison.NotLike:
                    value = " NOT LIKE ";
                    break;
                default:
                    break;
            }

            return value;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat la valeur associée à <paramref name="comparison"/>.
        /// </summary>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public static void AppendComparison(Comparison comparison, StringBuilder sb)
        {
            sb.Append(GetComparison(comparison));
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat la valeur associée à <paramref name="joinType"/>.
        /// </summary>
        /// <param name="joinType">Opérateur pour la clause JOIN.</param>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public static void AppendJointType(JoinType joinType, StringBuilder sb)
        {
            switch (joinType)
            {
                case JoinType.InnerJoin:
                    sb.Append("INNER JOIN ");
                    break;
                case JoinType.LeftJoin:
                    sb.Append("LEFT JOIN ");
                    break;
                case JoinType.RightJoin:
                    sb.Append("RIGHT JOIN ");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat la valeur associée à <paramref name="logicOperator"/>.
        /// </summary>
        /// <param name="logicOperator">Opérateur logique.</param>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public static void AppendLogicOperator(LogicOperator logicOperator, StringBuilder sb)
        {
            switch (logicOperator)
            {
                case LogicOperator.And:
                    sb.Append("AND ");
                    break;
                case LogicOperator.Or:
                    sb.Append("OR ");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat la valeur associée à <paramref name="sorting"/>.
        /// </summary>
        /// <param name="sorting">Opérateur de tri.</param>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public static void AppendSorting(Sorting sorting, StringBuilder sb)
        {
            switch (sorting)
            {
                case Sorting.Ascending:
                    sb.Append(" ASC");
                    break;
                case Sorting.Descending:
                    sb.Append(" DESC");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat la valeur associée à <paramref name="topUnit"/>.
        /// </summary>
        /// <param name="topUnit">Unité.</param>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public static void AppendTopUnit(TopUnit topUnit, StringBuilder sb)
        {
            switch (topUnit)
            {
                case TopUnit.Percent:
                    sb.Append(" PERCENT ");
                    break;
                default:
                    break;
            }
        }
    }
}
