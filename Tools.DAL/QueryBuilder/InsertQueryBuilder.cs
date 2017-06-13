using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Tools.DAL.QueryBuilder.Clause.Comment;
using Tools.DAL.QueryBuilder.Clause.EndQuery;
using Tools.DAL.QueryBuilder.Clause.Insert;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Constructeur de requêtes INSERT.
    /// </summary>
    public class InsertQueryBuilder : WriteQueryBuilder
    {
        /// <summary>
        /// Constructeur du commentaire en en-tête de requête.
        /// </summary>
        private CommentClause CommentClause { get; set; }

        /// <summary>
        /// Constructeur de la clause INSERT.
        /// </summary>
        private InsertClause InsertClause { get; set; }

        /// <summary>
        /// Constructeur de la fin de requête.
        /// </summary>
        private EndQueryClause EndQueryClause { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public InsertQueryBuilder()
        {
            CommentClause = new CommentClause();
            InsertClause = new InsertClause();
            EndQueryClause = new EndQueryClause();
        }

        /// <summary>
        /// Défini le contenu du commentaire qui sera ajouté en en-tête de requête.
        /// </summary>
        /// <param name="comment">Commentaire.</param>
        public void SetComment(string comment)
        {
            CommentClause.SetComment(comment);
        }

        /// <summary>
        /// Défini le nom de la table sur laquelle porte l'INSERT.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        public void SetTableName(string tableName)
        {
            InsertClause.SetTableName(tableName);
        }

        /// <summary>
        /// Ajoute le champ <paramref name="columnNames"/> dans la liste des champs à affecter.
        /// </summary>
        /// <param name="columnName">Nom du champ.</param>
        public void AddInsertFields(IEnumerable<string> columnNames)
        {
            InsertClause.AddInsertFields(columnNames);
        }

        /// <summary>
        /// Ajoute le champ <paramref name="values"/> dans la liste des valeurs.
        /// </summary>
        /// <param name="values">Valeurs.</param>
        public void AddInsertValues(IEnumerable<object> values)
        {
            InsertClause.AddInsertValues(values);
        }

        /// <summary>
        /// Ajoute le champ <paramref name="values"/> dans la liste des valeurs.
        /// </summary>
        /// <param name="values">Valeurs.</param>
        public void AddInsertValues(IEnumerable<IEnumerable<object>> values)
        {
            InsertClause.AddInsertValues(values);
        }

        /// <summary>
        /// Construit la requête.
        /// </summary>
        /// <returns>Requête.</returns>
        protected override StringBuilder BuildQuery()
        {
            StringBuilder sb = new StringBuilder();

            // Construction du commentaire.
            CommentClause.AppendQuery(sb);

            // Construction de la clause INSERT INTO.
            InsertClause.AppendQuery(sb);

            // Construction de la fin de la requête.
            EndQueryClause.AppendQuery(sb);

            return sb;
        }

        /// <summary>
        /// Ajoute les paramètres à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        protected override void AddParameters(DbCommand command)
        {
            // Ajout des paramètres.
            InsertClause.AddParameters(command);
        }
    }
}
