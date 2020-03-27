using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class BarSaleItemModel : IBarSaleItemModel
    {
        private static IBarSaleItemModel instance;
        private static readonly object padlock = new object();

        private IBarSaleItemMapper barSaleItemMapper;
        private IDatabase db;

        public List<IBarSaleItem> EntityList { get; private set; }

        #region Singleton stuff

        private BarSaleItemModel(IDatabase db, IBarSaleItemMapper barSaleItemMapper)
        {
            this.db = db;
            this.barSaleItemMapper = barSaleItemMapper;
            EntityList = barSaleItemMapper.GetAll();
        }

        public static IBarSaleItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IBarSaleItemMapper barSaleItemMapper = BarSaleItemMapper.GetInstance();
                    instance = new BarSaleItemModel(db, barSaleItemMapper);
                }
                return instance;
            }
        }

        public static IBarSaleItemModel GetInstance(IDatabase db, IBarSaleItemMapper barSaleItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new BarSaleItemModel(db, barSaleItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IBarSaleItem barSaleItem)
        {
            bool results = barSaleItemMapper.Insert(barSaleItem);
            EntityList = barSaleItemMapper.GetAll();
            return results;
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IBarSaleItem barsaleitem)
        {
            throw new NotImplementedException();
        }

        public List<IBarSaleItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public IBarSaleItem GetByKey(int key)
        {
            return barSaleItemMapper.GetByKey(key);
        }

        public List<IBarSaleItem> GetByBarSaleId(int id)
        {
            List<IBarSaleItem> barSaleItemsList = barSaleItemMapper.GetByBarSaleId(id);
            return barSaleItemsList;
        }

        public bool DeleteBy(int barSaleId, int menuItemId)
        {
            return barSaleItemMapper.DeleteBy(barSaleId, menuItemId);
        }

        #endregion
    }
}