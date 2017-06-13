using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Text;
using Tools.DAL.Database;
using Tools.DAL.QueryBuilder.Enum;

namespace Tools.DAL.QueryBuilder.Clause.Where
{
    /// <summary>
    /// Permet la construction d'une condition de recherche dans une clause WHERE.
    /// Condition limitée à 'Champ' 'Opérateur' 'Valeur'.
    /// </summary>
    public class SearchConditionClause
    {
        private StringBuilder _sb;

        /// <summary>
        /// Nom de la colonne.
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Opérateur de comparaison.
        /// </summary>
        public Comparison Comparison { get; set; }

        /// <summary>
        /// Valeur.
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        public SearchConditionClause() : this(null, Comparison.Equals, null) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe.
        /// </summary>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <param name="comparison">Opérateur de comparaison.</param>
        /// <param name="value">Valeur.</param>
        public SearchConditionClause(string columnName, Comparison comparison, object value)
        {
            _sb = new StringBuilder();

            ColumnName = columnName;
            Comparison = comparison;
            Value = value;
        }

        /// <summary>
        /// Ajoute à la ligne du <see cref="StringBuilder"/> en paramètre le résultat de la construction de la clause.
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/> à compléter par la méthode.</param>
        public void AppendQuery(StringBuilder sb)
        {
            sb.Append(ColumnName);

            QueryBuilderEnumHelper.AppendComparison(Comparison, sb);

            if (Comparison == Comparison.In
                || Comparison == Comparison.NotIn)
            {
                sb.Append("(");

                AddForIn((count, item) => 
                {
                    if (count > 0)
                        sb.Append(", ");

                    sb.Append(ParameterHelper.CreateParameterName(string.Format(CultureInfo.CurrentCulture, "{0}{1}", ColumnName, count)));
                });

                sb.Append(")");
            }
            else if (Comparison == Comparison.Like
                || Comparison == Comparison.NotLike)
            {
                sb.Append(string.Format(CultureInfo.CurrentCulture, "%{0}%", ParameterHelper.CreateParameterName(ColumnName)));
            }
            else
            {
                sb.Append(ParameterHelper.CreateParameterName(ColumnName));
            }
        }

        /// <summary>
        /// Ajoute le paramètre à la commande <paramref name="command"/>.
        /// La valeur associée est <see cref="Value"/>.
        /// </summary>
        /// <param name="command">Commande.</param>
        public void AddParameter(DbCommand command)
        {
            if (Comparison == Comparison.In
                || Comparison == Comparison.NotIn)
            {
                AddForIn((count, item) => { command.AddParameter(string.Format(CultureInfo.CurrentCulture, "{0}{1}", ColumnName, count), item); });
            }
            else
            {
                command.AddParameter(ColumnName, Value);
            }
        }

        /// <summary>
        /// Réalise une action dans le cas où la comparaison <see cref="Comparison.In"/>
        /// ou <see cref="Comparison.NotIn"/> est souhaitée.
        /// </summary>
        /// <param name="action">Action à réaliser.</param>
        private void AddForIn(Action<int, object> action)
        {
            var enumerable = (IEnumerable)Value;

            int count = 0;
            foreach (var item in enumerable)
            {
                action(count, item);
                count++;
            }
        }
    }
}