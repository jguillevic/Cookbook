using Tools.DAL.QueryBuilder.Clause.Comment;
using Tools.DAL.QueryBuilder.Clause.Delete;
using Tools.DAL.QueryBuilder.Clause.EndQuery;
using Tools.DAL.QueryBuilder.Clause.From;
using Tools.DAL.QueryBuilder.Clause.Where;
using System.Data.Common;
using System.Text;
using Tools.DAL.QueryBuilder.Enum;
using System.Collections.Generic;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Constructeur des requêtes DELETE.
    /// </summary>
    public class DeleteQueryBuilder : WriteQueryBuilder
    {
        /// <summary>
        /// Constructeur du commentaire en en-tête de requête.
        /// </summary>
        private CommentClause CommentClause { get; set; }

        /// <summary>
        /// Constructeur de la clause DELETE.
        /// </summary>
        private DeleteClause DeleteClause { get; set; }

        /// <summary>
        /// Constructeur de la clause FROM.
        /// </summary>
        private FromClause FromClause { get; set; }

        /// <summary>
        /// Constructeur de la clause WHERE.
        /// </summary>
        private WhereClause WhereClause { get; set; }

        /// <summary>
        /// Constructeur de la fin de requête.
        /// </summary>
        private EndQueryClause EndQueryClause { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public DeleteQueryBuilder()
        {
            CommentClause = new CommentClause();
            DeleteClause = new DeleteClause();
            FromClause = new FromClause();
            WhereClause = new WhereClause();
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
        /// Ajoute la clause FROM.
        /// Nécessaire d'appeler cette méthode avant d'ajouter une autre jointure.
        /// </summary>
        /// <param name="tableName">Nom de la table sur laquelle FROM s'applique.</param>
        public void AddFrom(string tableName)
        {
            FromClause.AddFrom(tableName);
        }

        /// <summary>
        /// Ajoute la clause FROM.
        /// Nécessaire d'appeler cette méthode avant d'ajouter une autre jointure.
        /// </summary>
        /// <param name="tableName">Nom de la table sur laquelle FROM s'applique.</param>
        /// <param name="aliasName">Nom de l'alias de la table.</param>
        public void AddFrom(string tableName, string aliasName)
        {
            FromClause.AddFrom(tableName, aliasName);
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
            FromClause.AddJoin(join, fromTableName, fromColumnNames, comparison, toTableName, toColumnNames);
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
            FromClause.AddJoin(join, fromTableName, fromColumnNames, comparison, toTableName, toAliasName, toColumnNames);
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
            FromClause.AddJoin(join, fromTableName, fromColumnNames, toAliasName, columnNames, values);
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
            WhereClause.AddWhere(columnName, comparison, value);
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
            WhereClause.AddCondition(logicOperator, columnName, comparison, value);
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

            // Construction de la clause DELETE.
            DeleteClause.AppendQuery(sb);

            // Construction de la clause FROM.
            FromClause.AppendQuery(sb);

            // Construction de la clause WHERE.
            WhereClause.AppendQuery(sb);

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
            WhereClause.AddParameters(command);
        }
    }
}
