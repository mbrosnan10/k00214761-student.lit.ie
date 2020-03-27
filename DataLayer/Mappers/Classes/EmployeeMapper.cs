using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class EmployeeMapper : IEmployeeMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> employeeSqlParams = new Dictionary<string, SqlParameter>();

        private static IEmployeeMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private EmployeeMapper(IDatabase db)
        {
            this.db = db;
            employeeSqlParams["EmployeeId"] = new SqlParameter("@EmployeeId", SqlDbType.Int);
            employeeSqlParams["EmployeeType"] = new SqlParameter("@EmployeeType", SqlDbType.VarChar, 256);
            employeeSqlParams["Username"] = new SqlParameter("@Username", SqlDbType.VarChar, 256);
            employeeSqlParams["Password"] = new SqlParameter("@Password", SqlDbType.VarChar, 256);
            employeeSqlParams["FirstName"] = new SqlParameter("@FirstName", SqlDbType.VarChar, 126);
            employeeSqlParams["LastName"] = new SqlParameter("@LastName", SqlDbType.VarChar, 126);
            employeeSqlParams["DateOfBirth"] = new SqlParameter("@DateOfBirth", SqlDbType.DateTime);
            employeeSqlParams["Email"] = new SqlParameter("@Email", SqlDbType.VarChar, 256);
            employeeSqlParams["ContactNumber"] = new SqlParameter("@ContactNumber", SqlDbType.VarChar, 16);
            employeeSqlParams["Address"] = new SqlParameter("@Address", SqlDbType.VarChar, 256);
            employeeSqlParams["NextOfKinName"] = new SqlParameter("@NextOfKinName", SqlDbType.VarChar, 256);
            employeeSqlParams["NextOfKinNumber"] = new SqlParameter("@NextOfKinNumber", SqlDbType.VarChar, 16);
            employeeSqlParams["Ppsn"] = new SqlParameter("@Ppsn", SqlDbType.VarChar, 10);
            employeeSqlParams["Iban"] = new SqlParameter("@Iban", SqlDbType.VarChar, 34);
            employeeSqlParams["Bic"] = new SqlParameter("@Bic", SqlDbType.VarChar, 8);
            employeeSqlParams["IsTemporary"] = new SqlParameter("@IsTemporary", SqlDbType.TinyInt);
            employeeSqlParams["IsLoggedIn"] = new SqlParameter("@IsLoggedIn", SqlDbType.TinyInt);
            employeeSqlParams["PasswordReset"] = new SqlParameter("@PasswordReset", SqlDbType.TinyInt);
        }

        public static IEmployeeMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new EmployeeMapper(database);
                }
                return instance;
            }
        }

        public static IEmployeeMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new EmployeeMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IEmployee employee)
        {
            string sql = "INSERT INTO Employee " +
                "(EmployeeType, Username, Password, FirstName, LastName, DateOfBirth, Email, ContactNumber, Address, NextOfKinName, NextOfKinNumber, Ppsn, Iban, Bic, IsTemporary, IsLoggedIn) " +
                "VALUES " +
                "(@EmployeeType, @Username, @Password, @FirstName, @LastName, @DateOfBirth, @Email, @ContactNumber, @Address, @NextOfKinName, @NextOfKinNumber, @Ppsn, @Iban, @Bic, @IsTemporary, @IsLoggedIn)";

            SqlParameter[] sqlParams = SetAllSqlParameters(employee);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IEmployee> GetAll()
        {
            string sql = "SELECT EmployeeId, EmployeeType, Username, Password, FirstName, LastName, DateOfBirth, Email, ContactNumber, Address, NextOfKinName, NextOfKinNumber, Ppsn, Iban, Bic, IsTemporary, IsLoggedIn, PasswordReset " +
                "FROM Employee";

            return db.ExecuteSelectMultiple<IEmployee>(sql, ReaderRowToEmployee);
         }

        public IEmployee GetByKey(int id)
        {
            string sql = "SELECT EmployeeId, EmployeeType, Username, Password, FirstName, LastName, DateOfBirth, Email, ContactNumber, Address, NextOfKinName, NextOfKinNumber, Ppsn, Iban, Bic, IsTemporary, IsLoggedIn, PasswordReset " +
                "FROM Employee " +
                "WHERE EmployeeId = @EmployeeId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeId"], id)
            };

            return db.ExecuteSelectSingle<IEmployee>(sql, sqlParams, ReaderRowToEmployee);
        }

        public IEmployee GetByUsername(string username)
        {
            string sql = "SELECT EmployeeId, EmployeeType, Username, Password, FirstName, LastName, DateOfBirth, Email, ContactNumber, Address, NextOfKinName, NextOfKinNumber, Ppsn, Iban, Bic, IsTemporary, IsLoggedIn,PasswordReset " +
                "FROM Employee " +
                "WHERE Username = @Username";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Username"], username)
            };

            return db.ExecuteSelectSingle<IEmployee>(sql, sqlParams, ReaderRowToEmployee);
        }
        public List<IEmployee> GetLogged()
        {
            string sql = "SELECT EmployeeId, EmployeeType, Username, Password, FirstName, LastName, DateOfBirth, Email, ContactNumber, Address, NextOfKinName, NextOfKinNumber, Ppsn, Iban, Bic, IsTemporary, IsLoggedIn "+
                              "FROM Employee " +
                              "WHERE IsLoggedIn = 1";

            return db.ExecuteSelectMultiple<IEmployee>(sql,  ReaderRowToEmployee);
        }

        #endregion

        #region Update

        public bool ChangePassword(int id, string newPassword)
        {
            string sql = "UPDATE Employee " +
                "SET " +
                "Password = @Password, " +
                "PasswordReset = 0 " + 
                "WHERE EmployeeId = @EmployeeId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Password"], newPassword),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeId"], id),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["PasswordReset"],0)
            };

            return db.ExecuteUpdate(sql, sqlParams);
        }

        public bool SetLoggedInStatus(int id, bool value)
        {
            string sql = "UPDATE Employee " +
                "SET " +
                "IsLoggedIn = @IsLoggedIn " +
                "WHERE EmployeeId = @EmployeeId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["IsLoggedIn"], value),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeId"], id)
            };

            return db.ExecuteUpdate(sql, sqlParams);
        }

        public bool Update(IEmployee employee)
        {
            string sql = "UPDATE Employee " +
                "SET " +
                "EmployeeType = @EmployeeType, Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, " +
                "DateOfBirth = @DateOfBirth, Email = @Email, ContactNumber = @ContactNumber, Address = @Address, NextOfKinName = @NextOfKinName, " +
                "NextOfKinNumber = @NextOfKinNumber, Ppsn = @Ppsn, Iban = @Iban, Bic = @Bic, IsTemporary = @IsTemporary, IsLoggedIn = @IsLoggedIn, " +
                "PasswordReset = @PasswordReset "+"WHERE EmployeeId = @EmployeeId";

            SqlParameter[] sqlParams = SetAllSqlParameters(employee);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        public bool DeleteByUsername(string username)
        {
            // SQL query to delete employee entry from db.
            string sql = "DELETE FROM Employee WHERE Username = @Username";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Username"], username)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IEmployee ReaderRowToEmployee(SqlDataReader reader)
        {
            // Get each of the columns data for the row.
            int id = (int)reader["EmployeeId"];
            string employeeType = (string)reader["EmployeeType"];
            string username = (string)reader["Username"];
            string password = (string)reader["Password"];
            string firstName = (string)reader["FirstName"];
            string lastName = (string)reader["LastName"];
            DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
            string email = (string)reader["Email"];
            string contactNum = (string)reader["ContactNumber"];
            string address = (string)reader["Address"];
            string nextOfKinName = (string)reader["NextOfKinName"];
            string nextOfKinNum = (string)reader["NextOfKinNumber"];
            string ppsn = (string)reader["Ppsn"];
            string iban = (string)reader["Iban"];
            string bic = (string)reader["Bic"];
            bool isTempStaff = (byte)reader["IsTemporary"] == 0 ? false : true;
            bool isLoggedIn = (byte)reader["IsLoggedIn"] == 0 ? false : true;
            bool passwordReset = (byte)reader["PasswordReset"] == 0 ? false : true;

            // Set the employee object that we will return).
            return EmployeeFactory.GetEmployee(id, username, password, employeeType, firstName, lastName, dateOfBirth,
                email, contactNum, address, nextOfKinName, nextOfKinNum, ppsn, iban, bic, isTempStaff, isLoggedIn, passwordReset);
        }

        private static SqlParameter[] SetAllSqlParameters(IEmployee employee)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeId"], employee.EmployeeId),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["EmployeeType"], employee.EmployeeType),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Username"], employee.Username),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Password"], employee.Password),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["FirstName"], employee.FirstName),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["LastName"], employee.LastName),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["DateOfBirth"], employee.DateOfBirth),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Email"], employee.Email),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["ContactNumber"], employee.ContactNumber),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Address"], employee.Address),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["NextOfKinName"], employee.NextOfKinName),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["NextOfKinNumber"], employee.NextOfKinNumber),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Ppsn"], employee.Ppsn),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Iban"], employee.Iban),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["Bic"], employee.Bic),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["IsTemporary"], employee.IsTemporary),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["IsLoggedIn"], employee.IsLoggedIn),
                MapperUtility.SqlParameterWithValue(employeeSqlParams["PasswordReset"],employee.PasswordReset)
            };
        }

        public List<IEmployee> GetByEmployeeId(int employeeId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}