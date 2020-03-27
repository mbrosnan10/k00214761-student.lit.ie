using BusinessEntities.Entities;
using BusinessEntities.Interfaces;
using DataLayer.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public class RoomTypeMapper : IRoomTypeMapper
    {
        private static IRoomTypeMapper instance;
        private static readonly object padlock = new object();

        private IDatabase db;
        private SqlCommand command;
        private RoomTypeMapper(IDatabase db)
        {
            this.db = db;
        }
        public static IRoomTypeMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new RoomTypeMapper(database);
                }
                return instance;
            }
        }
        public static IRoomTypeMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomTypeMapper(database);
                }
                return instance;
            }
        }

        public List<IRoomType> GetAll()
        {
            string sql = "SELECT RoomType FROM RoomType";

                return db.ExecuteSelectMultiple<IRoomType>(sql, ReaderRowToRoomType);
        }

        private IRoomType ReaderRowToRoomType(SqlDataReader reader)
        {
            int roomtype = (int)reader["RoomType"];
            return new RoomType(roomtype);
        }

        public IRoomType GetById(int roomtype)
        {
            return null;
        }
    }
}
