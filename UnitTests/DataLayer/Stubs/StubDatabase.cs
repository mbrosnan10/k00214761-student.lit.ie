using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DataLayer;

namespace UnitTests
{
    public class StubDatabase : IDatabase
    {
        public SqlConnection GetConnection_Return { get; set; }


        public StubDatabase()
        { ResetReturns(); }

        public void ResetReturns()
        {
            GetConnection_Return = null;
        }


        public void OpenConnection()
        {}

        public SqlConnection GetConnection()
        { return GetConnection_Return; }

        public void CloseConnection()
        {}

        public bool ExecuteDelete(string sql, SqlParameter[] sqlParams)
        {
            throw new NotImplementedException();
        }

        public List<T> ExecuteSelectMultiple<T>(string sql, Func<SqlDataReader, T> readerRowToType)
        {
            throw new NotImplementedException();
        }

        public T ExecuteSelectSingle<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteInsert(string sql, SqlParameter[] sqlParams)
        {
            throw new NotImplementedException();
        }

        public bool ExecuteUpdate(string sql, SqlParameter[] sqlParams)
        {
            throw new NotImplementedException();
        }

        public List<T> ExecuteSelectMultiple<T>(string sql, SqlParameter[] sqlParams, Func<SqlDataReader, T> readerRowToType)
        {
            throw new NotImplementedException();
        }
    }
}
