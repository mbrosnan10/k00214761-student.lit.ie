using BusinessEntities;
using DataLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class MenuTypeModel : IMenuTypeModel
    {
        private static IMenuTypeModel instance;
        private static readonly object padlock = new object();

        private IMenuTypeMapper menuTypeMapper;
        private IDatabase db;

        public List<IMenuType> EntityList { get; private set; }

        #region Singleton Stuff

        private MenuTypeModel(IDatabase db, IMenuTypeMapper menuTypeMapper)
        {
            this.db = db;
            this.menuTypeMapper = menuTypeMapper;
            EntityList = menuTypeMapper.GetAll();
        }

        public static IMenuTypeModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMenuTypeMapper menuTypeMapper = MenuTypeMapper.GetInstance();
                    instance = new MenuTypeModel(db, menuTypeMapper);
                }

                return instance;
            }
        }

        public static IMenuTypeModel GetInstance(IDatabase db, IMenuTypeMapper menuTypeMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuTypeModel(db, menuTypeMapper);
                }
                return instance;
            }
        }

        #endregion

        public List<IMenuType> GetAll()
        {
            return menuTypeMapper.GetAll();
        }

        public bool Create(IMenuType entity)
        {
            throw new System.NotImplementedException();
        }

        public IMenuType GetByKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(IMenuType entity)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteByKey(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}