using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class RoomTypeMapper : IRoomTypeMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the RoomType table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> roomTypeSqlParams = new Dictionary<string, SqlParameter>();

        private static IRoomTypeMapper instance;
        private IDatabase db;

        #region Singleton Stuff

        private RoomTypeMapper(IDatabase db)
        {
            this.db = db;
            roomTypeSqlParams["RoomType"] = new SqlParameter("@RoomType", SqlDbType.Int);
        }

        public static IRoomTypeMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new RoomTypeMapper(database);
                }
                return instance;
            }
        }

        public static IRoomTypeMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomTypeMapper(database);
                }
                return instance;
            }
        }

        #endregion

        public bool DeleteByKey(string key)
        {
            string sql = "DELETE FROM RoomType WHERE RoomType = @RoomType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(roomTypeSqlParams["RoomType"], key)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        public List<IRoomType> GetAll()
        {
            String sql = "SELECT RoomType " +
                       "FROM RoomType";

            return db.ExecuteSelectMultiple<IRoomType>(sql, ReaderRowToRoomType);
        }

        public IRoomType GetByKey(string key)
        {
            string sql = "SELECT RoomType " +
             "FROM RoomType " +
             "WHERE RoomType = @RoomType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(roomTypeSqlParams["RoomType"], key)
            };

            return db.ExecuteSelectSingle<IRoomType>(sql, sqlParams, ReaderRowToRoomType);
        }

        public bool Insert(IRoomType entity)
        {
            string sql = "INSERT INTO RoomType " +
                    "(RoomType) " +
                    "VALUES " +
                    "(@RoomType)";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteInsert(sql, sqlParams);
        }

        public bool Update(IRoomType entity)
        {
            string sql = "UPDATE RoomType " +
                "SET " +
                "RoomType = @RoomType, ";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #region Private Methods

        private static IRoomType ReaderRowToRoomType(SqlDataReader reader)
        {
            string id = (string)reader["RoomType"];

            return new RoomType(id);
        }

        private static SqlParameter[] SetAllSqlParameters(IRoomType roomType)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(roomTypeSqlParams["RoomType"], roomType.RoomTypeName),
            };
        }

        #endregion
    }
}