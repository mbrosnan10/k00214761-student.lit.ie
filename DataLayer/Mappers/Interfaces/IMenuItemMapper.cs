using BusinessEntities;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IMenuItemMapper : IMapper<IMenuItem, int>
    {
        List<IMenuItem> GetByMenuId(int menuId);
    }
}