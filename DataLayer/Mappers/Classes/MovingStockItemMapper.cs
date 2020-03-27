using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class MovingStockItemMapper : IMovingStockItemMapper
    {
        private static readonly object padlock = new object();
        private static IMovingStockItemMapper instance;

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> movingStockItemSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MovingStockItemMapper(IDatabase db)
        {
            this.db = db;
            movingStockItemSqlParams["MovingStockItemId"] = new SqlParameter("@MovingStockItemId", SqlDbType.Int);
            movingStockItemSqlParams["MovingStockId"] = new SqlParameter("@MovingStockId", SqlDbType.Int);
            movingStockItemSqlParams["StockId"] = new SqlParameter("@StockId", SqlDbType.Int);
            movingStockItemSqlParams["Quantity"] = new SqlParameter("@Quantity", SqlDbType.Int);
        }

        public static IMovingStockItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MovingStockItemMapper(database);
                }
                return instance;
            }
        }

        public static IMovingStockItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MovingStockItemMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IMovingStockItem movingStockItem)
        {
            string sql = "INSERT INTO MovingStockItem " +
                "(MovingStockId, StockId) " +
                "VALUES " +
                "(@MovingStockId, @StockId)";

            SqlParameter[] sqlParams = SetAllSqlParameters(movingStockItem);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IMovingStockItem> GetAll()
        {
            string sql = "SELECT MovingStockItemId, MovingStockId, StockId, Quantity " +
                "FROM MovingStockItem";

            return db.ExecuteSelectMultiple<IMovingStockItem>(sql, ReaderRowToMovingStockItem);
        }

        public IMovingStockItem GetByKey(int movingStockItemId)
        {
            string sql = "SELECT MovingStockItemId, MovingStockId, StockId, Quantity " +
                "FROM MovingStockItem " +
                "WHERE MovingStockItemId = @MovingStockItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["MovingStockItemId"], movingStockItemId)
            };

            return db.ExecuteSelectSingle<IMovingStockItem>(sql, sqlParams, ReaderRowToMovingStockItem);
        }

        public IMovingStockItem GetByMovingStockId(int movingStockId)
        {
            string sql = "SELECT MovingStockItemId, MovingStockId, StockId, Quantity " +
                "FROM MovingStockItem " +
                "WHERE MovingStockId = @MovingStockId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["MovingStockId"], movingStockId)
            };

            return db.ExecuteSelectSingle<IMovingStockItem>(sql, sqlParams, ReaderRowToMovingStockItem);
        }

        public IMovingStockItem GetByStockId(int stockId)
        {
            string sql = "SELECT MovingStockItemId, MovingStockId, StockId, Quantity " +
                "FROM MovingStockItem " +
                "WHERE StockId = @StockId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["StockId"], stockId)
            };

            return db.ExecuteSelectSingle<IMovingStockItem>(sql, sqlParams, ReaderRowToMovingStockItem);
        }

        #endregion

        #region Update

        public bool Update(IMovingStockItem movingStockItem)
        {
            string sql = "UPDATE MovingStockItem " +
                "SET " +
                "MovingStockItemId = @MovingStockItemId, " +
                "MovingStockId = @MovingStockId, " +
                "StockId = @StockId " +
                "Quantity = @Quantity";

            SqlParameter[] sqlParams = SetAllSqlParameters(movingStockItem);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int movingStockItemId)
        {
            string sql = "DELETE FROM MovingStockItem WHERE MovingStockItemId = @MovingStockItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["MovingStockItemId"], movingStockItemId),
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IMovingStockItem ReaderRowToMovingStockItem(SqlDataReader reader)
        {
            int movingStockItemId = (int)reader["MovingStockItemId"];
            int movingStockId = (int)reader["MovingStockId"];
            int stockId = (int)reader["StockId"];
            int quantity = (int)reader["Quantity"];

            return new MovingStockItem(movingStockItemId, movingStockId, stockId, quantity);
        }

        private static SqlParameter[] SetAllSqlParameters(IMovingStockItem movingStockItem)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["MovingStockId"], movingStockItem.MovingStockId),
                MapperUtility.SqlParameterWithValue(movingStockItemSqlParams["StockId"], movingStockItem.StockId),
            };
        }

        #endregion
    }
}