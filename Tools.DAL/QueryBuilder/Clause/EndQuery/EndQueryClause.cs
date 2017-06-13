using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.EndQuery
{
    /// <summary>
    /// Permet la construction de la fin de requête.
    /// </summary>
    public class EndQueryClause
    {
        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append(";");
        }
    }
}
