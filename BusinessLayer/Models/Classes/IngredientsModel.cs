using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class IngredientsModel : IIngredientsModel
    {
        private static readonly object padlock = new object();
        private static IIngredientsModel instance;
        private IDatabase db;
        private IIngredientsMapper ingredientsMapper;

        public List<IIngredients> EntityList { get; private set; }

        #region Singleton stuff

        private IngredientsModel(IDatabase db, IIngredientsMapper ingredientsMapper)
        {
            this.db = db;
            this.ingredientsMapper = ingredientsMapper;
            EntityList = ingredientsMapper.GetAll();
        }

        public static IIngredientsModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IIngredientsMapper ingredientsMapper = IngredientsMapper.GetInstance();
                    instance = new IngredientsModel(db, ingredientsMapper);
                }
                return instance;
            }
        }

        public static IIngredientsModel GetInstance(IDatabase db, IIngredientsMapper ingredientsMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new IngredientsModel(db, ingredientsMapper);
                }
                return instance;
            }
        }

        #endregion

        public bool Create(IIngredients ingredients)
        {
            throw new NotImplementedException();
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public List<IIngredients> GetAll()
        {
            throw new NotImplementedException();
        }

        public IIngredients GetByKey(int ingredientsId)
        {
            throw new NotImplementedException();
        }

        public List<IIngredients> GetByMenuItemId(int menuItemId)
        {
            return ingredientsMapper.GetByMenuItemId(menuItemId);
        }

        public bool Update(IIngredients ingredients)
        {
            throw new NotImplementedException();
        }
    }
}