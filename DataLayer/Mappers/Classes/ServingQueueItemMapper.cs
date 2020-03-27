using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class ServingQueueItemMapper : IServingQueueItemMapper
    {
        private static readonly object padlock = new object();
        private static IServingQueueItemMapper instance;

        // Stores the SqlParamter object type for each field in the EmployeeType table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> servingQueueItemSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;
        #region Singleton Stuff

        private ServingQueueItemMapper(IDatabase db)
        {
            this.db = db;
            servingQueueItemSqlParams["ServingQueueItemId"] = new SqlParameter("@ServingQueueItemId", SqlDbType.Int);
            servingQueueItemSqlParams["BarSaleItemId"] = new SqlParameter("@BarSaleItemId", SqlDbType.Int) { IsNullable = true };
            servingQueueItemSqlParams["TimeAdded"] = new SqlParameter("@TimeAdded", SqlDbType.DateTime);
            servingQueueItemSqlParams["TimeCompleted"] = new SqlParameter("@TimeCompleted", SqlDbType.DateTime);
            servingQueueItemSqlParams["IsComplete"] = new SqlParameter("@IsComplete", SqlDbType.TinyInt);
        }

        public static IServingQueueItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new ServingQueueItemMapper(database);
                }
                return instance;
            }
        }

        public static IServingQueueItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ServingQueueItemMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IServingQueueItem servingQueueItem)
        {
            string sql = "INSERT INTO ServingQueueItem " +
                "( TimeAdded, TimeCompleted, IsComplete) " +
                "VALUES " +
                "(@TimeAdded, @TimeCompleted, @IsComplete)";

            SqlParameter[] sqlParams = SetAllSqlParameters(servingQueueItem);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IServingQueueItem> GetAll()
        {
            string sql = "SELECT  ServingQueueItemId, BarSaleItemId, TimeAdded, TimeCompleted, IsComplete " +
                "FROM ServingQueueItem";

            return db.ExecuteSelectMultiple<IServingQueueItem>(sql, ReaderRowToServingQueueItem);
        }

        public IServingQueueItem GetByKey(int id)
        {
            string sql = "SELECT ServingQueueItemId, BarSaleItemId, TimeAdded, TimeCompleted, IsComplete " +
                "FROM ServingQueueItem " +
                "WHERE ServingQueueItemId = @ServingQueueItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["ServingQueueItemId"], id)
            };

            return db.ExecuteSelectSingle<IServingQueueItem>(sql, sqlParams, ReaderRowToServingQueueItem);
        }

        #endregion

        #region Update

        public bool Update(IServingQueueItem servingQueueItem)
        {
            string sql = "UPDATE ServingQueueItem " +
                "SET " +
                "TimeAdded = @TimeAdded ,TimeCompleted = @TimeCompleted, IsComplete = @IsComplete " +
                "WHERE ServingQueueItemId = @ServingQueueItemId";

            SqlParameter[] sqlParams = SetAllSqlParameters(servingQueueItem);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool Delete(int servingQueueItemId)
        {
            string sql = "DELETE FROM ServingQueueItem WHERE ServingQueueItemId= @ServingQueueItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["ServingQueueItemId"], servingQueueItemId)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IServingQueueItem ReaderRowToServingQueueItem(SqlDataReader reader)
        {
            int ServingQueueItemId = (int)reader["ServingQueueItemId"];
            int? BarSaleItemId = (reader["BarSaleItemId"] == DBNull.Value) ? null : (int?)reader["BarSaleItemId"];
            DateTime TimeAdded = (DateTime)reader["TimeAdded"];
            DateTime TimeCompleted = (DateTime)reader["TimeCompleted"];
            bool IsComplete = (byte)reader["IsComplete"] == 0 ? false : true;

            return new ServingQueueItem(ServingQueueItemId, BarSaleItemId, TimeAdded, TimeCompleted, IsComplete);
        }

        private static SqlParameter[] SetAllSqlParameters(IServingQueueItem servingQueueItem)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["ServingQueueItemId"], servingQueueItem.ServingQueueItemID),
                MapperUtility.SqlParameterWithNullableValue(servingQueueItemSqlParams["BarSaleItemId"], servingQueueItem.BarSaleItemID),
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["TimeAdded"], servingQueueItem.TimeAdded),
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["TimeCompleted"], servingQueueItem.TimeCompleted),
                MapperUtility.SqlParameterWithValue(servingQueueItemSqlParams["IsComplete"], servingQueueItem.IsComplete),
            };
        }

      

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
