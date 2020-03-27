using BusinessEntities;

namespace DataLayer
{
    public interface ICustomerBillMapper : IMapper<ICustomerBill, int>
    {
        ICustomerBill GetByReservationId(int reservationId);
    }
}