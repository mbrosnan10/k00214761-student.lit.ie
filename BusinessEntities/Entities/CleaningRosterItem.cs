using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CleaningRosterItem : ICleaningRosterItem
    {
        public int CleaningRosterItemId { get; set; }
        public int CleanintRosterId { get; set; }
        public int RoomId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TimeAdded { get; set; }
        public DateTime TimeCompleted { get; set; }
        public bool IsComplete { get; set; }

        public CleaningRosterItem()
        {
        }

        public CleaningRosterItem(int cleaningRosterItemId, int cleanintRosterId, int roomId, int employeeId, DateTime timeAdded, DateTime timeCompleted, bool isComplete)
        {
            CleaningRosterItemId = cleaningRosterItemId;
            CleanintRosterId = cleanintRosterId;
            RoomId = roomId;
            EmployeeId = employeeId;
            TimeAdded = timeAdded;
            TimeCompleted = timeCompleted;
            IsComplete = isComplete;
        }
    }
}
