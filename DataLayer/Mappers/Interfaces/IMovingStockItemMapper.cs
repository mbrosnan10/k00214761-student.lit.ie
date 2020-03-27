using BusinessEntities;

namespace DataLayer
{
    public interface IMovingStockItemMapper : IMapper<IMovingStockItem, int>
    {
        IMovingStockItem GetByMovingStockId(int movingStockId);

        IMovingStockItem GetByStockId(int stockId);
    }
}