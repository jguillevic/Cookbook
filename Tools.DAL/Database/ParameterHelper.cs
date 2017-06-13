using System;
using System.Data.Common;

namespace Tools.DAL.Database
{
    /// <summary>
    /// Méthodes d'aide à la création des paramètres.
    /// </summary>
    public static class ParameterHelper
    {
        /// <summary>
        /// Crée le nom du paramètre à partir de la chaîne d'entrée.
        /// </summary>
        /// <param name="parameterName">Nom du paramètre.</param>
        /// <returns>Nom du paramètre préfixé d'un "@".</returns>
        public static string CreateParameterName(string parameterName)
        {
            return string.Concat("@", parameterName);
        }

        /// <summary>
        /// Crée une nouvelle instance de <see cref="DbParameter"/>.
        /// Affecte à :
        /// - <see cref="DbParameter.ParameterName"/> <paramref name="parameterName"/>
        /// - <see cref="DbParameter.Value"/> <paramref name="value"/>
        /// Le nom du paramètre est précédé du symbole "@".
        /// </summary>
        /// <param name="command">Commande.</param>
        /// <param name="parameterName">Nom du paramètre.</param>
        /// <param name="value">Valeur.</param>
        /// <returns><see cref="DbParameter"/>.</returns>
        public static DbParameter CreateParameter(this DbCommand command, string parameterName, object value)
        {
            DbParameter parameter;

            parameter = command.CreateParameter();
            parameter.ParameterName = CreateParameterName(parameterName); ;

            parameter.Value = value ?? DBNull.Value;

            return parameter;
        }

        /// <summary>
        /// Ajoute une nouvelle instance de <see cref="DbParameter"/>.
        /// Affecte à :
        /// - <see cref="DbParameter.ParameterName"/> <paramref name="parameterName"/>
        /// - <see cref="DbParameter.Value"/> <paramref name="value"/>
        /// </summary>
        /// <param name="command">Commande.</param>
        /// <param name="parameterName">Nom du paramètre.</param>
        /// <param name="value">Valeur.</param>
        /// <returns>Index de l'élément ajouté.</returns>
        public static int AddParameter(this DbCommand command, string parameterName, object value)
        {
            DbParameter parameter;

            parameter = command.CreateParameter(parameterName, value);

            int index = command.Parameters.Add(parameter);

            return index;
        }
    }
}
