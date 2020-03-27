using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CleaningStatus : ICleaningStatus
    {
        public string CleaningStatusName { get; set; }

        public CleaningStatus()
        {
        }

        public CleaningStatus(string cleaningStatusName)
        {
            CleaningStatusName = cleaningStatusName;
        }
    }
}
