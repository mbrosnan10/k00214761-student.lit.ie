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
    public partial class CleaningRosterItemMapper : ICleaningRosterItemMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Cleaning roster item table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> cleaningRosterItemSqlParams = new Dictionary<string, SqlParameter>();


        private static ICleaningRosterItemMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private CleaningRosterItemMapper(IDatabase db)
        {
            this.db = db;
            cleaningRosterItemSqlParams["CleaningRosterItemId"] = new SqlParameter("@CleaningRosterItemId", SqlDbType.Int);
            cleaningRosterItemSqlParams["CleaningRosterId"] = new SqlParameter("@CleaningRosterId", SqlDbType.Int);
            cleaningRosterItemSqlParams["RoomId"] = new SqlParameter("@RoomId", SqlDbType.Int);
            cleaningRosterItemSqlParams["EmployeeId"] = new SqlParameter("@EmployeeId", SqlDbType.Int);
            cleaningRosterItemSqlParams["EmployeeId"] = new SqlParameter("@EmployeeId", SqlDbType.Int);
            cleaningRosterItemSqlParams["TimeAdded"] = new SqlParameter("@TimeAdded", SqlDbType.DateTime);
            cleaningRosterItemSqlParams["TimeCompleted"] = new SqlParameter("@TimeCompleted", SqlDbType.DateTime);
            cleaningRosterItemSqlParams["IsComplete"] = new SqlParameter("@IsComplete", SqlDbType.TinyInt);

        }

        public static ICleaningRosterItemMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new CleaningRosterItemMapper(database);
                }
                return instance;
            }
        }

        public static ICleaningRosterItemMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningRosterItemMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(ICleaningRosterItem cleaningRosterItem)
        {

            string sql = "INSERT INTO CleaningRosterItem" +
                         "(CleaningRosterId,RoomId,EmployeeId,TimeAdded,TimeCompleted,IsComplete)" +
                         "VALUES" +
                         "(CleaningRosterId = @CleaningRosterId,RoomId = @RoomId,EmployeeId = @EmployeeId,TimeAdded = @TimeAdded,TimeCompleted = @TimeCompleted,IsComplete = @IsComplete)";

            SqlParameter[] sqlParams = SetAllSqlParameters(cleaningRosterItem);

            return db.ExecuteInsert(sql, sqlParams);

        }

        #endregion

        #region Read

        public List<ICleaningRosterItem> GetAll()
        {

            string sql = "SELECT CleaningRosterId,RoomId,EmployeeId,TimeAdded,TimeCompleted,IsComplete" +
                          "FROM CleaningStockItem";
            return db.ExecuteSelectMultiple<ICleaningRosterItem>(sql, ReaderRowToCleaningRosterItem);


        }

        public ICleaningRosterItem GetByKey(int id)
        {

            string sql = "SELECT CleaningRosterId,RoomId,EmployeeId,TimeAdded,TimeCompleted,IsComplete" +
                          "FROM CleaningStockItem" +
                          "WHERE CleaningRosterId = @CleaningRosterId";
            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(cleaningRosterItemSqlParams["CleaningRosterItemId"], id)
            };

            return db.ExecuteSelectSingle<ICleaningRosterItem>(sql, sqlParams, ReaderRowToCleaningRosterItem);

        }

        #endregion

        #region Update

        public bool Update(ICleaningRosterItem cleaningRosterItem)
        {

            string sql = "Update CleaningRosterItem" +
                          "SET" +
                          "CleaningRosterId = @CleaningRosterId,RoomId = @RoomId,EmployeeId = @EmployeeId,TimeAdded = @TimeAdded,TimeCompleted = @TimeCompleted,IsComplete = @IsComplete";

            SqlParameter[] sqlParams = SetAllSqlParameters(cleaningRosterItem);
            return db.ExecuteUpdate(sql, sqlParams);

        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            //sql query to delete from db
            string sql = "DELETE FROM CleaningRosterItem WHERE CleaningRosterId = @CleaningRosterId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(cleaningRosterItemSqlParams["CleaningRosterId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private Methods
        private static ICleaningRosterItem ReaderRowToCleaningRosterItem(SqlDataReader reader)
        {
            int id = (int)reader["CleaningRosterItemId"];
            int cleaningRosterId = (int)reader["CleaningRosterId"];
            int roomId = (int)reader["RoomId"];
            int employeeId = (int)reader["EmployeeId"];
            DateTime timeAdded = (DateTime)reader["TimeAdded"];
            DateTime timeCompleted = (DateTime)reader["TimeCompleted"];
            bool isCompleted = (bool)reader["IsCompleted"];


            return new CleaningRosterItem(id, cleaningRosterId, roomId, employeeId, timeAdded, timeCompleted, isCompleted);
        }


        private static SqlParameter[] SetAllSqlParameters(ICleaningRosterItem cleaningRosterItem)
        {

            return new SqlParameter[] {
            MapperUtility.SqlParameterWithValue(cleaningRosterItemSqlParams["CleaningRosterItemId"],cleaningRosterItem.CleaningRosterItemId),
            
            };
        }

        #endregion
    }
}
