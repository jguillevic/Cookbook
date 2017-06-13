using Tools.DAL.QueryBuilder.Clause.Comment;
using Tools.DAL.QueryBuilder.Clause.EndQuery;
using Tools.DAL.QueryBuilder.Clause.Update;
using Tools.DAL.QueryBuilder.Clause.Where;
using System.Data.Common;
using System.Text;
using Tools.DAL.QueryBuilder.Clause.From;
using Tools.DAL.QueryBuilder.Enum;
using Tools.DAL.QueryBuilder.Clause;
using System.Collections.Generic;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Constructeur des requêtes UPDATE.
    /// </summary>
    public class UpdateQueryBuilder : WriteQueryBuilder
    {
        private const string TempAliasName = "Temp";

        /// <summary>
        /// Constructeur du commentaire en en-tête de requête.
        /// </summary>
        private CommentClause CommentClause { get; set; }

        /// <summary>
        /// Constructeur de la clause UPDATE.
        /// </summary>
        private UpdateClause UpdateClause { get; set; }

        /// <summary>
        /// Constructeur de la clause SET.
        /// </summary>
        private SetClause SetClause { get; set; }

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
        public UpdateQueryBuilder()
        {
            CommentClause = new CommentClause();
            UpdateClause = new UpdateClause();
            SetClause = new SetClause();
            FromClause = new FromClause();
            WhereClause = new WhereClause();
            EndQueryClause = new EndQueryClause();
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

            // Construction de la clause UPDATE.
            UpdateClause.AppendQuery(sb);

            // Construction de la clause SET.
            SetClause.AppendQuery(sb);

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
            FromClause.AddParameters(command);
            WhereClause.AddParameters(command);
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
        /// Défini le nom de la table sur laquelle porte le UPDATE.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        public void SetTableName(string tableName)
        {
            var aliasName = ClauseHelper.GetAliasFromTableName(tableName);

            UpdateClause.SetTableName(aliasName);
            FromClause.AddFrom(tableName, aliasName);
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une nouvelle table.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine.</param>
        /// <param name="fromColumnName">Nom de la colonne d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toColumnName">Nom de la colonne de destination.</param>
        public void AddJoin(JoinType join, string fromTableName, string fromColumnName, Comparison comparison, string toTableName, string toColumnName)
        {
            FromClause.AddJoin(join, fromTableName, new List<string> { fromColumnName }, comparison, toTableName, new List<string> { toColumnName });
        }

        /// <summary>
        /// Ajoute une jointure JOIN sur une nouvelle table.
        /// </summary>
        /// <param name="join">Type de jointure.</param>
        /// <param name="fromTableName">Nom de la table d'origine.</param>
        /// <param name="fromColumnName">Nom de la colonne d'origine.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="toTableName">Nom de la table de destination.</param>
        /// <param name="toAliasName">Nom de l'alias de la table de destination.</param>
        /// <param name="toColumnName">Nom de la colonne de destination.</param>
        public void AddJoin(JoinType join, string fromTableName, string fromColumnName, Comparison comparison, string toTableName, string toAliasName, string toColumnName)
        {
            FromClause.AddJoin(join, fromTableName, new List<string> { fromColumnName }, comparison, toTableName, toAliasName, new List<string> { toColumnName });
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
        /// Ajoute les champs dont les valeurs sont à mettre à jour.
        /// </summary>
        /// <param name="idColumnNames">Noms des colonnes de l'identifiant.</param>
        /// <param name="columnNames">Nom des colonnes.</param>
        /// <param name="values">Valeurs.</param>
        public void AddSettedFields(IEnumerable<string> idColumnNames, IEnumerable<string> columnNames, List<List<object>> values)
        {
            var toAliasName = ClauseHelper.GetAliasFromTableName(TempAliasName);

            SetClause.AddSettedFields(columnNames, FromClause.AliasName, toAliasName);

            FromClause.AddJoin(JoinType.InnerJoin, FromClause.AliasName, idColumnNames, toAliasName, columnNames, values);
        }
    }
}
