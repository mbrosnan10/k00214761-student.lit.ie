using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class RoomTypeModel : IRoomTypeModel
    {
        private static IRoomTypeModel instance;
        private static readonly object padlock = new object();

        private IRoomTypeMapper roomTypeMapper;
        private IDatabase db;

        public List<IRoomType> EntityList { get; private set; }

        #region Singleton stuff

        private RoomTypeModel(IDatabase db, IRoomTypeMapper roomTypeMapper)
        {
            this.db = db;
            this.roomTypeMapper = roomTypeMapper;
            EntityList = roomTypeMapper.GetAll();
        }

        public static IRoomTypeModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IRoomTypeMapper roomTypeMapper = RoomTypeMapper.GetInstance();
                    instance = new RoomTypeModel(db, roomTypeMapper);
                }
                return instance;
            }
        }

        public static IRoomTypeModel GetInstance(IDatabase db, IRoomTypeMapper roomTypeMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomTypeModel(db, roomTypeMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IRoomType roomtype)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IRoomType roomtype)
        {
            throw new NotImplementedException();
        }

        public List<IRoomType> GetAll()
        {
            return roomTypeMapper.GetAll();
        }

        public IRoomType GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}