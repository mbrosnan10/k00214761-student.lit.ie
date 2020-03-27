using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IBarSaleItem
    {
        int BarSaleItemId { get; set; }
        int BarSaleId { get; set; }
        int MenuItemId { get; set; }
        int Quantity { get; set; }
        decimal ItemPrice { get; set; }
        string Notes { get; set; }
    }
}
