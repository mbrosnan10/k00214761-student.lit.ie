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
    public class RoomMapper : IRoomMapper
    {

        private static IDictionary<string, SqlParameter> roomSqlParams = new Dictionary<string, SqlParameter>();
        private static IRoomMapper instance;
        private static readonly object padlock = new object();

        private IDatabase db;


        private RoomMapper(IDatabase db)
        {
            this.db = db;
            roomSqlParams["RoomId"] = new SqlParameter("@RoomId", SqlDbType.Int);
            roomSqlParams["RoomType"] = new SqlParameter("@RoomType", SqlDbType.VarChar, 256);
            roomSqlParams["MaxGuests"] = new SqlParameter("@MaxGuests", SqlDbType.Int);
            roomSqlParams["CleaningStatus"] = new SqlParameter("@CleaningStatus", SqlDbType.VarChar, 256);
            roomSqlParams["IsAvailable"] = new SqlParameter("@IsAvailable", SqlDbType.TinyInt);
            roomSqlParams["AllowsSmoking"] = new SqlParameter("@AllowsSmoking", SqlDbType.TinyInt);
            roomSqlParams["HasCot"] = new SqlParameter("@HasCot", SqlDbType.TinyInt);
        }

        public static IRoomMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new RoomMapper(database);
                }
                return instance;
            }
        }

        public static IRoomMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomMapper(database);
                }
                return instance;
            }
        }

        public List<IRoom> GetAll()
        {
            String sql = "SELECT  RoomId,RoomType,CleaningStatus,MaxGuests,IsAvailable,AllowsSmoking,HasCot " +
                    "FROM Room";

            return db.ExecuteSelectMultiple<IRoom>(sql, ReaderRowToRoom);
        }

        public IRoom GetByKey(int RoomId)
        {
            string sql = "SELECT RoomId,RoomType,CleaningStatus,MaxGuests,IsAvailable,AllowsSmoking,HasCot " +
          "FROM Room " +
          "WHERE RoomId = @RoomId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(roomSqlParams["RoomId"], RoomId)
            };
            return db.ExecuteSelectSingle<IRoom>(sql, sqlParams, ReaderRowToRoom);
        }

        public bool Insert(IRoom entity)
        {
            string sql = "INSERT INTO Room " +
               "(RoomId,RoomType,CleaningStatus,MaxGuests,IsAvailable,AllowsSmoking,HasCot) " +
               "VALUES " +
               "(@RoomId,@RoomType,@CleaningStatus,@MaxGuests,@IsAvailable,@AllowsSmoking,@HasCot)";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteInsert(sql, sqlParams);
        }

        public bool Update(IRoom entity)
        {
            string sql = "UPDATE Room " +
                  "SET " +
                  "RoomType = @RoomType, " +
                  "CleaningStatus = @CleaningStatus, " +
                  "MaxGuests = @MaxGuests, " +
                  "IsAvailable = @IsAvailable, " +
                  "AllowsSmoking = @AllowsSmoking, " +
                  "HasCot = @HasCot " + 
                  "WHERE RoomId = @RoomId";

            SqlParameter[] sqlParams = SetAllSqlParameters(entity);

            return db.ExecuteUpdate(sql, sqlParams);
        }

        public bool SetAvalible(int id, bool value)
        {
            string sql = "UPDATE Room " +
                    "SET " +
                    "IsAvailable = @IsAvailable " +
                    "WHERE EmployeeId = @RoomId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(roomSqlParams["IsAvailable"], value),
                MapperUtility.SqlParameterWithValue(roomSqlParams["RoomId"], id)
            };

            return db.ExecuteUpdate(sql, sqlParams);
        }

        public bool DeleteByKey(int key)
        {
            string sql = "DELETE FROM Room WHERE RoomId = @RoomId";

            SqlParameter[] sqlParams = {
                MapperUtility.SqlParameterWithValue(roomSqlParams["RoomId"], key)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        private IRoom ReaderRowToRoom(SqlDataReader reader)
        {
            int roomId = (int)reader["RoomId"];
            string roomType = (string)reader["RoomType"];
            int maxGuest = (int)reader["MaxGuests"];
            string cleaningStatus = (string)reader["CleaningStatus"];
            bool isUsable = (byte)reader["IsAvailable"] == 0 ? false : true;
            bool allowsSmoking = (byte)reader["AllowsSmoking"] == 0 ? false : true;
            bool hascot = (byte)reader["HasCot"] == 0 ? false : true;

            return new Room(roomId, roomType, maxGuest, cleaningStatus, isUsable, allowsSmoking, hascot);
        }

        private static SqlParameter[] SetAllSqlParameters(IRoom room)
        {
            return new SqlParameter[]
            {
                MapperUtility.SqlParameterWithValue(roomSqlParams["RoomId"], room.RoomId),
                MapperUtility.SqlParameterWithValue(roomSqlParams["RoomType"], room.RoomType),
                MapperUtility.SqlParameterWithValue(roomSqlParams["MaxGuests"], room.MaxGuests),
                MapperUtility.SqlParameterWithValue(roomSqlParams["CleaningStatus"], room.CleaningStatus),
                MapperUtility.SqlParameterWithValue(roomSqlParams["IsAvailable"], room.IsUsable),
                MapperUtility.SqlParameterWithValue(roomSqlParams["AllowsSmoking"], room.AllowSmoking),
                MapperUtility.SqlParameterWithValue(roomSqlParams["HasCot"], room.HasCot),
            };
        }




    }
}
