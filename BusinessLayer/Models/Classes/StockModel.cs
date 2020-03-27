using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class StockModel : IStockModel
    {
        private static IStockModel instance;
        private static readonly object padlock = new object();

        private IStockMapper stockMapper;
        private IDatabase db;

        public List<IStock> EntityList { get; private set; }

        #region Singleton stuff

        private StockModel(IDatabase db, IStockMapper stockMapper)
        {
            this.db = db;
            this.stockMapper = stockMapper;
            EntityList = stockMapper.GetAll();
        }

        public static IStockModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IStockMapper stockMapper = StockMapper.GetInstance();
                    instance = new StockModel(db, stockMapper);
                }
                return instance;
            }
        }

        public static IStockModel GetInstance(IDatabase db, IStockMapper stockMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new StockModel(db, stockMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IStock stock)
        {
            return stockMapper.Insert(stock);
        }

        public bool DeleteByKey(int stockid)
        { 
            return stockMapper.DeleteByKey(stockid);
        }

        public bool Update(IStock stock)
        {
            return stockMapper.Update(stock);
        }

        public bool UpdateQuantity(int stockId, int newQuantity)
        {
            return stockMapper.UpdateQuantity(stockId, newQuantity);
        }

        public List<IStock> GetAll()
        {
            return stockMapper.GetAll();
        }

        public IStock GetByKey(int stockid)
        {
            return stockMapper.GetByKey(stockid);
        }

        #endregion
    }
}