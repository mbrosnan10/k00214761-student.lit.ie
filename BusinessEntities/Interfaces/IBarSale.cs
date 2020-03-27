using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IBarSale
    {
        int BarSaleId { get; set; }
      int? EmployeeId { get; set; }
        DateTime DateOfSale { get; set; }
        decimal AmountPaid { get; set; }
        bool IsPaid { get; set; }
    }
}
