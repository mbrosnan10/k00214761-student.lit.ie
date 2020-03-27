using BusinessEntities;

namespace DataLayer
{
    public interface ICustomerBillItemMapper : IMapper<ICustomerBillItem, int>
    {
        ICustomerBillItem GetByCustomerBillId(int customerBillId);
    }
}