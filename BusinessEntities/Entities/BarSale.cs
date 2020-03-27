using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class BarSale : IBarSale
    {
        public int BarSaleId { get; set; }
    public int? EmployeeId { get; set; }
        public DateTime DateOfSale { get; set; }
        public decimal AmountPaid { get; set; }
        public bool IsPaid { get; set; }
        

        public BarSale()
        {
        }
        public BarSale(int barSaleId)
        {
            BarSaleId = barSaleId;
           EmployeeId = 0;
            DateOfSale = new DateTime();
            AmountPaid = 0;
            IsPaid = false;

        }

        public BarSale(int barSaleId, int? employeeId, DateTime dateOfSale, decimal amountPaid, bool isPaid)
        {
            BarSaleId = barSaleId;
          EmployeeId = employeeId;
            DateOfSale = dateOfSale;
            AmountPaid = amountPaid;
            IsPaid = isPaid;
        }
   


    }
}
