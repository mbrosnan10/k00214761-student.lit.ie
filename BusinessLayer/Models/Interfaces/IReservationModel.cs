using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;

namespace BusinessLayer
{
    public interface IReservationModel : IModel<IReservation, int>
    {
        List<IReservation> GetByCheckInBetweenDates(DateTime from, DateTime to);
    }
}
