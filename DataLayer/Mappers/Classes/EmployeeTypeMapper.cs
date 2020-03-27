using BusinessEntities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class EmployeeTypeMapper : IEmployeeTypeMapper              
    {
        private static readonly object padlock = new object();
        private static IEmployeeTypeMapper instance;

        // Stores the SqlParamter object type for each field in the EmployeeType table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> employeeTypeSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;


        #region Create

        public bool Insert(IEmployeeType employeeType)
        {
            string sql = "INSERT INTO EmployeeType " +
                "(EmployeeType) " +
                "VALUES " +
                "(@EmployeeType)";

            SqlParameter[] sqlParams = SetAllSqlParameters(employeeType);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IEmployeeType> GetAll()
        {
            string sql = "SELECT EmployeeType" +
                "FROM EmployeeType";

            return db.ExecuteSelectMultiple<IEmployeeType>(sql, ReaderRowToEmployeeType);
        }

        public IEmployeeType Get(string employeeType)
        {
            string sql = "SELECT EmployeeType" +
                "FROM EmployeeType " +
                "WHERE EmployeeType = @EmployeeType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeTypeSqlParams["EmployeeType"], employeeType)
            };

            return db.ExecuteSelectSingle<IEmployeeType>(sql, sqlParams, ReaderRowToEmployeeType);
        }

        #endregion

        #region Update

        public bool Update(IEmployeeType employeeType)
        {
            string sql = "UPDATE EmployeeType " +
                "SET " +
                "EmployeeType = @EmployeeType";

            SqlParameter[] sqlParams = SetAllSqlParameters(employeeType);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool Delete(string employeeType)
        {
            string sql = "DELETE FROM EmployeeType WHERE EmployeeType = @EmployeeType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeTypeSqlParams["EmployeeType"], employeeType)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IEmployeeType ReaderRowToEmployeeType(SqlDataReader reader)
        {
            string employeeType = (string)reader["EmployeeType"];

            return new EmployeeType(employeeType);
        }

        private static SqlParameter[] SetAllSqlParameters(IEmployeeType employeeType)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(employeeTypeSqlParams["EmployeeType"], employeeType.EmployeeTypeName),

            };
        }

        #endregion
    }
}
