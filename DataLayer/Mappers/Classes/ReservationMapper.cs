using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public partial class ReservationMapper : IReservationMapper
    {
        private static readonly object padlock = new object();

        // Stores the SqlParamter object type for each field in the Employee table, this makes the class more maintainable.
        private static IDictionary<string, SqlParameter> reservationSqlParams = new Dictionary<string, SqlParameter>();


        private static IReservationMapper instance;
        private IDatabase db;

        #region Singleton stuff

        private ReservationMapper(IDatabase db)
        {
            this.db = db;
            reservationSqlParams["ReservationId"] = new SqlParameter("@ReservationId", SqlDbType.Int);
            reservationSqlParams["RoomType"] = new SqlParameter("@RoomType", SqlDbType.VarChar, 256);
            reservationSqlParams["RoomId"] = new SqlParameter("@RoomId", SqlDbType.Int) { IsNullable = true };
            reservationSqlParams["CheckInDate"] = new SqlParameter("@CheckInDate", SqlDbType.DateTime);
            reservationSqlParams["CheckOutDate"] = new SqlParameter("@CheckOutDate", SqlDbType.DateTime);
            reservationSqlParams["PricePerNight"] = new SqlParameter("@PricePerNight", SqlDbType.Decimal) { Precision = 18 };
            reservationSqlParams["GuestFirstName"] = new SqlParameter("@GuestFirstName", SqlDbType.VarChar, 256);
            reservationSqlParams["GuestLastName"] = new SqlParameter("@GuestLastName", SqlDbType.VarChar, 256);
            reservationSqlParams["GuestEmail"] = new SqlParameter("@GuestEmail", SqlDbType.VarChar, 256);
            reservationSqlParams["GuestPhoneNumber"] = new SqlParameter("@GuestPhoneNumber", SqlDbType.VarChar, 256);
            reservationSqlParams["NumberOfGuests"] = new SqlParameter("@NumberOfGuests", SqlDbType.Int);
            reservationSqlParams["Deposit"] = new SqlParameter("@Deposit", SqlDbType.Decimal) { Precision = 18 };
            reservationSqlParams["Smoking"] = new SqlParameter("@SmokingNeeded", SqlDbType.TinyInt);
            reservationSqlParams["CotNeeded"] = new SqlParameter("@CotNeeded", SqlDbType.TinyInt);
            reservationSqlParams["IsCancelled"] = new SqlParameter("@IsCancelled", SqlDbType.TinyInt);
            reservationSqlParams["CheckedOut"] = new SqlParameter("@CheckedOut", SqlDbType.TinyInt);
        }

        public static IReservationMapper GetInstance()
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    IDatabase database = Database.GetInstance();
                    instance = new ReservationMapper(database);
                }
                return instance;
            }
        }

        public static IReservationMapper GetInstance(IDatabase database)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ReservationMapper(database);
                }
                return instance;
            }
        }

        #endregion

        #region Create

        public bool Insert(IReservation reservation)
        {

            string sql = "INSERT INTO Reservation " +
                         "(RoomType, RoomId, CheckInDate, CheckOutDate, PricePerNight, GuestFirstName, GuestLastName, GuestEmail, GuestPhoneNumber,NumberOfGuests, Deposit, SmokingNeeded, CotNeeded, CheckedOut ) " +
                         "VALUES " +
                         "(@RoomType, @RoomId, @CheckInDate, @CheckOutDate, @PricePerNight, @GuestFirstName, @GuestLastName, @GuestEmail, @GuestPhoneNumber,@NumberOfGuests, @Deposit, @SmokingNeeded, @CotNeeded, @CheckedOut )";

            SqlParameter[] sqlParams = SetAllSqlParameters(reservation);

            return db.ExecuteInsert(sql, sqlParams);

        }

        #endregion

        #region Read

        public List<IReservation> GetAll()
        {

            string sql = "SELECT ReservationId, RoomType, RoomId, CheckInDate, CheckOutDate, PricePerNight, GuestFirstName, GuestLastName, GuestEmail, GuestPhoneNumber,NumberOfGuests, Deposit, SmokingNeeded, CotNeeded, IsCancelled, CheckedOut " +
                          "FROM Reservation";
            return db.ExecuteSelectMultiple<IReservation>(sql, ReaderRowToReservation);


        }

        public IReservation GetByKey(int id)
        {

            string sql = "SELECT ReservationId, RoomType, RoomId, CheckInDate, CheckOutDate, PricePerNight, GuestFirstName, GuestLastName, GuestEmail, GuestPhoneNumber,NumberOfGuests, Deposit, SmokingNeeded, CotNeeded, IsCancelled, CheckedOut " +
                          "FROM Reservation " +
                          "WHERE ReservationId = @ReservationId";
            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(reservationSqlParams["ReservationId"], id)
            };

            return db.ExecuteSelectSingle<IReservation>(sql, sqlParams, ReaderRowToReservation);

        }

        public List<IReservation> GetByCheckInBetweenDates(DateTime from, DateTime to)
        {
            string sql = "SELECT ReservationId, RoomType, RoomId, CheckInDate, CheckOutDate, PricePerNight, GuestFirstName, GuestLastName, GuestEmail, GuestPhoneNumber,NumberOfGuests, Deposit, SmokingNeeded, CotNeeded, IsCancelled, CheckedOut " +
                         "FROM Reservation " +
                         "WHERE CheckInDate >= @from AND CheckInDate <= @to";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(new SqlParameter("@from", SqlDbType.DateTime), from),
                MapperUtility.SqlParameterWithValue(new SqlParameter("@to", SqlDbType.DateTime), to),
            };

            return db.ExecuteSelectMultiple<IReservation>(sql, sqlParams, ReaderRowToReservation);
        }

        #endregion

        #region Update

        public bool Update(IReservation reservation)
        {

            string sql = "UPDATE Reservation " +
                          "SET " +
                          "RoomType = @RoomType, RoomId = @RoomId, CheckInDate = @CheckInDate, CheckOutDate = @CheckOutDate, PricePerNight = @PricePerNight, " +
                          "GuestFirstName = @GuestFirstName, GuestLastName = @GuestLastName, GuestEmail = @GuestEmail, " +
                          "GuestPhoneNumber = @GuestPhoneNumber, NumberOfGuests = @NumberOfGuests, " +
                          "Deposit = @Deposit, SmokingNeeded = @SmokingNeeded, CotNeeded = @CotNeeded, IsCancelled = @IsCancelled, CheckedOut = @CheckedOut " +
                          "WHERE ReservationId = @ReservationId";

            SqlParameter[] sqlParams = SetAllSqlParameters(reservation);
            return db.ExecuteUpdate(sql, sqlParams);

        }

        #endregion

        #region Delete

        public bool DeleteByKey(int id)
        {
            //sql query to delete from db
            string sql = "DELETE FROM Reservation WHERE ReservationId = @ReservationId";

            SqlParameter[] sqlParams =
            {
                MapperUtility.SqlParameterWithValue(reservationSqlParams["ReservationId"], id)
            };

            return db.ExecuteDelete(sql, sqlParams);
        }

        #endregion

        #region Private Methods
        private static IReservation ReaderRowToReservation(SqlDataReader reader)
        {
            int id = (int)reader["ReservationId"];
            string roomType = (string)reader["RoomType"];
            int? roomId = (reader["RoomId"] == DBNull.Value) ? null : (int?)reader["RoomId"];
            DateTime checkInDate = (DateTime)reader["CheckInDate"];
            DateTime checkOutDate = (DateTime)reader["CheckOutDate"];
            decimal pricePerNight = (decimal)reader["PricePerNight"];
            string guestFirstName = (string)reader["GuestFirstName"];
            string guestLastName = (string)reader["GuestLastName"];
            string guestEmail = (string)reader["GuestEmail"];
            string guestPhoneNumber = (string)reader["GuestPhoneNumber"];
            int numberOfGuests = (int)reader["NumberOfGuests"];
            decimal deposit = (decimal)reader["Deposit"];
            bool smoking = (byte)reader["SmokingNeeded"] == 0 ? false : true;
            bool cotNeeded = (byte)reader["CotNeeded"] == 0 ? false : true;
            bool isCancelled = (byte)reader["IsCancelled"] == 0 ? false : true;
            bool CheckedOut = (byte)reader["CheckedOut"] == 0 ? false : true;

            return new Reservation(id, roomType, roomId, checkInDate, checkOutDate, pricePerNight, guestFirstName, guestLastName, guestEmail, guestPhoneNumber, numberOfGuests, deposit, smoking, cotNeeded, isCancelled, CheckedOut);
        }

        private static SqlParameter[] SetAllSqlParameters(IReservation reservation)
        {

            return new SqlParameter[] {
            MapperUtility.SqlParameterWithValue(reservationSqlParams["ReservationId"],reservation.ReservationId),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["RoomType"],reservation.RoomType),
            MapperUtility.SqlParameterWithNullableValue(reservationSqlParams["RoomId"], reservation.RoomId),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["CheckInDate"],reservation.CheckInDate),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["CheckOutDate"],reservation.CheckOutDate),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["PricePerNight"],reservation.PricePerNight),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["GuestFirstName"],reservation.GuestFirstName),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["GuestLastName"],reservation.GuestLastName),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["GuestEmail"],reservation.GuestEmail),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["GuestPhoneNumber"],reservation.GuestPhoneNumber),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["NumberOfGuests"],reservation.NumberOfGuests),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["Deposit"],reservation.Deposit),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["Smoking"],reservation.Smoking),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["CotNeeded"],reservation.CotNeeded),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["IsCancelled"],reservation.IsCancelled),
            MapperUtility.SqlParameterWithValue(reservationSqlParams["CheckedOut"],reservation.CheckedOut)
            };
        }

        public IReservation GetSuggestedPrice(decimal pricePerNight)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
