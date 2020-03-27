using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class BarSaleMapper : IBarSaleMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the BarSale table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> barSaleSqlParams = new Dictionary<string, SqlParameter>();

        private static IBarSaleMapper instance;
        private IDatabase db;

        #region Singleton Stuff

        private BarSaleMapper(IDatabase db)
        {
            this.db = db;
            barSaleSqlParams["BarSaleId"] = new SqlParameter("@BarSaleId", SqlDbType.Int);
            barSaleSqlParams["EmployeeId"] = new SqlParameter("@EmployeeId", SqlDbType.Int) { IsNullable = true };
            barSaleSqlParams["DateOfSale"] = new SqlParameter("@DateOfSale", SqlDbType.DateTime);
            barSaleSqlParams["AmountPaid"] = new SqlParameter("@AmountPaid", SqlDbType.Decimal) { Precision = 18 };
            barSaleSqlParams["IsPaid"] = new SqlParameter("@IsPaid", SqlDbType.TinyInt);
        }

        public static IBarSaleMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new BarSaleMapper(database);
                }
                return instance;
            }
        }

        public static IBarSaleMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new BarSaleMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region  Create

        public bool Insert(IBarSale barSale)
        {
            string sql ="INSERT INTO BarSale " +
                        "(EmployeeId, DateOfSale, AmountPaid, IsPaid) " +
                        "VALUES " +
                        "(@EmployeeId, @DateOfSale, @AmountPaid, @IsPaid )";

            SqlParameter[] sqlParams = SetAllSqlParameters(barSale);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IBarSale> GetAll()
        {
            string sql = "SELECT BarSaleId, EmployeeId, DateOfSale, AmountPaid, IsPaid " +
                         "FROM BarSale";

            return db.ExecuteSelectMultiple<IBarSale>(sql, ReaderRowToBarSale);
        }

        public IBarSale GetByKey(int id)
        {
            string sql = "SELECT BarSaleId, EmployeeId, DateOfSale, AmountPaid, IsPaid " +
                         "FROM BarSale " +
                         "WHERE BarSaleId = @BarSaleId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["BarSaleId"],id)
            };

            return db.ExecuteSelectSingle<IBarSale>(sql, sqlParams, ReaderRowToBarSale);
        }

        #endregion

        #region Update

        public bool Update(IBarSale barSale)
        {
            string sql = "UPDATE BarSale " +
                         "SET " +
                         "BarSaleDate = @BarSaleDate" +
                         "DateOfSale = @DateOfSale, " +
                         "AmountPaid = @AmountPaid, " +
                         "IsPaid = @IsPaid " +
                         "WHERE BarSaleId = @BarSaleId";

            SqlParameter[] sqlParams = SetAllSqlParameters(barSale);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM BarSale WHERE BarSaleId = @BarSaleId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["BarSaleId"],id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private Methods

        private static IBarSale ReaderRowToBarSale(SqlDataReader reader)
        {
            int id = (int)reader["BarSaleId"];
            int? employeeId = (reader["EmployeeId"] == DBNull.Value) ? null : (int?)reader["EmployeeId"];
            DateTime dateOfSale = (DateTime)reader["DateOfSale"];
            decimal amountPaid = (decimal)reader["AmountPaid"];
            bool isPaid = (byte)reader["IsPaid"] == 0 ? false : true;

            return new BarSale(id, employeeId, dateOfSale, amountPaid, isPaid);
        }

        private static SqlParameter[] SetAllSqlParameters(IBarSale barSale)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["BarSaleId"], barSale.BarSaleId),
                MapperUtility.SqlParameterWithNullableValue(barSaleSqlParams["EmployeeId"], barSale.EmployeeId),
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["DateOfSale"], barSale.DateOfSale),
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["AmountPaid"], barSale.AmountPaid),
                MapperUtility.SqlParameterWithValue(barSaleSqlParams["IsPaid"], barSale.IsPaid),
            };
        }

        #endregion
    }
}