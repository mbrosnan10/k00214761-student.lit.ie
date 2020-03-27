using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Reservation : IReservation
    {
        public int ReservationId { get; set; }
        public string RoomType { get; set; }
        public int? RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal PricePerNight { get; set; }
        public string GuestFirstName { get; set; }
        public string GuestLastName { get; set; }
        public string GuestEmail { get; set; }
        public string GuestPhoneNumber { get; set; }
        public int NumberOfGuests { get; set; }
        public decimal Deposit { get; set; }
        public bool Smoking { get; set; }
        public bool CotNeeded { get; set; }
        public bool IsCancelled { get; set; }
        public bool CheckedOut { get; set; }

        public Reservation()
        {

        }

        public Reservation(int reservationID)
        {
            ReservationId = reservationID;
            RoomType = "";
            RoomId = 0;
            CheckInDate = new DateTime();
            CheckOutDate = new DateTime();
            PricePerNight = 0;
            GuestFirstName = "";
            GuestLastName = "";
            GuestEmail = "";
            GuestPhoneNumber = "";
            NumberOfGuests = 0;
            Deposit = 0;
            Smoking = false;
            CotNeeded = false;
            IsCancelled = false;
            CheckedOut = false;
        }

        public Reservation(int reservationId, string roomType, int? roomId, DateTime checkInDate, DateTime checkOutDate, 
            decimal pricePerNight, string guestFirstName, string guestLastName, string guestEmail, string guestPhoneNumber, 
            int numberOfGuests, decimal deposit, bool smoking, bool cotNeeded, bool isCancelled,bool CheckedOut)
            : this(reservationId)
        {
            RoomType = roomType;
            RoomId = roomId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            PricePerNight = pricePerNight;
            GuestFirstName = guestFirstName;
            GuestLastName = guestLastName;
            GuestEmail = guestEmail;
            GuestPhoneNumber = guestPhoneNumber;
            NumberOfGuests = numberOfGuests;
            Deposit = deposit;
            Smoking = smoking;
            CotNeeded = cotNeeded;
            IsCancelled = isCancelled;
            this.CheckedOut = CheckedOut;
        }

        public Reservation(int reservationID, string roomType, object p, DateTime checkInDate, DateTime checkOutDate, decimal pricePerNight) : this(reservationID)
        {
            RoomType = roomType;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            PricePerNight = pricePerNight;
        }
    }
}
