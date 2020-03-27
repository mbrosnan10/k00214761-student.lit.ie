using BusinessEntities;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IBarSaleItemModel : IModel<IBarSaleItem, int>
    {
        List<IBarSaleItem> GetByBarSaleId(int id);
        bool DeleteBy(int barSaleId, int menuItemId);
    }
}