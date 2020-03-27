using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class RoomType : IRoomType
    {
        public string RoomTypeName { get; set; }

        public RoomType()
        {
        }

        public RoomType(string roomTypeName)
        {
            RoomTypeName = roomTypeName;
        }
    }
}
