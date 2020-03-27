using BusinessEntities;
using DataLayer;
using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class ReservationModel : IReservationModel
    {
        private static IReservationModel instance;
        private static readonly object padlock = new object();

        private IReservationMapper reservationMapper;

        public List<IReservation> EntityList { get; private set; }

        #region Singleton stuff

        private ReservationModel(IReservationMapper reservationMapper)
        {
            this.reservationMapper = reservationMapper;
            EntityList = reservationMapper.GetAll();
        }

        public static IReservationModel GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IReservationMapper reservationMapper = ReservationMapper.GetInstance();
                    instance = new ReservationModel(reservationMapper);
                }
                return instance;
            }
        }

        public static IReservationModel GetInstance(IReservationMapper reservationMapper)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ReservationModel(reservationMapper);
                }
                return instance;
            }
        }

        #endregion

        #region Public methods

        public bool Create(IReservation reservation)
        {
            return reservationMapper.Insert(reservation);
        }
      public  IReservation GetSuggestedPrice(decimal pricePerNight)
        {
            return reservationMapper.GetSuggestedPrice(pricePerNight);
        }

        public IReservation GetByKey(int reservationID)
        {
            return reservationMapper.GetByKey(reservationID);
        }

        public List<IReservation> GetAll()
        {
            return reservationMapper.GetAll();
        }

        public List<IReservation> GetByCheckInBetweenDates(DateTime from, DateTime to)
        {
            return reservationMapper.GetByCheckInBetweenDates(from, to);
        }

        public bool Update(IReservation entity)
        {
            return reservationMapper.Update(entity);
        }

        public bool DeleteByKey(int key)
        {
            return reservationMapper.DeleteByKey(key);
        }

        #endregion
    }
}