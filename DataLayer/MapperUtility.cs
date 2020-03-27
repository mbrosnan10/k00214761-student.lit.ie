using System;
using System.Data.SqlClient;

namespace DataLayer
{
    internal static class MapperUtility
    {
        public static SqlParameter SqlParameterWithValue(SqlParameter sqlParam, object value)
        {
            sqlParam.Value = value;
            return sqlParam;
        }

        public static SqlParameter SqlParameterWithNullableValue(SqlParameter sqlParam, object value)
        {
            if (value == null)
            {
                return SqlParameterWithValue(sqlParam, DBNull.Value);
            }
            else
            {
                return SqlParameterWithValue(sqlParam, value);
            }
        }
    }
}