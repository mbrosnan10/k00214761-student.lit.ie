using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Ingredients : IIngredients
    {
        public int IngredientId { get; set; }
        public int StockID { get; set; }
        public int MenuItemID { get; set; }
        public int Quantity { get; set; }

        public Ingredients(int ingredientId, int stockID, int menuItemID, int quantity)
        {
            IngredientId = ingredientId;
            StockID = stockID;
            MenuItemID = menuItemID;
            Quantity = quantity;
        }

    }
}
