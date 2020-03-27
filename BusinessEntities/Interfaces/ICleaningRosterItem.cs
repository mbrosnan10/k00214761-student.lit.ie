using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface ICleaningRosterItem
    {
        int CleaningRosterItemId { get; set; }
        int CleanintRosterId { get; set; }
        int RoomId { get; set; }
        int EmployeeId { get; set; }
        DateTime TimeAdded { get; set; }
        DateTime TimeCompleted { get; set; }
        bool IsComplete { get; set; }
    }
}
