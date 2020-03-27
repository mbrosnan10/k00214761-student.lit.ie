using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public interface IMenuHasItemsModel : IModel<IMenuHasItems, int>
    {
        List<IMenuHasItems> GetByMenuId(int menuId);
        bool DeleteBy(int menuId, int menuItemId);
    }
}
