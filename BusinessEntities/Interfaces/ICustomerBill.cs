namespace BusinessEntities
{
    public interface ICustomerBill
    {
        int CustomerBillId { get; set; }
        int ReservationId { get; set; }
        double AmountPaid { get; set; }
        bool IsPaid { get; set; }
    }
}