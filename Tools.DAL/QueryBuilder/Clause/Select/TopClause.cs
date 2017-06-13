using Tools.DAL.QueryBuilder.Enum;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Select
{
    /// <summary>
    /// Permet la construction d'une clause TOP.
    /// </summary>
    public class TopClause
    {
        /// <summary>
        /// Quantité.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unité.
        /// </summary>
        public TopUnit TopUnit { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public TopClause()
        {
            Quantity = 0;
            TopUnit = TopUnit.Records;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append("TOP ");
            sb.Append(Quantity);

            QueryBuilderEnumHelper.AppendTopUnit(TopUnit, sb);
        }

        /// <summary>
        /// Défini les paramètres de la clause TOP.
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="topUnit"></param>
        public void SetTopClause(int quantity, TopUnit topUnit)
        {
            Quantity = quantity;
            TopUnit = topUnit;
        }
    }
}