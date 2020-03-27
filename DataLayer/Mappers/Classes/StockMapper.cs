using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class StockMapper : IStockMapper
    {

        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Stock table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> stockSqlParams = new Dictionary<string, SqlParameter>();

        private static IStockMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private StockMapper(IDatabase db)
        {
            this.db = db;
            stockSqlParams["StockId"] = new SqlParameter("@StockId", SqlDbType.Int);
            stockSqlParams["ItemName"] = new SqlParameter("@ItemName", SqlDbType.VarChar, 256);
            stockSqlParams["Description"] = new SqlParameter("@Description", SqlDbType.VarChar, 256);
            stockSqlParams["StockLocation"] = new SqlParameter("@StockLocation", SqlDbType.VarChar, 256);
            stockSqlParams["Quantity"] = new SqlParameter("@Quantity", SqlDbType.Int);
            stockSqlParams["Supplier"] = new SqlParameter("@Supplier", SqlDbType.VarChar, 256);
            stockSqlParams["DateAdded"] = new SqlParameter("@DateAdded", SqlDbType.DateTime);
            stockSqlParams["ExpiryDate"] = new SqlParameter("@ExpiryDate", SqlDbType.DateTime);
        }

        public static IStockMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new StockMapper(database);
                }
                return instance;
            }
        }

        public static IStockMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new StockMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IStock stock)
        {

            string sql = "INSERT INTO Stock " +
                         "(ItemName,Description,StockLocation,Quantity,Supplier,DateAdded,ExpiryDate )" +
                         "VALUES " +
                         "(@ItemName,@Description,@StockLocation,@Quantity,@Supplier,@DateAdded,@ExpiryDate)";

            SqlParameter[] sqlParams = SetAllSqlParameters(stock);

            return db.ExecuteInsert(sql, sqlParams);

        }

        #endregion

        #region Read

        public List<IStock> GetAll()
        {

            string sql = "SELECT StockID,StockLocation,ItemName,Description, Quantity,Supplier,DateAdded,ExpiryDate " +
                          "FROM Stock";
            return db.ExecuteSelectMultiple<IStock>(sql, ReaderRowToStock);


        }

        public IStock GetByKey(int id)
        {

            string sql = "SELECT StockID,StockLocation,ItemName,Description, Quantity,Supplier,DateAdded,ExpiryDate " +
                          "FROM Stock " +
                          "WHERE StockId = @StockId";
            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(stockSqlParams["StockId"], id)
            };

            return db.ExecuteSelectSingle<IStock>(sql, sqlParams, ReaderRowToStock);

        }

        #endregion

        #region Update

        public bool Update(IStock stock)
        {

            string sql = "UPDATE Stock" +
                          "SET" +
                          "StockID = @StockID,ItemName = @ItemName,Description = Description,StockLocation = @StockLocation, Quantity = @Quantity,Supplier = @Supplier,DateAdded = @DateAdded,ExpiryDate = @ExpiryDate";

            SqlParameter[] sqlParams = SetAllSqlParameters(stock);
            return db.ExecuteUpdate(sql, sqlParams);

        }

        public bool UpdateQuantity(int stockId, int newQuantity)
        {

            string sql = "UPDATE Stock " +
                          "SET " +
                          "Quantity = @Quantity " +
                          "WHERE StockID = @StockId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(stockSqlParams["StockId"], stockId),
                MapperUtility.SqlParameterWithValue(stockSqlParams["Quantity"], newQuantity)
            };

            return db.ExecuteUpdate(sql, sqlParams);

        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            //sql query to delete from db
            string sql = "DELETE FROM Stock WHERE StockId = @StockId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(stockSqlParams["StockId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IStock ReaderRowToStock(SqlDataReader reader)
        {
            // Get each of the columns data for the row.
            int id = (int)reader["StockId"];
            string itemName = (string)reader["ItemName"];
            string description = (string)reader["Description"];
            string stockLocation = (string)reader["StockLocation"];
            int quantity = (int)reader["Quantity"];
            string supplier = (string)reader["Supplier"];
            DateTime dateAdded = (DateTime)reader["DateAdded"];
            DateTime expiryDate = (DateTime)reader["ExpiryDate"];


            return new Stock(id, itemName, description, stockLocation, quantity, supplier, dateAdded, expiryDate);
        }

        private static SqlParameter[] SetAllSqlParameters(IStock stock)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(stockSqlParams["StockId"], stock.StockId),
                MapperUtility.SqlParameterWithValue(stockSqlParams["ItemName"], stock.ItemName),
                MapperUtility.SqlParameterWithValue(stockSqlParams["Description"], stock.Description),
                MapperUtility.SqlParameterWithValue(stockSqlParams["StockLocation"], stock.StockLocation),
                MapperUtility.SqlParameterWithValue(stockSqlParams["Quantity"], stock.Quantity),
                MapperUtility.SqlParameterWithValue(stockSqlParams["Supplier"], stock.Supplier),
                MapperUtility.SqlParameterWithValue(stockSqlParams["DateAdded"], stock.DateAdded),
                MapperUtility.SqlParameterWithValue(stockSqlParams["ExpiryDate"], stock.ExpiryDate)


            };
        }

        #endregion
    }
}
