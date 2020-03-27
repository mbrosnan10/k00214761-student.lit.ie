using BusinessEntities;

namespace DataLayer
{
    public interface IStockMapper : IMapper<IStock, int>
    {
        bool UpdateQuantity(int stockId, int newQuantity);
    }
}