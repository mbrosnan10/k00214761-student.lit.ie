using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class BarSaleItemMapper : IBarSaleItemMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the BarSaleItem table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> barSaleItemSqlParams = new Dictionary<string, SqlParameter>();

        private static IBarSaleItemMapper instance;
        private IDatabase db;

        #region Singleton Stuff

        private BarSaleItemMapper(IDatabase db)
        {
            this.db = db;
            barSaleItemSqlParams["BarSaleItemId"] = new SqlParameter("@BarSaleItemId", SqlDbType.Int);
            barSaleItemSqlParams["BarSaleId"] = new SqlParameter("@BarSaleId", SqlDbType.Int);
            barSaleItemSqlParams["MenuItemId"] = new SqlParameter("@MenuItemId", SqlDbType.Int);
            barSaleItemSqlParams["Quantity"] = new SqlParameter("@Quantity", SqlDbType.Int);
            barSaleItemSqlParams["ItemPrice"] = new SqlParameter("@ItemPrice", SqlDbType.Decimal) { Precision = 18 };
            barSaleItemSqlParams["Notes"] = new SqlParameter("@Notes", SqlDbType.VarChar, 1000);
        }

        public static IBarSaleItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new BarSaleItemMapper(database);
                }
                return instance;
            }
        }

        public static IBarSaleItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new BarSaleItemMapper(database);
                }
                return instance;
            }
        }

        #endregion


        public List<IBarSaleItem> GetByBarSaleId(int barSaleId)
        {
            string sql = "SELECT BarSaleItemId, BarSaleId, MenuItemId, Quantity, ItemPrice, Notes " +
                         "FROM BarSaleItem " +
                         "WHERE BarSaleId = @BarSaleId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleId"], barSaleId)
            };

            return db.ExecuteSelectMultiple<IBarSaleItem>(sql, sqlParams, ReaderRowToBarSaleItem);
        }


        public bool DeleteBy(int barSaleId, int menuItemId)
        {
            string sql = "DELETE FROM BarSaleItem WHERE BarSaleId = @BarSaleId AND MenuItemId = @MenuItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleId"], barSaleId),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["MenuItemId"], menuItemId),
            };
            return db.ExecuteDelete(sql, sqlParams);
        }

        public bool DeleteByKey(int key)
        {
            string sql = "DELETE FROM BarSaleItem WHERE BarSaleItemId = @BarSaleItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleItemId"], key)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        public List<IBarSaleItem> GetAll()
        {
            String sql = "SELECT  BarSaleItemId,BarSaleId,MenuItemId,Quantity,ItemPrice,Notes " +
                  "FROM BarSaleItem";

            return db.ExecuteSelectMultiple<IBarSaleItem>(sql, ReaderRowToBarSaleItem);
        }

        public IBarSaleItem GetByKey(int key)
        {
            string sql = "SELECT BarSaleItemId,BarSaleId,MenuItemId,Quantity,ItemPrice,Notes " +
           "FROM BarSaleItem " +
           "WHERE BarSaleItemId = @BarSaleItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleItemId"], key)
            };

            return db.ExecuteSelectSingle<IBarSaleItem>(sql, sqlParams, ReaderRowToBarSaleItem);
        }

        public bool Insert(IBarSaleItem entity)
        {
            string sql = "INSERT INTO BarSaleItem " +
                  "(BarSaleId,MenuItemId,Quantity,ItemPrice,Notes) " +
                  "VALUES " +
                  "(@BarSaleId, @MenuItemId, @Quantity, @ItemPrice, @Notes)";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteInsert(sql, sqlParams);
        }

        public bool Update(IBarSaleItem entity)
        {
            string sql = "UPDATE BarSaleItem " +
                 "SET " +
                 "BarSaleItemId = @BarSaleItemId, " +
                 "BarSaleId = @BarSaleId, " +
                 "MenuItemId = @MenuItemId, " +
                 "Quantity = @Quantity, " +
                 "ItemPrice = @ItemPrice" +
                 "Notes = @Notes ";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #region Private Methods

        private static IBarSaleItem ReaderRowToBarSaleItem(SqlDataReader reader)
        {
            int id = (int)reader["BarSaleItemId"];
            int barSaleId = (int)reader["BarSaleId"];
            int menuItemId = (int)reader["MenuItemId"];
            int quantity = (int)reader["Quantity"];
            decimal itemPrice = (decimal)reader["ItemPrice"];
            string notes = (string)reader["Notes"];

            return new BarSaleItem(id, barSaleId, menuItemId, quantity, itemPrice, notes);
        }

        private static SqlParameter[] SetAllSqlParameters(IBarSaleItem barSaleItem)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleItemId"], barSaleItem.BarSaleItemId),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["BarSaleId"], barSaleItem.BarSaleId),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["MenuItemId"], barSaleItem.MenuItemId),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["Quantity"], barSaleItem.Quantity),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["ItemPrice"], barSaleItem.ItemPrice),
                MapperUtility.SqlParameterWithValue(barSaleItemSqlParams["Notes"], barSaleItem.Notes),
            };
        }

        public bool ChargeToReservation(int barsaleid)
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}