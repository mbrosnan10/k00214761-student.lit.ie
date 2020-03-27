using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class MovingStockItemModel : IMovingStockItemModel
    {
        private static IMovingStockItemModel instance;
        private static readonly object padlock = new object();

        private IMovingStockItemMapper movingStockItemMapper;
        private IDatabase db;

        public List<IMovingStockItem> EntityList { get; private set; }

        #region Singleton stuff

        private MovingStockItemModel(IDatabase db, IMovingStockItemMapper movingStockItemMapper)
        {
            this.db = db;
            this.movingStockItemMapper = movingStockItemMapper;
            EntityList = movingStockItemMapper.GetAll();
        }

        public static IMovingStockItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMovingStockItemMapper movingStockItemMapper = MovingStockItemMapper.GetInstance();
                    instance = new MovingStockItemModel(db, movingStockItemMapper);
                }
                return instance;
            }
        }

        public static IMovingStockItemModel GetInstance(IDatabase db, IMovingStockItemMapper movingStockItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MovingStockItemModel(db, movingStockItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IMovingStockItem movingstock)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IMovingStockItem movingstock)
        {
            throw new NotImplementedException();
        }

        public List<IMovingStockItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IMovingStockItem GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}