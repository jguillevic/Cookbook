using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Insert
{
    /// <summary>
    /// Permet la construction d'une clause INSERT.
    /// </summary>
    public class InsertClause
    {
        /// <summary>
        /// Nom de la table sur laquelle INSERT s'applique.
        /// </summary>
        private string _tableName;

        /// <summary>
        /// Constructeur des champs ajoutés.
        /// </summary>
        private InsertFieldClause _insertFields;

        /// <summary>
        /// Constructeur des valeurs ajoutés.
        /// </summary>
        private List<InsertValuesClause> _insertValues;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public InsertClause()
        {
            _insertValues = new List<InsertValuesClause>();
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            AppendInsertInto(sb);

            AppendValues(sb);
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre les champs à insérer.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        private void AppendInsertInto(StringBuilder sb)
        {
            sb.Append("INSERT INTO ");

            sb.Append(_tableName);

            _insertFields.AppendQuery(sb);
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre les paramètres.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        private void AppendValues(StringBuilder sb)
        {
            sb.Append(" VALUES ");

            for (int i = 0; i < _insertValues.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                _insertValues[i].AppendQuery(sb, _insertFields.ColumnNames, i);
            }
        }

        /// <summary>
        /// Défini le nom de la table sur laquelle porte l'INSERT.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        public void SetTableName(string tableName)
        {
            _tableName = tableName;
        }

        /// <summary>
        /// Ajoute le champ <paramref name="columnNames"/> dans la liste des champs à affecter.
        /// </summary>
        /// <param name="columnName">Nom du champ.</param>
        public void AddInsertFields(IEnumerable<string> columnNames)
        {
            _insertFields = new InsertFieldClause(columnNames);
        }

        /// <summary>
        /// Ajoute le champ <paramref name="values"/> dans la liste des valeurs.
        /// </summary>
        /// <param name="values">Valeurs.</param>
        public void AddInsertValues(IEnumerable<object> values)
        {
            _insertValues.Add(new InsertValuesClause(values));
        }

        /// <summary>
        /// Ajoute le champ <paramref name="values"/> dans la liste des valeurs.
        /// </summary>
        /// <param name="values">Valeurs.</param>
        public void AddInsertValues(IEnumerable<IEnumerable<object>> values)
        {
            foreach (var value in values)
            {
                AddInsertValues(value);
            }
        }

        /// <summary>
        /// Ajoute les paramètre à la commande <paramref name="command"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        internal void AddParameters(DbCommand command)
        {
            foreach (var insertValue in _insertValues)
            {
                insertValue.AddParameters(command);
            }
        }
    }
}
