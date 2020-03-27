using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class StockLocationModel : IStockLocationModel
    {
        private static IStockLocationModel instance;
        private static readonly object padlock = new object();

        private IStockLocationMapper stockLocationMapper;
        private IDatabase db;

        public List<IStockLocation> EntityList { get; private set; }

        #region Singleton stuff

        private StockLocationModel(IDatabase db, IStockLocationMapper stockLocationMapper)
        {
            this.db = db;
            this.stockLocationMapper = stockLocationMapper;
            EntityList = stockLocationMapper.GetAll();
        }

        public static IStockLocationModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IStockLocationMapper stockLocationMapper = StockLocationMapper.GetInstance();
                    instance = new StockLocationModel(db, stockLocationMapper);
                }
                return instance;
            }
        }

        public static IStockLocationModel GetInstance(IDatabase db, IStockLocationMapper stockLocationMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new StockLocationModel(db, stockLocationMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IStockLocation stocklocation)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IStockLocation stocklocation)
        {
            throw new NotImplementedException();
        }

        public List<IStockLocation> GetAll()
        {
            throw new NotImplementedException();
        }

        public IStockLocation GetByKey(string key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}