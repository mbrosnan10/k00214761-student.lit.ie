using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Room : IRoom
    {
        public int RoomId { get; private set; }
        public string RoomType { get; set; }
        public string CleaningStatus { get; set; }
        public int MaxGuests { get; set; }
        public bool IsUsable { get; set; }
        public bool AllowSmoking { get; set; }
        public bool HasCot { get; set; }

        public Room()
        {
        }

        public Room(int roomId, string roomType, int maxGuests, string cleaningStatus, 
            bool isUsable, bool allowSmoking, bool cotAvailable)
        {
            RoomId = roomId;
            RoomType = roomType;
            MaxGuests = maxGuests;
            CleaningStatus = cleaningStatus;
            IsUsable = isUsable;
            AllowSmoking = allowSmoking;
            HasCot = cotAvailable;
        }
    }
}
