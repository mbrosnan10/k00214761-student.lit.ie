namespace BusinessEntities
{
    public class CustomerBillItem : ICustomerBillItem
    {
        public int CustomerBillItemId { get; set; }
        public int CustomerBillId { get; set; }
        public int? BarSaleId { get; set; }
        public double ItemPrice { get; set; }

        public CustomerBillItem()
        {
        }

        public CustomerBillItem(int customerBillItemId, int customerBillId, int? barSaleId, double itemPrice)
        {
            CustomerBillItemId = customerBillItemId;
            CustomerBillId = customerBillId;
            BarSaleId = barSaleId;
            ItemPrice = itemPrice;
        }
    }
}