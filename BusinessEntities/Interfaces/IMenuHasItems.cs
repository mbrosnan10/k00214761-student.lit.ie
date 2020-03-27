using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IMenuHasItems
    {
        int MenuHasItemsId { get; set; }
        int MenuId { get; set; }
        int MenuItemId { get; set; }

    }
}
