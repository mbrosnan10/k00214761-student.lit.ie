using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class MovingStockMapper : IMovingStockMapper
    {
        private static readonly object padlock = new object();
        private static IMovingStockMapper instance;

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> movingStockSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MovingStockMapper(IDatabase db)
        {
            this.db = db;
            movingStockSqlParams["MovingStockId"] = new SqlParameter("@MovingStockId", SqlDbType.Int);
            movingStockSqlParams["FromLocation"] = new SqlParameter("@FromLocation", SqlDbType.VarChar, 256);
            movingStockSqlParams["ToLocation"] = new SqlParameter("@ToLocation", SqlDbType.VarChar, 256);
        }

        public static IMovingStockMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MovingStockMapper(database);
                }
                return instance;
            }
        }

        public static IMovingStockMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MovingStockMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IMovingStock movingStock)
        {
            string sql = "INSERT INTO MovingStock " +
                "(FromLocation, ToLocation) " +
                "VALUES " +
                "(@FromLocation, @ToLocation)";

            SqlParameter[] sqlParams = SetAllSqlParameters(movingStock);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IMovingStock> GetAll()
        {
            string sql = "SELECT MovingStockId, FromLocation, ToLocation " +
                "FROM MovingStock";

            return db.ExecuteSelectMultiple<IMovingStock>(sql, ReaderRowToMovingStock);
        }

        public IMovingStock GetByKey(int id)
        {
            string sql = "SELECT MovingStockId, FromLocation, ToLocation " +
                "FROM MovingStock " +
                "WHERE MovingStockId = @MovingStockId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockSqlParams["MovingStockId"], id)
            };

            return db.ExecuteSelectSingle<IMovingStock>(sql, sqlParams, ReaderRowToMovingStock);
        }

        #endregion

        #region Update

        public bool Update(IMovingStock movingStock)
        {
            string sql = "UPDATE MovingStock " +
                "SET " +
                "MovingStockId = @MovingStockId, " +
                "FromLocation = @FromLocation, " +
                "ToLocation = @ToLocation";

            SqlParameter[] sqlParams = SetAllSqlParameters(movingStock);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM MovingStock WHERE MovingStockId = @MovingStockId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(movingStockSqlParams["MovingStockId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IMovingStock ReaderRowToMovingStock(SqlDataReader reader)
        {
            int id = (int)reader["MovingStockId"];
            string fromLocation = (string)reader["FromLocation"];
            string toLocation = (string)reader["ToLocation"];

            return new MovingStock(id, fromLocation, toLocation);
        }

        private static SqlParameter[] SetAllSqlParameters(IMovingStock movingStock)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(movingStockSqlParams["MovingStockId"], movingStock.MovingStockId),
                MapperUtility.SqlParameterWithValue(movingStockSqlParams["FromLocation"], movingStock.FromLocation),
                MapperUtility.SqlParameterWithValue(movingStockSqlParams["ToLocation"], movingStock.ToLocation)
            };
        }

        #endregion
    }
}