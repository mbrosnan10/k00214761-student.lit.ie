using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class BarSaleModel : IBarSaleModel
    {
        private static IBarSaleModel instance;
        private static readonly object padlock = new object();

        private IBarSaleMapper barSaleMapper;

        public List<IBarSale> EntityList { get; private set; }

        #region Singleton stuff

        private BarSaleModel(IBarSaleMapper barSaleMapper)
        {
            this.barSaleMapper = barSaleMapper;
            EntityList = barSaleMapper.GetAll();
        }

        public static IBarSaleModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IBarSaleMapper barSaleMapper = BarSaleMapper.GetInstance();
                    instance = new BarSaleModel(barSaleMapper);
                }
                return instance;
            }
        }

        public static IBarSaleModel GetInstance(IBarSaleMapper barSaleMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new BarSaleModel(barSaleMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IBarSale barSale)
        {
            return barSaleMapper.Insert(barSale);
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IBarSale barSale)
        {
            return barSaleMapper.Update(barSale);
        }

        public List<IBarSale> GetAll()
        {
            return barSaleMapper.GetAll();
        }

        public IBarSale GetByKey(int barSaleId)
        {
            return barSaleMapper.GetByKey(barSaleId);
        }

        #endregion
    }
}