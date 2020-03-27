using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class CleaningStatusModel : ICleaningStatusModel
    {
        private static ICleaningStatusModel instance;
        private static readonly object padlock = new object();

        private ICleaningStatusMapper cleaningStatusMapper;
        private IDatabase db;

        public List<ICleaningStatus> EntityList { get;}

        #region Singleton stuff

        private CleaningStatusModel(IDatabase db, ICleaningStatusMapper cleaningStatusMapper)
        {
            this.db = db;
            this.cleaningStatusMapper = cleaningStatusMapper;
            EntityList = cleaningStatusMapper.GetAll();
        }

        public static ICleaningStatusModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    ICleaningStatusMapper cleaningStatusMapper = CleaningStatusMapper.GetInstance();
                    instance = new CleaningStatusModel(db, cleaningStatusMapper);
                }
                return instance;
            }
        }

        public static ICleaningStatusModel GetInstance(IDatabase db, ICleaningStatusMapper cleaningStatusMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningStatusModel(db, cleaningStatusMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(ICleaningStatus cleaningstatus)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICleaningStatus cleaningstatus)
        {
            throw new NotImplementedException();
        }

        public List<ICleaningStatus> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICleaningStatus GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}