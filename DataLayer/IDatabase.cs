using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataLayer
{
    public interface IDatabase
    {
        void CloseConnection();

        SqlConnection GetConnection();

        void OpenConnection();

        #region Query executors

        bool ExecuteDelete(string sql, SqlParameter[] sqlParams);

        bool ExecuteInsert(string sql, SqlParameter[] sqlParams);

        List<T> ExecuteSelectMultiple<T>(string sql, Func<SqlDataReader, T> readerRowToType);
     
        List<T> ExecuteSelectMultiple<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType);

        T ExecuteSelectSingle<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType);

        bool ExecuteUpdate(string sql, SqlParameter[] sqlParams);

        #endregion Query executors
    }
}