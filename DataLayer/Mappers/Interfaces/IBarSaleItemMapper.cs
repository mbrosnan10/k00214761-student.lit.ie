using BusinessEntities;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IBarSaleItemMapper : IMapper<IBarSaleItem, int>
    {
 
        bool ChargeToReservation(int barsaleid);
        bool DeleteByKey(int key);
        bool Insert(IBarSaleItem entity);

        List<IBarSaleItem> GetByBarSaleId(int barSaleId);

        bool DeleteBy(int barSaleId, int menuItemId);

    }
}