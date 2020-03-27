using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IIngredientsModel : IModel<IIngredients, int>
    {
        List<IIngredients> GetByMenuItemId(int menuItemId);
    }
}
