namespace BusinessEntities
{
    public class MovingStockItem : IMovingStockItem
    {
        public int MovingStockItemId { get; set; }
        public int MovingStockId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }

        public MovingStockItem()
        {
        }

        public MovingStockItem(int movingStockItemId, int movingStockId, int stockId, int quantity)
        {
            MovingStockItemId = movingStockItemId;
            MovingStockId = movingStockId;
            StockId = stockId;
            Quantity = quantity;
        }
    }
}