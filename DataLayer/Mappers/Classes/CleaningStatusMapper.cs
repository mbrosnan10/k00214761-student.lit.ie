using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class CleaningStatusMapper : ICleaningStatusMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the CleaningStatus table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> cleaningStatusSqlParams = new Dictionary<string, SqlParameter>();

        private static ICleaningStatusMapper instance;
        private IDatabase db;

        #region Singleton Stuff

        private CleaningStatusMapper(IDatabase db)
        {
            this.db = db;
            cleaningStatusSqlParams["CleaningStatus"] = new SqlParameter("@CleaningStatus", SqlDbType.VarChar);
        }

        public static ICleaningStatusMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new CleaningStatusMapper(database);
                }
                return instance;
            }
        }

        public static ICleaningStatusMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningStatusMapper(database);
                }
                return instance;
            }
        }

        #endregion

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public List<ICleaningStatus> GetAll()
        {
            string sql = "SELECT CleaningStatus " +
                "FROM CleaningStatus";

            return db.ExecuteSelectMultiple<ICleaningStatus>(sql, ReaderRowToCleaningStatus);
        }

        public ICleaningStatus GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Insert(ICleaningStatus entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICleaningStatus entity)
        {
            throw new NotImplementedException();
        }

        #region Private Methods

        private static ICleaningStatus ReaderRowToCleaningStatus(SqlDataReader reader)
        {
            string id = (string)reader["CleaningStatus"];

            return new CleaningStatus(id);
        }

        private static SqlParameter[] SetAllSqlParameters(ICleaningStatus cleaningStatus)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(cleaningStatusSqlParams["CleaningStatus"], cleaningStatus.CleaningStatusName),
            };
        }

        #endregion
    }
}