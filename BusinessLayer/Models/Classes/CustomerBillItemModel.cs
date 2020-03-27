using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class CustomerBillItemModel : ICustomerBillItemModel
    {
        private static ICustomerBillItemModel instance;
        private static readonly object padlock = new object();

        private ICustomerBillItemMapper customerBillItemMapper;
        private IDatabase db;

        public List<ICustomerBillItem> EntityList { get; private set; }

        #region Singleton stuff

        private CustomerBillItemModel(IDatabase db, ICustomerBillItemMapper customerBillItemMapper)
        {
            this.db = db;
            this.customerBillItemMapper = customerBillItemMapper;
            EntityList = customerBillItemMapper.GetAll();
        }

        public static ICustomerBillItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    ICustomerBillItemMapper customerBillItemMapper = CustomerBillItemMapper.GetInstance();
                    instance = new CustomerBillItemModel(db, customerBillItemMapper);
                }
                return instance;
            }
        }

        public static ICustomerBillItemModel GetInstance(IDatabase db, ICustomerBillItemMapper customerBillItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CustomerBillItemModel(db, customerBillItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(ICustomerBillItem customerbillitem)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICustomerBillItem customerbillitem)
        {
            throw new NotImplementedException();
        }

        public List<ICustomerBillItem> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICustomerBillItem GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}