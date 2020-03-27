using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class CustomerBillMapper : ICustomerBillMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> customerBillSqlParams = new Dictionary<string, SqlParameter>();

        private static ICustomerBillMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private CustomerBillMapper(IDatabase db)
        {
            this.db = db;
            customerBillSqlParams["CustomerBillId"] = new SqlParameter("@CustomerBillId", SqlDbType.Int);
            customerBillSqlParams["ReservationId"] = new SqlParameter("@ReservationId", SqlDbType.Int);
            customerBillSqlParams["AmountPaid"] = new SqlParameter("@AmountPaid", SqlDbType.Decimal) { Precision = 2 };
            customerBillSqlParams["IsPaid"] = new SqlParameter("@IsPaid", SqlDbType.Int);
        }

        public static ICustomerBillMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new CustomerBillMapper(database);
                }
                return instance;
            }
        }

        public static ICustomerBillMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CustomerBillMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(ICustomerBill customerBill)
        {
            string sql = "INSERT INTO CustomerBill " +
                "(ReservationId, AmountPaid, IsPaid) " +
                "VALUES " +
                "(@ReservationId, @AmountPaid, @IsPaid)";

            SqlParameter[] sqlParams = SetAllSqlParameters(customerBill);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<ICustomerBill> GetAll()
        {
            string sql = "SELECT CustomerBillId, ReservationId, AmountPaid, IsPaid " +
                "FROM CustomerBill";

            return db.ExecuteSelectMultiple<ICustomerBill>(sql, ReaderRowToCustomerBill);
        }

        public ICustomerBill GetByKey(int id)
        {
            string sql = "SELECT CustomerBillId, ReservationId, AmountPaid, IsPaid " +
                "FROM CustomerBill " +
                "WHERE CustomerBillId = @CustomerBillId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["CustomerBillId"], id)
            };

            return db.ExecuteSelectSingle<ICustomerBill>(sql, sqlParams, ReaderRowToCustomerBill);
        }

        public ICustomerBill GetByReservationId(int reservationId)
        {
            string sql = "SELECT CustomerBillId, ReservationId, AmountPaid, IsPaid " +
                "FROM CustomerBill " +
                "WHERE ReservationId = @ReservationId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["ReservationId"], reservationId)
            };

            return db.ExecuteSelectSingle<ICustomerBill>(sql, sqlParams, ReaderRowToCustomerBill);
        }

        #endregion

        #region Update

        public bool Update(ICustomerBill customerBill)
        {
            string sql = "UPDATE CustomerBill " +
                "SET " +
                "CustomerBillId = @CustomerBillId, " +
                "ReservationId = @ReservationId, " +
                "AmountPaid = @AmountPaid, " +
                "IsPaid = @IsPaid";

            SqlParameter[] sqlParams = SetAllSqlParameters(customerBill);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM CustomerBill WHERE CustomerBillId = @CustomerBillId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["CustomerBillId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static ICustomerBill ReaderRowToCustomerBill(SqlDataReader reader)
        {
            int id = (int)reader["CustomerBillId"];
            int reservationId = (int)reader["ReservationId"];
            double amountPaid = (double)reader["AmountPaid"];
            bool isPaid = (byte)reader["IsPaid"] == 0 ? false : true;

            return new CustomerBill(id, reservationId, amountPaid, isPaid);
        }

        private static SqlParameter[] SetAllSqlParameters(ICustomerBill customerBill)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["CustomerBillId"], customerBill.CustomerBillId),
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["ReservationId"], customerBill.ReservationId),
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["AmountPaid"], customerBill.AmountPaid),
                MapperUtility.SqlParameterWithValue(customerBillSqlParams["IsPaid"], customerBill.IsPaid)
            };
        }

        #endregion
    }
}