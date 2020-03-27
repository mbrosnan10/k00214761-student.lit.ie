using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IIngredients
    {
        int IngredientId { get; set; }
        int StockID { get; set; }
        int MenuItemID { get; set; }
        int Quantity { get; set; }
    }
}
