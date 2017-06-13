using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Comment
{
    /// <summary>
    /// Permet la construction d'un commentaire.
    /// </summary>
    public class CommentClause
    {
        /// <summary>
        /// Commentaire de la requête.
        /// </summary>
        private string _comment;

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            // Si le commentaire est reseigné.
            if (!string.IsNullOrWhiteSpace(_comment))
            {
                sb.Append("-- ");
                sb.Append(_comment);
                sb.AppendLine();
            }
        }

        /// <summary>
        /// Défini le contenu du commentaire qui sera ajouté en en-tête de requête.
        /// </summary>
        /// <param name="comment">Commentaire.</param>
        public void SetComment(string comment)
        {
            _comment = comment;
        }
    }
}
