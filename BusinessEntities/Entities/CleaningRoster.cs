
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CleaningRoster : ICleaningRoster
    {

        public int CleaningRosterId { get; set; }
        public DateTime CleaningRosterDate { get; set; }
        public bool IsActive { get; set; }

        public CleaningRoster()
        {
        }

        public CleaningRoster(int cleaningRosterId, DateTime cleaningRosterDate, bool isActive)
        {
            CleaningRosterId = cleaningRosterId;
            CleaningRosterDate = cleaningRosterDate;
            IsActive = isActive;
        }
    }
}
