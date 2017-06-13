using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Delete
{
    /// <summary>
    /// Permet la construction d'une clause DELETE.
    /// </summary>
    public class DeleteClause
    {
        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append("DELETE");
        }
    }
}
