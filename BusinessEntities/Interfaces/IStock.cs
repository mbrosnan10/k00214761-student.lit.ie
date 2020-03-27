using System;

namespace BusinessEntities
{
    public interface IStock
    {
        int StockId { get; set; }
        string ItemName { get; set; }
        string Description { get; set; }
        string StockLocation { get; set; }
        int Quantity { get; set; }
        string Supplier { get; set; }
        DateTime DateAdded { get; set; }
        DateTime ExpiryDate { get; set; }
    }
}