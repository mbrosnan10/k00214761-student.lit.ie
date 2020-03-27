using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataLayer;

namespace BusinessLayer
{

    public class MenuHasItemsModel : IMenuHasItemsModel
    {

        private static IMenuHasItemsModel instance;
        private static readonly object padlock = new object();
        private IDatabase db;

        private IMenuHasItemsMapper menuHasItemsMapper;

        public List<IMenuHasItems> EntityList { get; private set; }

        #region Singleton stuff

        private MenuHasItemsModel(IDatabase db, IMenuHasItemsMapper menuHasItemsMapper)
        {
           
            this.menuHasItemsMapper = menuHasItemsMapper;
            EntityList = menuHasItemsMapper.GetAll();
        }

        public static IMenuHasItemsModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IMenuHasItemsMapper menuHasItemsMapper = MenuHasItemsMapper.GetInstance();
                    instance = new MenuHasItemsModel(db, menuHasItemsMapper);
                }
                return instance;
            }
        }

        public static IMenuHasItemsModel GetInstance(IDatabase db, IMenuHasItemsMapper menuHasItemsMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new MenuHasItemsModel(db, menuHasItemsMapper);
                }
                return instance;
            }
        }

        #endregion

        #region methods
        public bool Create(IMenuHasItems entity)
        {
            bool results = menuHasItemsMapper.Insert(entity);
            EntityList = menuHasItemsMapper.GetAll();
            return results;
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public List<IMenuHasItems> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<IMenuHasItems> GetByMenuId(int menuId)
        {
            List<IMenuHasItems> menuHasItemsList = menuHasItemsMapper.GetByMenuId(menuId);
            return menuHasItemsList;
        }

        //delete by menu id and menu item id
        public bool DeleteBy(int menuId, int menuItemId)
        {
            
            return menuHasItemsMapper.DeleteBy(menuId, menuItemId);
        }

        public IMenuHasItems GetByKey(int key)
        {
            throw new NotImplementedException();
        }

       

        public bool Update(IMenuHasItems entity)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
