using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace DataLayer
{
    public class MenuHasItemsMapper : IMenuHasItemsMapper
    {
        private static readonly object padlock = new object();
        private static IMenuHasItemsMapper instance;

        // Stores the SqlParamter object type for each field in the table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> menuHasItemsSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MenuHasItemsMapper(IDatabase db)
        {
            this.db = db;
            menuHasItemsSqlParams["MenuHasItemsId"] = new SqlParameter("@MenuHasItemsId", SqlDbType.Int);
            menuHasItemsSqlParams["MenuId"] = new SqlParameter("@MenuId", SqlDbType.Int);
            menuHasItemsSqlParams["MenuItemId"] = new SqlParameter("@MenuItemId", SqlDbType.Int);

        }

        public static IMenuHasItemsMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MenuHasItemsMapper(database);
                }
                return instance;
            }
        }

        public static IMenuHasItemsMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuHasItemsMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region methods
        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool DeleteBy(int menuId, int menuItemId)
        {
            string sql = "DELETE FROM MenuHasItems WHERE MenuId = @MenuId AND MenuItemId = @MenuItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuId"], menuId),
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuItemId"], menuItemId),
            };
            return db.ExecuteDelete(sql, sqlParams);
        }

        public List<IMenuHasItems> GetAll()
        {
            string sql = "SELECT MenuHasItemsId, MenuId, MenuItemId " +
                         "FROM MenuHasItems";
            return db.ExecuteSelectMultiple<IMenuHasItems>(sql, ReaderRowToMenuHasItems);
        }

        public IMenuHasItems GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Insert(IMenuHasItems entity)
        {
            string sql = "INSERT INTO MenuHasItems " +
                         "(MenuId, MenuItemId)" +
                         "VALUES" +
                         "(@MenuID, @MenuItemId)";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteInsert(sql, sqlParams);
        }

        public bool Update(IMenuHasItems entity)
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<IMenuHasItems> GetByMenuId(int menuId)
        {
            string sql = "SELECT MenuHasItemsId, MenuItemId, MenuId " +
                         "FROM MenuHasItems " +
                         "WHERE MenuId = @MenuId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuId"], menuId)
            };

            return db.ExecuteSelectMultiple<IMenuHasItems>(sql, sqlParams, ReaderRowToMenuHasItems);
        }



        #region Private methods

        private static IMenuHasItems ReaderRowToMenuHasItems(SqlDataReader reader)
        {
            int id = (int)reader["MenuHasItemsId"];
            int menuId = (int)reader["MenuId"];
            int menuItemId = (int)reader["MenuItemId"];

            return new MenuHasItems(id, menuId, menuItemId);
        }

        private static SqlParameter[] SetAllSqlParameters(IMenuHasItems menuHasItems)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuHasItemsId"], menuHasItems.MenuHasItemsId),
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuId"], menuHasItems.MenuId),
                MapperUtility.SqlParameterWithValue(menuHasItemsSqlParams["MenuItemId"], menuHasItems.MenuItemId),
                
            };
        }

  
        #endregion


    }
}
