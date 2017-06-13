using Tools.DAL.QueryBuilder.Enum;
using System.Collections.Generic;
using System.Text;

namespace Tools.DAL.QueryBuilder.Clause.Select
{
    /// <summary>
    /// Permet la construction d'une clause SELECT.
    /// </summary>
    public class SelectClause
    {
        /// <summary>
        /// Constructeur de la clause TOP.
        /// </summary>
        private TopClause _topClause;

        /// <summary>
        /// Constructeurs des champs requêtés.
        /// </summary>
        private List<QueriedFieldClause> _queriedFieldClauses;

        /// <summary>
        /// TOP doit-il être utilisé ?
        /// </summary>
        private bool _hasTop;

        /// <summary>
        /// DISTINCT doit-il être utilisé ?
        /// </summary>
        private bool _hasDistinct;

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public SelectClause()
        {
            _hasTop = false;
            _hasDistinct = false;
            _topClause = new TopClause();
            _queriedFieldClauses = new List<QueriedFieldClause>();
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append("SELECT ");

            if (_hasDistinct)
            {
                sb.Append("DISTINCT ");
            }

            if (_hasTop)
            {
                _topClause.AppendQuery(sb);
                sb.Append(" ");
            }

            for (int i = 0; i <_queriedFieldClauses.Count ; i++)
            {
                if (i > 0)
                {
                    sb.AppendLine();
                    sb.Append(", ");
                }

                _queriedFieldClauses[i].AppendQuery(sb); 
            }
        }

        /// <summary>
        /// Ajoute une instruction TOP.
        /// </summary>
        /// <param name="quantity">Quantité.</param>
        /// <param name="unit">Unité.</param>
        public void AddTop(int quantity, TopUnit unit)
        {
            _hasTop = true;

            _topClause.Quantity = quantity;
            _topClause.TopUnit = unit;
        }

        /// <summary>
        /// Ajoute une instruction DISTINCT.
        /// </summary>
        public void AddDistinct()
        {
            _hasDistinct = true;
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        public void AddQueriedField(string columnName)
        {
            AddQueriedField(null, columnName, null, false);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public void AddQueriedField(string columnName, bool isCount)
        {
            AddQueriedField(null, columnName, null, isCount);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        public void AddQueriedField(string tableName, string columnName)
        {
            AddQueriedField(tableName, columnName, null, false);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="isCount">Le champ requêté doit-il être compté ?</param>
        public void AddQueriedField(string tableName, string columnName, bool isCount)
        {
            AddQueriedField(tableName, columnName, null, isCount);
        }

        /// <summary>
        /// Ajoute un champ dont la valeur est à récupérer.
        /// </summary>
        /// <param name="tableName">Nom de la table.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="alias">Alias de la colonne.</param>
        public void AddQueriedField(string tableName, string columnName, string alias)
        {
            AddQueriedField(tableName, columnName, null, false);
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
            _queriedFieldClauses.Add(new QueriedFieldClause(tableName, columnName, alias, isCount));
        }
    }
}