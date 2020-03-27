namespace BusinessEntities
{
    public interface ICustomerBillItem
    {
        int CustomerBillItemId { get; set; }
        int CustomerBillId { get; set; }
        int? BarSaleId { get; set; }
        double ItemPrice { get; set; }
    }
}