using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IMenuHasItemsMapper : IMapper<IMenuHasItems, int>
    {
        List<IMenuHasItems> GetByMenuId(int menuId);

        bool DeleteBy(int menuId, int menuItemId);
    }
}
