using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class StockLocation : IStockLocation
    {

        public string StockLocationName { get; set; }

        public StockLocation()
        {
        }

        public StockLocation(string stockLocation)
        {
            this.StockLocationName = stockLocation;
        }
    }
}
