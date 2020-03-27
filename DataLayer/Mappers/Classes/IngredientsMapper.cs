using BusinessEntities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataLayer
{
    public class IngredientsMapper : IIngredientsMapper
    {
        private static readonly object padlock = new object();
        private static IIngredientsMapper instance;

        // Stores the SqlParamter object type for each field in the EmployeeType table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> ingredientsTypeSqlParams = new Dictionary<string, SqlParameter>();

        private IDatabase db;

        #region Singleton stuff

        private IngredientsMapper(IDatabase db)
        {
            this.db = db;
            ingredientsTypeSqlParams["IngredientId"] = new SqlParameter("@IngredientId", SqlDbType.Int);
            ingredientsTypeSqlParams["StockId"] = new SqlParameter("@StockId", SqlDbType.Int);
            ingredientsTypeSqlParams["MenuItemId"] = new SqlParameter("@MenuItemId", SqlDbType.Int);
            ingredientsTypeSqlParams["Quantity"] = new SqlParameter("@Quantity", SqlDbType.Int);
        }

        public static IIngredientsMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new IngredientsMapper(database);
                }
                return instance;
            }
        }

        public static IIngredientsMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new IngredientsMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IIngredients ingredients)
        {
            string sql = "INSERT INTO Ingredients " +
                "(StockId, MenuItemId, Quantity) " +
                "VALUES " +
                "(@StockId, @MenuItemId, @Quantity)";

            SqlParameter[] sqlParams = SetAllSqlParameters(ingredients);

            return db.ExecuteInsert(sql, sqlParams);
        }

        #endregion

        #region Read

        public List<IIngredients> GetAll()
        {
            string sql = "SELECT IngredientId, StockId, MenuItemId, Quantity " +
                "FROM Ingredients";

            return db.ExecuteSelectMultiple<IIngredients>(sql, ReaderRowToIngredients);
        }

        public IIngredients GetByKey(int ingredientId)
        {
            string sql = "SELECT IngredientId, StockId, MenuItemId, Quantity " +
                "FROM Ingredients " +
                "WHERE IngredientId = @IngredientId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["IngredientId"], ingredientId)
            };

            return db.ExecuteSelectSingle<IIngredients>(sql, sqlParams, ReaderRowToIngredients);
        }

        public List<IIngredients> GetByMenuItemId(int menuItemId)
        {
            string sql = "SELECT IngredientId, StockId, MenuItemId, Quantity " +
                "FROM Ingredients " +
                "WHERE MenuItemId = @MenuItemId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["MenuItemId"], menuItemId)
            };

            return db.ExecuteSelectMultiple<IIngredients>(sql, sqlParams, ReaderRowToIngredients);
        }
        #endregion

        #region Update

        public bool Update(IIngredients ingredient)
        {
            string sql = "UPDATE Ingredients " +
                "SET " +
                "StockId = @StockId, MenuItemId = @MenuItemId, Quantity = @Quantity " + 
                "WHERE IngredientId = @IngredientId";

            SqlParameter[] sqlParams = SetAllSqlParameters(ingredient);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        #endregion

        #region Delete

        public bool DeleteByKey(int ingredientId)
        {
            string sql = "DELETE FROM Ingredients WHERE IngredientId = @IngredientId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["IngredientId"], ingredientId)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private methods

        private static IIngredients ReaderRowToIngredients(SqlDataReader reader)
        {
            int IngredientId = (int)reader["IngredientId"];
            int StockId = (int)reader["StockId"];
            int MenuItemId = (int)reader["MenuItemId"];
            int Quantity = (int)reader["Quantity"];

            return new Ingredients(IngredientId, StockId, MenuItemId, Quantity);
        }

        private static SqlParameter[] SetAllSqlParameters(IIngredients ingredient)
        {
            return new SqlParameter[] {
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["IngredientId"], ingredient.IngredientId),
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["StockId"], ingredient.StockID),
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["MenuItemId"], ingredient.MenuItemID),
                MapperUtility.SqlParameterWithValue(ingredientsTypeSqlParams["Quantity"], ingredient.Quantity),
            };
        }

        #endregion
    }
}
