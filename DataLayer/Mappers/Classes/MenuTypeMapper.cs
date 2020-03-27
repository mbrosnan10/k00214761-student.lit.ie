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
    public partial class MenuTypeMapper : IMenuTypeMapper
    {

        private static readonly object padlock = new object();
        private static IMenuTypeMapper instance;

        // Stores the SqlParamter object type for each field in the MenuType table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> menuTypeSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MenuTypeMapper(IDatabase db)
        {
            this.db = db;
            menuTypeSqlParams["MenuType"] = new SqlParameter("@MenuType", SqlDbType.VarChar, 256);
          
        }

        public static IMenuTypeMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MenuTypeMapper(database);
                }
                return instance;
            }
        }

        public static IMenuTypeMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuTypeMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IMenuType menuType)
        {
            string sql = "INSERT INTO MenuType " +
                "(MenuType) " +
                "VALUES " +
                "(@MenuType)";

            SqlParameter[] sqlParams = SetAllSqlParameters(menuType);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IMenuType> GetAll()
        {
            string sql = "SELECT MenuType " +
                        "FROM MenuType";

            return db.ExecuteSelectMultiple<IMenuType>(sql, ReaderRowToMenuType);
        }

        public IMenuType GetByKey(string menuType)
        {
            string sql = "SELECT MenuType " +
                "FROM MenuType " +
                "WHERE MenuType = @MenuType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuTypeSqlParams["MenuType"], menuType)
            };

            return db.ExecuteSelectSingle<IMenuType>(sql, sqlParams, ReaderRowToMenuType);
        }

        #endregion

        #region Update

        public bool Update(IMenuType menuType)
        {
            string sql = "UPDATE MenuType " +
                "SET " +
                "MenuType = @MenuType," ;

            SqlParameter[] sqlParams = SetAllSqlParameters(menuType);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(string menuType)
        {
            string sql = "DELETE FROM MenuType WHERE MenuType = @MenuType";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuTypeSqlParams["MenuType"], menuType)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IMenuType ReaderRowToMenuType(SqlDataReader reader)
        { 
            string menuType = (string)reader["MenuType"];

            return new MenuType(menuType);
        }

        private static SqlParameter[] SetAllSqlParameters(IMenuType menuType)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(menuTypeSqlParams["MenuType"], menuType.MenuTypeName),
                
            };
        }

        #endregion
    }
}
