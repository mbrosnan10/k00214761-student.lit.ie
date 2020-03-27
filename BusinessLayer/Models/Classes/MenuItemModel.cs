using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public partial class MenuItemModel : IMenuItemModel
    {
        private static IMenuItemModel instance;
        private static readonly object padlock = new object();

        private IMenuItemMapper menuItemMapper;
        private IDatabase db;

        public List<IMenuItem> EntityList { get; private set; }

        #region Singleton stuff

        private MenuItemModel(IDatabase db, IMenuItemMapper menuItemMapper)
        {
            this.db = db;
            this.menuItemMapper = menuItemMapper;
            EntityList = menuItemMapper.GetAll();
        }

        public static IMenuItemModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMenuItemMapper menuItemMapper = MenuItemMapper.GetInstance();
                    instance = new MenuItemModel(db, menuItemMapper);
                }
                return instance;
            }
        }

        public static IMenuItemModel GetInstance(IDatabase db, IMenuItemMapper menuItemMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuItemModel(db, menuItemMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IMenuItem menuItem)
        {
            bool result = menuItemMapper.Insert(menuItem);
            EntityList = menuItemMapper.GetAll();
            return result;
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IMenuItem menuItem)
        {
            bool result =  menuItemMapper.Update(menuItem);
            EntityList = menuItemMapper.GetAll();
            return result;
        }

        public List<IMenuItem> GetAll()
        {
            return menuItemMapper.GetAll();
        }

        public IMenuItem GetByKey(int key)
        {
            return menuItemMapper.GetByKey(key);
        }

        public bool Insert(IMenuItem menuitem)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}