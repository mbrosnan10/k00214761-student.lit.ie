namespace BusinessEntities
{
    public class MovingStock : IMovingStock
    {
        public int MovingStockId { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }

        public MovingStock()
        {
        }

        public MovingStock(int movingStockId, string fromLocation, string toLocation)
        {
            MovingStockId = movingStockId;
            FromLocation = fromLocation;
            ToLocation = toLocation;
        }
    }
}