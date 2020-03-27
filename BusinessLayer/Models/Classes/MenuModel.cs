using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class MenuModel : IMenuModel
    {
        private static IMenuModel instance;
        private static readonly object padlock = new object();

        private IMenuMapper menuMapper;
        private IDatabase db;

        public List<IMenu> EntityList { get; private set;  }

        #region Singleton stuff

        private MenuModel(IDatabase db, IMenuMapper menuMapper)
        {
            this.db = db;
            this.menuMapper = menuMapper;
            EntityList = menuMapper.GetAll();
        }

        public static IMenuModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMenuMapper menuMapper = MenuMapper.GetInstance();
                    instance = new MenuModel(db, menuMapper);
                }
                return instance;
            }
        }

        public static IMenuModel GetInstance(IDatabase db, IMenuMapper menuMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuModel(db, menuMapper);
                }
                return instance;
            }
        }

        #endregion

        public IMenu GetByKey(int menuid)
        {
            return menuMapper.GetByKey(menuid);
        }

        public bool Create(IMenu menu)
        {
            bool result = menuMapper.Insert(menu);
            EntityList = menuMapper.GetAll();
            return result;
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }
        public bool Update(IMenu menu)
        {
            return menuMapper.Update(menu);
        }
        public List<IMenu> GetAll()
        {
            return menuMapper.GetAll();
        }

        public List<IMenu> GetActive()
        {
            return menuMapper.GetActive();
        }
    }
}