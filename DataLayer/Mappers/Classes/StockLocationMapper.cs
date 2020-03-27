using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class StockLocationMapper : IStockLocationMapper
    {

        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the stock location table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> stockLocationSqlParams = new Dictionary<string, SqlParameter>();

        private static IStockLocationMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private StockLocationMapper(IDatabase db)
        {
            this.db = db;
            stockLocationSqlParams["StockLocation"] = new SqlParameter("@StockLocation", SqlDbType.VarChar, 256);
        }

        public static IStockLocationMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new StockLocationMapper(database);
                }
                return instance;
            }
        }

        public static IStockLocationMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new StockLocationMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region  Create
        public bool Insert(IStockLocation stockLocation)
        {
            string sql = "INSERT INTO StockLocation " +
                "(StockLocation) " +
                "VALUES " +
                "(@StockLocation)";

            SqlParameter[] sqlParams = SetAllSqlParameters(stockLocation);

            return db.ExecuteInsert(sql, sqlParams);
        }
        #endregion

        #region Read
        public List<IStockLocation> GetAll()
        {
            string sql = "SELECT StockLocation " +
                         "FROM StockLocation";
            return db.ExecuteSelectMultiple<IStockLocation>(sql, ReaderRowToStockLocation);
        }
        public IStockLocation GetByKey(string stockLocation)
        {
            string sql = "SELECT StockLocation" +
                         "FROM StockLocation" +
                         "WHERE StockLocation = @StockLocation";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(stockLocationSqlParams["StockLocation"],stockLocation)
            };
            return db.ExecuteSelectSingle<IStockLocation>(sql, sqlParams, ReaderRowToStockLocation);
        }
        #endregion 

        #region Update

        public bool Update(IStockLocation stockLocation)
        {

            string sql = "UPDATE StockLocation" +
                          "SET" +
                          "StockLocation = @StockLocation";

            SqlParameter[] sqlParams = SetAllSqlParameters(stockLocation);
            return db.ExecuteUpdate(sql, sqlParams);

        }

        #endregion

        #region Delete

        public bool DeleteByKey(string stockLocation)
        {
            //sql query to delete from db
            string sql = "DELETE FROM StockLocation WHERE StockLocation = @StockLocation";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(stockLocationSqlParams["StockLocation"], stockLocation)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IStockLocation ReaderRowToStockLocation(SqlDataReader reader)
        {
            // Get each of the columns data for the row.
            string stockLocation = (string)reader["StockLocation"];


            return new StockLocation(stockLocation);

        }

        private static SqlParameter[] SetAllSqlParameters(IStockLocation stockLocation)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(stockLocationSqlParams["StockLocation"], stockLocation),
              
            };
        }

        #endregion
    }
}
