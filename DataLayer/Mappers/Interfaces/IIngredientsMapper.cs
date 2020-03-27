using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IIngredientsMapper : IMapper<IIngredients, int>
    {
        List<IIngredients> GetByMenuItemId(int menuItemId);
    }
}
