using BusinessEntities;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IMenuMapper : IMapper<IMenu, int>
    {
        List<IMenu> GetActive();
    }
}