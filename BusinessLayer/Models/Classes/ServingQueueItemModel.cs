using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class ServingQueueItemModel : IServingQueueItemModel
    {
        private static IServingQueueItemModel instance;
        private static readonly object padlock = new object();

        private IServingQueueItemMapper servingQueueItemMapper;


        public List<IServingQueueItem> EntityList { get; private set; }

        #region Singleton stuff

        private ServingQueueItemModel(IServingQueueItemMapper servingQueueItemMapper)
        {
            
            this.servingQueueItemMapper = servingQueueItemMapper;
            EntityList = servingQueueItemMapper.GetAll();
        }

        public static IServingQueueItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    
                    IServingQueueItemMapper servingQueueItemMapper = ServingQueueItemMapper.GetInstance();
                    instance = new ServingQueueItemModel( servingQueueItemMapper);
                }
                return instance;
            }
        }
        public static IServingQueueItemModel GetInstance(IDatabase db, IServingQueueItemMapper servingQueueItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ServingQueueItemModel( servingQueueItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IServingQueueItem servingQueueItem)
        {
            return servingQueueItemMapper.Insert(servingQueueItem);
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IServingQueueItem servingQueueItem)
        {
            return servingQueueItemMapper.Update(servingQueueItem);
        }

        public List<IServingQueueItem> GetAll()
        {
            return servingQueueItemMapper.GetAll();
        }

        public IServingQueueItem GetByKey(int key)
        {
            return servingQueueItemMapper.GetByKey(key);
        }

        public bool Insert(IServingQueueItem servingQueueItem)
        {
            throw new NotImplementedException();
        }

        #endregion
    
}
}
