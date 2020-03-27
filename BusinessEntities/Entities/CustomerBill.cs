namespace BusinessEntities
{
    public class CustomerBill : ICustomerBill
    {
        public int CustomerBillId { get; set; }
        public int ReservationId { get; set; }
        public double AmountPaid { get; set; }
        public bool IsPaid { get; set; }

        public CustomerBill()
        {
        }

        public CustomerBill(int customerBiillId, int reservationId, double amountPaid, bool isPaid)
        {
            CustomerBillId = customerBiillId;
            ReservationId = reservationId;
            AmountPaid = amountPaid;
            IsPaid = isPaid;
        }
    }
}