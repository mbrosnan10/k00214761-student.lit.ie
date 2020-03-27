using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public partial class MenuItemMapper : IMenuItemMapper
    {
        private static readonly object padlock = new object();
        private static IMenuItemMapper instance;

        // Stores the SqlParamter object type for each field in the table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> menuItemSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private MenuItemMapper(IDatabase db)
        {
            this.db = db;
            menuItemSqlParams["MenuItemId"] = new SqlParameter("@MenuItemId", SqlDbType.Int);
            menuItemSqlParams["MenuId"] = new SqlParameter("@MenuId", SqlDbType.Int) { IsNullable = true };
            menuItemSqlParams["MenuItemName"] = new SqlParameter("@MenuItemName", SqlDbType.VarChar, 256);
            menuItemSqlParams["ItemPrice"] = new SqlParameter("@ItemPrice", SqlDbType.Decimal) { Precision = 20 };
            menuItemSqlParams["AllergyInfo"] = new SqlParameter("@AllergyInfo", SqlDbType.VarChar, 1000);
        }

        public static IMenuItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new MenuItemMapper(database);
                }
                return instance;
            }
        }

        public static IMenuItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuItemMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IMenuItem menuItem)
        {
            //string sql = "INSERT INTO MenuItem " +
            //    "(MenuId, MenuItemName, ItemPrice, AllergyInfo) " +
            //    "VALUES " +
            //    "(@MenuId, @MenuItemName, @ItemPrice, @AllergyInfo)";

            string sql = "INSERT INTO MenuItem " +
                "(MenuItemName, ItemPrice, AllergyInfo) " +
                "VALUES " +
                "(@MenuItemName, @ItemPrice, @AllergyInfo)";

            SqlParameter[] sqlParams = SetAllSqlParameters(menuItem);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IMenuItem> GetAll()
        {
            //string sql = "SELECT MenuItemId, MenuId, MenuItemName, ItemPrice, AllergyInfo " +
            //    "FROM MenuItem";

            string sql = "SELECT MenuItemId, MenuItemName, ItemPrice, AllergyInfo " +
                "FROM MenuItem";

            return db.ExecuteSelectMultiple<IMenuItem>(sql, ReaderRowToMenuItem);
        }

        public IMenuItem GetByKey(int id)
        {
            //string sql = "SELECT MenuItemId, MenuId, MenuItemName, ItemPrice, AllergyInfo " +
            //    "FROM MenuItem " +
            //    "WHERE MenuItemId = @MenuItemId";

            string sql = "SELECT MenuItemId, MenuItemName, ItemPrice, AllergyInfo " +
                "FROM MenuItem " +
                "WHERE MenuItemId = @MenuItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemId"], id)
            };

            return db.ExecuteSelectSingle<IMenuItem>(sql, sqlParams, ReaderRowToMenuItem);
        }

        //shoudlnt have to use, relationship handled in menuHasItemsTable
        public List<IMenuItem> GetByMenuId(int menuId)
        {
            string sql = "SELECT MenuItemId, MenuId, MenuItemName, ItemPrice, AllergyInfo " +
                "FROM MenuItem " +
                "WHERE MenuId = @MenuId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuId"], menuId)
            };

            return db.ExecuteSelectMultiple<IMenuItem>(sql, sqlParams, ReaderRowToMenuItem);
        }

        #endregion

        #region Update

        public bool Update(IMenuItem menuItem)
        {
            //string sql = "UPDATE MenuItem " +
            //    "SET " +
            //    "MenuItemId = @MenuItemId, " +
            //    "MenuId = @MenuId, " +
            //    "MenuItemName = @MenuItemName, " +
            //    "ItemPrice = @ItemPrice, " +
            //    "AllergyInfo = @AllergyInfo";

            string sql = "UPDATE MenuItem " +
                "SET " +
                "MenuItemName = @MenuItemName, " +
                "ItemPrice = @ItemPrice, " +
                "AllergyInfo = @AllergyInfo " + 
                "WHERE MenuItemId = @MenuItemId";


            SqlParameter[] sqlParams = SetAllSqlParameters(menuItem);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            string sql = "DELETE FROM MenuItem WHERE MenuItemId = @MenuItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IMenuItem ReaderRowToMenuItem(SqlDataReader reader)
        {
            int id = (int)reader["MenuItemId"];
            //int? menuId = (int?)reader["MenuId"];
            string menuItemName = (string)reader["MenuItemName"];
            decimal itemPrice = (decimal)reader["ItemPrice"];
            string allergyInfo = (string)reader["AllergyInfo"];

            return new MenuItem(id, /*menuId,*/ menuItemName, itemPrice, allergyInfo);
        }

        private static SqlParameter[] SetAllSqlParameters(IMenuItem menuItem)
        {
            // TODO: Make this consistent to the other mappers.
            //SqlParameter[] sqlParameters = new SqlParameter[0];

            //sqlParameters[0] = MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemId"], menuItem.MenuItemId);
            //sqlParameters[1] = MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemName"], menuItem.MenuItemName);
            //sqlParameters[2] = MapperUtility.SqlParameterWithValue(menuItemSqlParams["ItemPrice"], menuItem.ItemPrice);
            //sqlParameters[3] = MapperUtility.SqlParameterWithValue(menuItemSqlParams["AllergyInfo"], menuItem.AllergyInfo);

            return new SqlParameter[] { 
            MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemId"], menuItem.MenuItemId),
            MapperUtility.SqlParameterWithValue(menuItemSqlParams["MenuItemName"], menuItem.MenuItemName),
            MapperUtility.SqlParameterWithValue(menuItemSqlParams["ItemPrice"], menuItem.ItemPrice),
            MapperUtility.SqlParameterWithValue(menuItemSqlParams["AllergyInfo"], menuItem.AllergyInfo)
            };
        }

        #endregion
    }
}