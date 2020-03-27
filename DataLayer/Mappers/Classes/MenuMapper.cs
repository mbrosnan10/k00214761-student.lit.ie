using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class MenuMapper : IMenuMapper
    {
        private static readonly object padlock = new object();
        private static IMenuMapper instance;

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> menuSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MenuMapper(IDatabase db)
        {
            this.db = db;
            menuSqlParams["MenuId"] = new SqlParameter("@MenuId", SqlDbType.Int);
            menuSqlParams["MenuType"] = new SqlParameter("@MenuType", SqlDbType.VarChar, 256);
            menuSqlParams["MenuName"] = new SqlParameter("@MenuName", SqlDbType.VarChar, 256);
            menuSqlParams["MenuDescription"] = new SqlParameter("@MenuDescription", SqlDbType.VarChar, 256);
            menuSqlParams["IsActive"] = new SqlParameter("@IsActive", SqlDbType.TinyInt);
        }

        public static IMenuMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MenuMapper(database);
                }
                return instance;
            }
        }

        public static IMenuMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IMenu menu)
        {
            string sql = "INSERT INTO Menu " +
                "(MenuType, MenuName, MenuDescription, IsActive) " +
                "VALUES " +
                "(@MenuType, @MenuName,@MenuDescription, @IsActive)";

            SqlParameter[] sqlParams = SetAllSqlParameters(menu);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IMenu> GetAll()
        {
            string sql = "SELECT MenuId, MenuType, MenuName, MenuDescription, IsActive " +
                "FROM Menu";

            return db.ExecuteSelectMultiple<IMenu>(sql, ReaderRowToMenu);
        }

        public List<IMenu> GetActive()
        {
            string sql = "SELECT MenuId, MenuType, MenuName, MenuDescription, IsActive " +
                              "FROM Menu " +
                              "WHERE IsActive = 1" ;
           
            return db.ExecuteSelectMultiple<IMenu>(sql, ReaderRowToMenu);
        }

        public IMenu GetByKey(int id)
        {
            string sql = "SELECT MenuId, MenuType, MenuName, MenuDescription, IsActive " +
                "FROM Menu " +
                "WHERE MenuId = @MenuId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuId"], id)
            };

            return db.ExecuteSelectSingle<IMenu>(sql, sqlParams, ReaderRowToMenu);
        }

        #endregion

        #region Update

        public bool Update(IMenu menu)
        {
            string sql = "UPDATE Menu " +
                "SET " +
                "MenuType = @MenuType," +
                "MenuName = @MenuName, " +
                "MenuDescription = @MenuDescription," + 
                "IsActive = @IsActive " + "WHERE MenuId = @MenuId";

            SqlParameter[] sqlParams = SetAllSqlParameters(menu);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        public bool SetIsActive(int id, bool value)
        {
            string sql = "UPDATE Menu " +
                "SET " +
                "IsActive = @IsActive " +
                "WHERE MenuId = @Menuid";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuSqlParams["IsActive"], value),
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuId"], id)
            };

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM Menu WHERE MenuId = @MenuId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IMenu ReaderRowToMenu(SqlDataReader reader)
        {
            int id = (int)reader["MenuId"];
            string menuType = (string)reader["MenuType"];
            string menuName = (string)reader["MenuName"];
            string menuDescription = (string)reader["MenuDescription"];
            bool isActive = (byte)reader["IsActive"] == 0 ? false : true;

            return new Menu(id,menuType, menuName, menuDescription,isActive);
        }

        private static SqlParameter[] SetAllSqlParameters(IMenu menu)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuId"], menu.MenuId),
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuType"], menu.MenuType),
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuName"], menu.MenuName),
                MapperUtility.SqlParameterWithValue(menuSqlParams["MenuDescription"], menu.MenuDescription),
                MapperUtility.SqlParameterWithValue(menuSqlParams["IsActive"], menu.IsActive),
            };
        }



        #endregion
 
    }
}