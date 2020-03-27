using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class CleaningRosterItemModel : ICleaningRosterItemModel
    {
        private static ICleaningRosterItemModel instance;
        private static readonly object padlock = new object();

        private ICleaningRosterItemMapper cleaningRosterItemMapper;
        private IDatabase db;

        public List<ICleaningRosterItem> EntityList { get;}

        #region Singleton stuff

        private CleaningRosterItemModel(IDatabase db, ICleaningRosterItemMapper cleaningRosterItemMapper)
        {
            this.db = db;
            this.cleaningRosterItemMapper = cleaningRosterItemMapper;
            EntityList = cleaningRosterItemMapper.GetAll();
        }

        public static ICleaningRosterItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    ICleaningRosterItemMapper cleaningRosterItemMapper = CleaningRosterItemMapper.GetInstance();
                    instance = new CleaningRosterItemModel(db, cleaningRosterItemMapper);
                }
                return instance;
            }
        }

        public static ICleaningRosterItemModel GetInstance(IDatabase db, ICleaningRosterItemMapper cleaningRosterItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CleaningRosterItemModel(db, cleaningRosterItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(ICleaningRosterItem cleaningrosteritem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICleaningRosterItem cleaningrosteritem)
        {
            throw new NotImplementedException();
        }

        public List<ICleaningRosterItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICleaningRosterItem GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}