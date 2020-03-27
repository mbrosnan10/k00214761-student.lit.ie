using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class CustomerBillModel : ICustomerBillModel
    {
        private static ICustomerBillModel instance;
        private static readonly object padlock = new object();

        private ICustomerBillMapper customerBillMapper;
        private IDatabase db;

        public List<ICustomerBill> EntityList { get; private set; }

        #region Singleton stuff

        private CustomerBillModel(IDatabase db, ICustomerBillMapper customerBillMapper)
        {
            this.db = db;
            this.customerBillMapper = customerBillMapper;
            EntityList = customerBillMapper.GetAll();
        }

        public static ICustomerBillModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    ICustomerBillMapper customerBillMapper = CustomerBillMapper.GetInstance();
                    instance = new CustomerBillModel(db, customerBillMapper);
                }
                return instance;
            }
        }

        public static ICustomerBillModel GetInstance(IDatabase db, ICustomerBillMapper customerBillMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new CustomerBillModel(db, customerBillMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(ICustomerBill customerbill)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(ICustomerBill customerbill)
        {
            throw new NotImplementedException();
        }

        public List<ICustomerBill> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICustomerBill GetByKey(int key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}