using Tools.DAL.QueryBuilder.Clause.Comment;
using Tools.DAL.QueryBuilder.Clause.EndQuery;
using Tools.DAL.QueryBuilder.Clause.From;
using Tools.DAL.QueryBuilder.Clause.OrderBy;
using Tools.DAL.QueryBuilder.Clause.Select;
using Tools.DAL.QueryBuilder.Clause.Where;
using System.Data.Common;
using System.Text;
using Tools.DAL.QueryBuilder.Enum;
using System.Collections.Generic;

namespace Tools.DAL.QueryBuilder
{
    /// <summary>
    /// Constructeur de requêtes SELECT.
    /// </summary>
    public class SelectQueryBuilder : ReadQueryBuilder
    {
        /// <summary>
        /// Constructeur du commentaire en en-tête de requête.
        /// </summary>
        private CommentClause CommentClause { get; set; }

        /// <summary>
        /// Constructeur de la clause SELECT.
        /// </summary>
        private SelectClause SelectClause { get; set; }

        /// <summary>
        /// Constructeur de la clause FROM.
        /// </summary>
        private FromClause FromClause { get; set; }

        /// <summary>
        /// Constructeur de la clause WHERE.
        /// </summary>
        private WhereClause WhereClause { get; set; }

        /// <summary>
        /// Constructeur de la clause ORDER BY.
        /// </summary>
        private OrderByClause OrderByClause { get; set; }

        /// <summary>
        /// Constructeur de la fin de requête.
        /// </summary>
        private EndQueryClause EndQueryClause { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public SelectQueryBuilder()
        {
            CommentClause = new CommentClause();
            SelectClause = new SelectClause();
            FromClause = new FromClause();
            WhereClause = new WhereClause();
            OrderByClause = new OrderByClause();
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
        /// Ajoute une instruction TOP.
        /// </summary>
        /// <param name="quantity">Quantité.</param>
        /// <param name="unit">Unité.</param>
        public void AddTop(int quantity, TopUnit unit)
        {
            SelectClause.AddTop(quantity, unit);
        }

        /// <summary>
        /// Ajoute une instruction DISTINCT.
        /// </summary>
        public void AddDistinct()
        {
            SelectClause.AddDistinct();
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        public void AddQueriedField(string columnName)
        {
            SelectClause.AddQueriedField(columnName);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public void AddQueriedField(string columnName, bool isCount)
        {
            SelectClause.AddQueriedField(columnName, isCount);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        public void AddQueriedField(string tableName, string columnName)
        {
            SelectClause.AddQueriedField(tableName, columnName);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public void AddQueriedField(string tableName, string columnName, bool isCount)
        {
            SelectClause.AddQueriedField(tableName, columnName, isCount);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="alias">Alias de la colonne.</param>
        public void AddQueriedField(string tableName, string columnName, string alias)
        {
            SelectClause.AddQueriedField(tableName, columnName, alias);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="alias">Alias de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public void AddQueriedField(string tableName, string columnName, string alias, bool isCount)
        {
            SelectClause.AddQueriedField(tableName, columnName, alias, isCount);
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
        /// Ajoute un champ dans la clause ORDER BY.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="sortOrder">Opérateur de tri.</param>
        public void AddOrderBy(string columnName, Sorting sortOrder)
        {
            OrderByClause.AddOrderBy(columnName, sortOrder);
        }

        /// <summary>
        /// Construit la requête.
        /// Tous les noms de paramètres sont automatiquement préfixés du caractère "@".
        /// </summary>
        /// <returns>Requête.</returns>
        protected override StringBuilder BuildQuery()
        {
            StringBuilder sb = new StringBuilder();

            // Construction du commentaire.
            CommentClause.AppendQuery(sb);

            // Construction de la clause SELECT.
            SelectClause.AppendQuery(sb);

            // Construction de la clause FROM.
            FromClause.AppendQuery(sb);

            // Construction de la clause WHERE.
            WhereClause.AppendQuery(sb);

            // Construction de la clause ORDER BY.
            OrderByClause.AppendQuery(sb);

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