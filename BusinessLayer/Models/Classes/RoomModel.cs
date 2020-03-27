using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    public partial class RoomModel : IRoomModel
    {
        private static IRoomModel instance;
        private static readonly object padlock = new object();

        private IRoomMapper roomMapper;
        private IRoomTypeMapper roomTypeMapper;
        private IReservationMapper reservationMapper;
        private IDatabase db;

        public List<IRoom> EntityList { get; private set; }

        #region Singleton stuff

        private RoomModel(IDatabase db, IRoomMapper roomMapper, IRoomTypeMapper roomTypeMapper, IReservationMapper reservationMapper)
        {
            this.db = db;
            this.roomMapper = roomMapper;
            this.roomTypeMapper = roomTypeMapper;
            this.reservationMapper = reservationMapper;
            EntityList = roomMapper.GetAll();
        }

        public static IRoomModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase db = Database.GetInstance();
                    IRoomMapper roomMapper = RoomMapper.GetInstance();
                    IRoomTypeMapper roomTypeMapper = RoomTypeMapper.GetInstance();
                    IReservationMapper reservationMapper = ReservationMapper.GetInstance();
                    instance = new RoomModel(db, roomMapper, roomTypeMapper, reservationMapper);
                }
                return instance;
            }
        }

        public static IRoomModel GetInstance(IDatabase db, IRoomMapper roomMapper, IRoomTypeMapper roomTypeMapper, IReservationMapper reservationMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new RoomModel(db, roomMapper, roomTypeMapper, reservationMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IRoom room)
        {
            bool result = roomMapper.Insert(room);
            EntityList = roomMapper.GetAll();
            return result;
        }

        public bool DeleteByKey(int key)
        {
            throw new NotImplementedException();
        }

        public bool Update(IRoom room)
        {
            bool result = roomMapper.Update(room);
            EntityList = roomMapper.GetAll();
            return result;
        }

        public List<IRoom> GetAll()
        {
            throw new NotImplementedException();
        }

        public IRoom GetByKey(int roomId)
        {
            return roomMapper.GetByKey(roomId);
        }

        public Dictionary<string, int> GetRoomAvailability(DateTime from, DateTime to)
        {
            Dictionary<string, int> roomCountByType = RoomCountByType();
            Dictionary<string, int> reservationCountByType = ReservationCountByType(from, to);
            Dictionary<string, int> availabilityByType = new Dictionary<string, int>();

            foreach (KeyValuePair<string, int> roomCountType in roomCountByType)
            {
                availabilityByType.Add(roomCountType.Key, roomCountType.Value - reservationCountByType[roomCountType.Key]);
            }

            return availabilityByType;
        }
        #endregion

        #region Private methods
        private Dictionary<string, int> RoomCountByType()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result = roomMapper.GetAll()
                .GroupBy(room => room.RoomType)
                .ToDictionary(group => group.Key, group => group.Count());

            foreach (IRoomType roomType in roomTypeMapper.GetAll())
            {
                if (result.ContainsKey(roomType.RoomTypeName) == false)
                {
                    result.Add(roomType.RoomTypeName, 0);
                }
            }

            result.Remove("none");

            return result;
        }

        private Dictionary<string, int> ReservationCountByType(DateTime from, DateTime to)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            result = reservationMapper.GetAll()
                .Where(reservation =>
                    (reservation.CheckOutDate >= from && reservation.CheckOutDate < to) ||
                    (reservation.CheckInDate > from && reservation.CheckInDate <= to) ||
                    (reservation.CheckInDate < from && reservation.CheckOutDate > to) ||
                    (reservation.CheckInDate > from && reservation.CheckOutDate < to)
                )
                .GroupBy(reservation => reservation.RoomType)
                .ToDictionary(group => group.Key, group => group.Count());

            foreach (IRoomType roomType in roomTypeMapper.GetAll())
            {
                if (result.ContainsKey(roomType.RoomTypeName) == false)
                {
                    result.Add(roomType.RoomTypeName, 0);
                }
            }

            result.Remove("none");

            return result;
        }
        #endregion
    }
}