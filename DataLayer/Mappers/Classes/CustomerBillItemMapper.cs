using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class CustomerBillItemMapper : ICustomerBillItemMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> customerBillItemSqlParams = new Dictionary<string, SqlParameter>();

        private static ICustomerBillItemMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private CustomerBillItemMapper(IDatabase db)
        {
            this.db = db;
            customerBillItemSqlParams["CustomerBillItemId"] = new SqlParameter("@CustomerBillItemId", SqlDbType.Int);
            customerBillItemSqlParams["CustomerBillId"] = new SqlParameter("@CustomerBillId", SqlDbType.Int);
            customerBillItemSqlParams["BarSaleId"] = new SqlParameter("@BarSaleId", SqlDbType.Int);
            customerBillItemSqlParams["ItemPrice"] = new SqlParameter("@ItemPrice", SqlDbType.Decimal) { Precision = 2 };
        }

        public static ICustomerBillItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new CustomerBillItemMapper(database);
                }
                return instance;
            }
        }

        public static ICustomerBillItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CustomerBillItemMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(ICustomerBillItem customerBillItem)
        {
            string sql = "INSERT INTO CustomerBillItem " +
                "(CustomerBillId, BarSaleId, ItemPrice) " +
                "VALUES " +
                "(@CustomerBillId, @BarSaleId, @ItemPrice)";

            SqlParameter[] sqlParams = SetAllSqlParameters(customerBillItem);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<ICustomerBillItem> GetAll()
        {
            string sql = "SELECT CustomerBillItemId, CustomerBillId, BarSaleId, ItemPrice " +
                "FROM CustomerBillItem";

            return db.ExecuteSelectMultiple<ICustomerBillItem>(sql, ReaderRowToCustomerBillItem);
        }

        public ICustomerBillItem GetByCustomerBillId(int customerBillId)
        {
            string sql = "SELECT CustomerBillItemId, CustomerBillId, BarSaleId, ItemPrice " +
                "FROM CustomerBillItem " +
                "WHERE CustomerBillId = @CustomerBillId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["CustomerBillId"], customerBillId)
            };

            return db.ExecuteSelectSingle<ICustomerBillItem>(sql, sqlParams, ReaderRowToCustomerBillItem);
        }

        public ICustomerBillItem GetByKey(int id)
        {
            string sql = "SELECT CustomerBillItemId, CustomerBillId, BarSaleId, ItemPrice " +
                "FROM CustomerBillItem " +
                "WHERE CustomerBillItemId = @CustomerBillItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["CustomerBillItemId"], id)
            };

            return db.ExecuteSelectSingle<ICustomerBillItem>(sql, sqlParams, ReaderRowToCustomerBillItem);
        }

        #endregion

        #region Update

        public bool Update(ICustomerBillItem customerBillItem)
        {
            string sql = "UPDATE CustomerBillItem " +
                "SET " +
                "CustomerBillItemId = @CustomerBillItemId, " +
                "CustomerBillId = @CustomerBillId, " +
                "BarSaleId = @BarSaleId, " +
                "ItemPrice = @ItemPrice";

            SqlParameter[] sqlParams = SetAllSqlParameters(customerBillItem);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM CustomerBillItem WHERE CustomerBillItemId = @CustomerBillItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["CustomerBillItemId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static ICustomerBillItem ReaderRowToCustomerBillItem(SqlDataReader reader)
        {
            int id = (int)reader["CustomerBillItemId"];
            int customerBillId = (int)reader["CustomerBillId"];
            int? barSaleId = (int?)reader["BarSaleId"];
            double itemPrice = (double)reader["ItemPrice"];

            return new CustomerBillItem(id, customerBillId, barSaleId, itemPrice);
        }

        private static SqlParameter[] SetAllSqlParameters(ICustomerBillItem customerBillItem)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["CustomerBillItemId"], customerBillItem.CustomerBillItemId),
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["CustomerBillId"], customerBillItem.CustomerBillId),
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["BarSaleId"], customerBillItem.BarSaleId),
                MapperUtility.SqlParameterWithValue(customerBillItemSqlParams["ItemPrice"], customerBillItem.ItemPrice)
            };
        }

        #endregion
    }
}