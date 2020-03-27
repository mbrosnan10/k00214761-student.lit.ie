using BusinessEntities;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IStockModel : IModel<IStock, int>
    {
        bool UpdateQuantity(int stockId, int newQuantity);
    }
}