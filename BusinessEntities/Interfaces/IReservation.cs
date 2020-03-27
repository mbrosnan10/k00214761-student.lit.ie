using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public interface IReservation 
    {
        int ReservationId { get; set; }
        string RoomType { get; set; }
        int? RoomId { get; set; }
        DateTime CheckInDate { get; set; }
        DateTime CheckOutDate { get; set; }
        decimal PricePerNight { get; set; }
        string GuestFirstName { get; set; }
        string GuestLastName { get; set; }
        string GuestEmail { get; set; }
        string GuestPhoneNumber { get; set; }
        int NumberOfGuests { get; set; }
        decimal Deposit { get; set; }
        bool Smoking { get; set; }
        bool CotNeeded { get; set; }
        bool IsCancelled { get; set; }
        bool CheckedOut { get; set; }
    }
}
