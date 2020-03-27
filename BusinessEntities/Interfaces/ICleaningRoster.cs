using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface ICleaningRoster
    {
        int CleaningRosterId { get; set; }
        DateTime CleaningRosterDate { get; set; }
        bool IsActive { get; set; }

    }
}
