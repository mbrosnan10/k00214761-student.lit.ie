namespace BusinessEntities
{
    public interface IMovingStock
    {
        string FromLocation { get; set; }
        int MovingStockId { get; set; }
        string ToLocation { get; set; }
    }
}