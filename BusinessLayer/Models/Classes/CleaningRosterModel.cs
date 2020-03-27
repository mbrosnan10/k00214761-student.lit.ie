using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class CleaningRosterModel : ICleaningRosterModel
    {
        private static ICleaningRosterModel instance;
        private static readonly object padlock = new object();

        private ICleaningRosterMapper cleaningRosterMapper;
        private IDatabase db;

        public List<ICleaningRoster> EntityList { get;}

        #region Singleton stuff

        private CleaningRosterModel(IDatabase db, ICleaningRosterMapper cleaningRosterMapper)
        {
            this.db = db;
            this.cleaningRosterMapper = cleaningRosterMapper;
            EntityList = cleaningRosterMapper.GetAll();
        }

        public static ICleaningRosterModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    ICleaningRosterMapper cleaningRosterMapper = CleaningRosterMapper.GetInstance();
                    instance = new CleaningRosterModel(db, cleaningRosterMapper);
                }
                return instance;
            }
        }

        public static ICleaningRosterModel GetInstance(IDatabase db, ICleaningRosterMapper cleaningRosterMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningRosterModel(db, cleaningRosterMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(ICleaningRoster cleaningroster)
        {
            return cleaningRosterMapper.Insert(cleaningroster);
        }

        public bool DeleteByKey(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICleaningRoster cleaningroster)
        {
            return cleaningRosterMapper.Update(cleaningroster);
        }

        public List<ICleaningRoster> GetAll()
        {
            return cleaningRosterMapper.GetAll();
        }

        public ICleaningRoster GetByKey(int key)
        {
            return cleaningRosterMapper.GetByKey(key);
        }

        #endregion
    }
}