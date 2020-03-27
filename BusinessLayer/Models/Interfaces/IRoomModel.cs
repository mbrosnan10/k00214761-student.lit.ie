using BusinessEntities;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public interface IRoomModel : IModel<IRoom, int>
    {
        Dictionary<string, int> GetRoomAvailability(DateTime from, DateTime to);
    }
}