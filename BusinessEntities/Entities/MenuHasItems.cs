using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class MenuHasItems : IMenuHasItems
    {
        public int MenuHasItemsId { get; set; }
        public int MenuId { get; set; }
        public int MenuItemId { get; set; }

        public MenuHasItems()
        {
        }

        public MenuHasItems(int menuHasItemsId, int menuId, int menuItemId)
        {
            MenuHasItemsId = menuHasItemsId;
            MenuId = menuId;
            MenuItemId = menuItemId;
        }
    }
}
