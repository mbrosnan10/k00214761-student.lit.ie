using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class CleaningRosterMapper : ICleaningRosterMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the CleaningRoster table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> cleaningRosterSqlParams = new Dictionary<string, SqlParameter>();

        private static ICleaningRosterMapper instance;
        private IDatabase db;

        #region Singleton Stuff

        private CleaningRosterMapper(IDatabase db)
        {
            this.db = db;
            cleaningRosterSqlParams["CleaningRosterId"] = new SqlParameter("@CleaningRosterId", SqlDbType.Int);
            cleaningRosterSqlParams["CleaningRosterDate"] = new SqlParameter("@CleaningRosterDate", SqlDbType.DateTime);
            cleaningRosterSqlParams["IsActive"] = new SqlParameter("@IsActive", SqlDbType.TinyInt);
        }

        public static ICleaningRosterMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new CleaningRosterMapper(database);
                }
                return instance;
            }
        }

        public static ICleaningRosterMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningRosterMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region  Create

        public bool Insert(ICleaningRoster cleaningRoster)
        {
            string sql = "INSERT INTO CleaningRoster" +
                        "(CleaningRosterDate, IsActive)" +
                        "VALUES" +
                        "(@CleaningRosterDate, @IsActive)";

            SqlParameter[] sqlParams = SetAllSqlParameters(cleaningRoster);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<ICleaningRoster> GetAll()
        {
            string sql = "SELECT CleaningRosterId, CleaningRosterDate, IsActive " +
                         "FROM CleaningRoster";

            return db.ExecuteSelectMultiple<ICleaningRoster>(sql, ReaderRowToCleaningRoster);
        }

        public ICleaningRoster GetByKey(int id)
        {
            string sql = "SELECT CleaningRosterId, CleaningRosterDate, IsActive " +
                         "FROM CleaningRoster" +
                         "WHERE CleaningRosterId = @CleaningRosterId";

            SqlParameter[] sqlParams =
        {
            MapperUtility.SqlParameterWithValue(cleaningRosterSqlParams["CleaningRosterId"],id)
        };

            return db.ExecuteSelectSingle<ICleaningRoster>(sql, sqlParams, ReaderRowToCleaningRoster);
        }

        #endregion

        #region Update

        public bool Update(ICleaningRoster cleaningRoster)
        {
            string sql = "UPDATE CleaningRoster" +
                         "CleaningRosterDate = @CleaningRosterDate, IsActive = @IsActive" +
                         "WHERE CleaningRosterId = @CleaningRosterId";

            SqlParameter[] sqlParams = SetAllSqlParameters(cleaningRoster);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM CleaningRoster WHERE CleaningRosterId = @CleaningRosterId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(cleaningRosterSqlParams["CleaningRosterId"],id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private Methods

        private static ICleaningRoster ReaderRowToCleaningRoster(SqlDataReader reader)
        {
            int id = (int)reader["CleaningRosterId"];
            DateTime cleaningRosterId = (DateTime)reader["CleaningRosterDate"];
            bool isActive = (byte)reader["IsActive"] == 0 ? false : true;

            return new CleaningRoster(id, cleaningRosterId, isActive);
        }

        private static SqlParameter[] SetAllSqlParameters(ICleaningRoster cleaningRoster)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(cleaningRosterSqlParams["CleaningRosterId"], cleaningRoster.CleaningRosterId),
                MapperUtility.SqlParameterWithValue(cleaningRosterSqlParams["CleaningRosterDate"], cleaningRoster.CleaningRosterDate),
                MapperUtility.SqlParameterWithValue(cleaningRosterSqlParams["IsActive"], cleaningRoster.IsActive)
            };
        }

        #endregion
    }
}