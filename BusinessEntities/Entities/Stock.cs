using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Stock : IStock
    {
        public int StockId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string  StockLocation { get; set; }
        public int Quantity { get; set; }
        public string Supplier { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Stock()
        {
        }

        public Stock(int stockId)
        {
            StockId = stockId;
            ItemName = "";
            Description = "";
            StockLocation = "";
            Quantity = 0;
            Supplier = "";
            DateAdded = new DateTime();
            ExpiryDate = new DateTime();
        }

        public Stock(int stockId, string itemName, string description, string stockLocation, int quantity, string supplier, DateTime dateAdded, DateTime expiryDate) 
        {
            StockId = stockId;
            ItemName = itemName;
            Description = description;
            StockLocation = stockLocation;
            Quantity = quantity;
            Supplier = supplier;
            DateAdded = dateAdded;
            ExpiryDate = expiryDate;
        }
    }
}
