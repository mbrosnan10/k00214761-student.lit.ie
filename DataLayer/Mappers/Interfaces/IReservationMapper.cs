using BusinessEntities;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public interface IReservationMapper : IMapper<IReservation, int>
    {
        List<IReservation> GetByCheckInBetweenDates(DateTime from, DateTime to);
        IReservation GetSuggestedPrice(decimal pricePerNight);
    }
}