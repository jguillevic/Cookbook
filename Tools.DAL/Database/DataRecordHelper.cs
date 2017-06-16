using System;
using System.Data;

namespace Tools.DAL.Database
{
    public static class DataRecordHelper
    {
        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'une chaîne de caractères.</returns>
        public static string GetString(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            string value = dataRecord.GetString(ordinal);

            return value;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'une chaîne de caractères.</returns>
        public static string GetNullableString(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            object value = dataRecord.GetValue(ordinal);

            if (value != DBNull.Value)
                return (string)value;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'un GUID.</returns>
        public static Guid GetGuid(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            Guid value = dataRecord.GetGuid(ordinal);

            return value;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'un GUID.</returns>
        public static Guid? GetNullableGuid(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            object value = dataRecord.GetValue(ordinal);

            if (value != DBNull.Value)
                return (Guid)value;
            else
                return null;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'un <see cref="int"/>.</returns>
        public static int GetInt32(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            int value = dataRecord.GetInt32(ordinal);

            return value;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne sous la forme d'un <see cref="decimal"/>.</returns>
        public static decimal GetDecimal(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            decimal value = dataRecord.GetDecimal(ordinal);

            return value;
        }

        /// <summary>
        /// Obtient la valeur de la colonne à partir de son nom.
        /// </summary>
        /// <param name="dataRecord">Lecteur d'enregistrements.</param>
        /// <param name="columnName">Nom de la colonne.</param>
        /// <returns>Valeur de la colonne.</returns>
        public static object GetValue(this IDataRecord dataRecord, string columnName)
        {
            int ordinal = dataRecord.GetOrdinal(columnName);

            object value = dataRecord.GetValue(ordinal);

            return value;
        }
    }
}
