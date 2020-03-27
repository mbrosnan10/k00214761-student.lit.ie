using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BarSaleItem : IBarSaleItem
    {
        public int BarSaleItemId { get; set; }
        public int BarSaleId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal ItemPrice { get; set; }
        public string Notes { get; set; }

        public BarSaleItem()
        {
        }

        public BarSaleItem(int barSaleItemId, int barSaleId, int menuItemId, int quantity, decimal itemPrice, string notes)
        {
            BarSaleItemId = barSaleItemId;
            BarSaleId = barSaleId;
            MenuItemId = menuItemId;
            Quantity = quantity;
            ItemPrice = itemPrice;
            Notes = notes;
        }
    }
}
