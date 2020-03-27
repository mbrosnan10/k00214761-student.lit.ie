using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class MovingStockModel : IMovingStockModel
    {
        private static IMovingStockModel instance;
        private static readonly object padlock = new object();

        private IMovingStockMapper movingStockMapper;
        private IDatabase db;

        public List<IMovingStock> EntityList { get; private set; }

        #region Singleton stuff

        private MovingStockModel(IDatabase db, IMovingStockMapper movingStockMapper)
        {
            this.db = db;
            this.movingStockMapper = movingStockMapper;
            EntityList = movingStockMapper.GetAll();
        }

        public static IMovingStockModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMovingStockMapper movingStockMapper = MovingStockMapper.GetInstance();
                    instance = new MovingStockModel(db, movingStockMapper);
                }
                return instance;
            }
        }

        public static IMovingStockModel GetInstance(IDatabase db, IMovingStockMapper movingStockMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MovingStockModel(db, movingStockMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IMovingStock movingstock)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IMovingStock movingstock)
        {
            throw new NotImplementedException();
        }

        public List<IMovingStock> GetAll()
        {
            throw new NotImplementedException();
        }

        public IMovingStock GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}