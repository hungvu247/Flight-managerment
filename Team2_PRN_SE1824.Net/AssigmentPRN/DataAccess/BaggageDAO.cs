using DataAccess.BussinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BaggageDAO
    {

        private static  FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();

        public static List<Baggage> GetAllBagges()
        {
            return flightManagementDbContext.Baggages.ToList();
        }

        public static List<dynamic> GetAllBookingHasNotBaggage()
        {
            var bookings = flightManagementDbContext.Bookings
                .Include(b => b.Passenger)
                .Include(b => b.BookingPlatform)
               .Include(b => b.Flight)
            .ThenInclude(f => f.DepartingAirportNavigation)  // Include DepartingAirport information
        .Include(b => b.Flight)
            .ThenInclude(f => f.ArrivingAirportNavigation)
            .GroupJoin(flightManagementDbContext.Baggages, // Join with Baggage table
               b => b.Id, // BookingId in Bookings table
               g => g.BookingId, // BookingId in Baggage table
               (b, g) => new { Booking = b, Baggages = g }) // GroupJoin result
    .Where(bg => !bg.Baggages.Any())
                .Select(b => new
                {
                    
                    BookingId = b.Booking.Id,
                    PassengerName = b.Booking.Passenger.FirstName + " " + b.Booking.Passenger.LastName,
                    Email = b.Booking.Passenger.Email,
                    PlatformName = b.Booking.BookingPlatform.Name,
                    Dob = b.Booking.Passenger.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                    BookingTime = b.Booking.BookingTime.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    FlightCode = b.Booking.Flight.Id, // Assuming FlightCode is in Flight table
                    DepartingAirport = b.Booking.Flight.DepartingAirportNavigation != null ? b.Booking.Flight.DepartingAirportNavigation.Name : "Unknown",  // Get Departing Airport Name
                    ArrivingAirport = b.Booking.Flight.ArrivingAirportNavigation != null ? b.Booking.Flight.ArrivingAirportNavigation.Name : "Unknown",  // Get Arriving Airport Name
                    DepartingTime = b.Booking.Flight.DepartureTime.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    ArrivingTime = b.Booking.Flight.ArrivalTime.Value.ToString("dd-MM-yyyy HH:mm:ss")
                })
            .ToList<dynamic>();

            return bookings;
        }


        public static List<dynamic> GetAllBookingHasBaggage()
        {
            var bookings = flightManagementDbContext.Bookings
                .Include(b => b.Passenger)
                .Include(b => b.BookingPlatform)
                .Include(b => b.Baggages)
                .Include(b => b.Flight)
                    .ThenInclude(f => f.DepartingAirportNavigation)  // Include DepartingAirport information
                .Include(b => b.Flight)
                    .ThenInclude(f => f.ArrivingAirportNavigation)
                .Where(b => b.Baggages.Any(bg => bg.Status == true)) // Filter for bookings that have at least one baggage
                .Select(b => new
                {
                    Gid = b.Baggages.FirstOrDefault().Id,
                    BookingId = b.Id,
                    PassengerName = b.Passenger.FirstName + " " + b.Passenger.LastName,
                    Email = b.Passenger.Email,
                    PlatformName = b.BookingPlatform.Name,
                    Dob = b.Passenger.DateOfBirth.Value.ToString("dd-MM-yyyy"),
                    // SelectWeight gets the weight of the first baggage if there are multiple
                    Weight = b.Baggages.FirstOrDefault().WeightInKg ?? 0, // Provide a default value if no baggage
                    BookingTime = b.BookingTime.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    FlightCode = b.Flight.Id, // Assuming FlightCode is in Flight table
                    DepartingAirport = b.Flight.DepartingAirportNavigation != null ? b.Flight.DepartingAirportNavigation.Name : "Unknown",  // Get Departing Airport Name
                    ArrivingAirport = b.Flight.ArrivingAirportNavigation != null ? b.Flight.ArrivingAirportNavigation.Name : "Unknown",  // Get Arriving Airport Name
                    DepartingTime = b.Flight.DepartureTime.Value.ToString("dd-MM-yyyy HH:mm:ss"),
                    ArrivingTime = b.Flight.ArrivalTime.Value.ToString("dd-MM-yyyy HH:mm:ss")
                })
                .ToList<dynamic>();

            return bookings;
        }


        public static List<Airport> LoadAllAirport()
        {
            return flightManagementDbContext.Airports.ToList();
        }


        public static List<dynamic> GetAllByCbBookingPlatform(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time FROM booking bo LEFT JOIN baggage ba " +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id " +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE bp.id = @bpid AND ba.booking_id IS NULL ";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName")); 
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport")); 
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbBookingPlatformUpdate(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time, ba.weight_in_kg, ba.id as gid FROM booking bo RIGHT JOIN baggage ba " +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id " +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE bp.id = @bpid AND ba.status = 1  ";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.Weight = reader.GetDecimal(reader.GetOrdinal("weight_in_kg"));
                                booking.Gid = reader.GetInt32(reader.GetOrdinal("gid"));
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }



        public static List<dynamic> GetAllByCbDepartingAirport(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time FROM booking bo LEFT JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE dep_airport.id = @bpid AND ba.booking_id IS NULL";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbDepartingAirportUpdate(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time, ba.weight_in_kg, ba.id as gid FROM booking bo RIGHT JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE dep_airport.id = @bpid AND ba.status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.Weight = reader.GetDecimal(reader.GetOrdinal("weight_in_kg"));
                                booking.Gid = reader.GetInt32(reader.GetOrdinal("gid"));
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbArrivingAirport(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = " SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time FROM booking bo Left JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE arr_airport.id  = @bpid AND ba.booking_id IS NULL";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbArrivingAirportUpdate(string bookingPlatform)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = " SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time, ba.weight_in_kg, ba.id as gid FROM booking bo Right JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE arr_airport.id  = @bpid AND ba.status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@bpid", bookingPlatform);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.Weight = reader.GetDecimal(reader.GetOrdinal("weight_in_kg"));
                                booking.Gid = reader.GetInt32(reader.GetOrdinal("gid"));
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbDepartingAirportAndArrivingAirport(string departingAirport, string arrivingAirport)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time FROM booking bo Left JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE dep_airport.id  = @did AND   arr_airport.id = @aid AND ba.booking_id IS NULL";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@did", departingAirport);
                        cmd.Parameters.AddWithValue("@aid", arrivingAirport);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllByCbDepartingAirportAndArrivingAirportUpdate(string departingAirport, string arrivingAirport)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = "SELECT bo.id,bo.flight_id,pas.first_name + ' ' + pas.last_name as PassengerName ,pas.email," +
                " pas.date_of_birth,bp.name as BookingPlaformName ,dep_airport.name as DepartingAirport,arr_airport.name as ArrivingAirport, " +
                " fl.departure_time, fl.arrival_time, ba.weight_in_kg , ba.id as gid FROM booking bo RIGHT JOIN baggage ba" +
                " ON ba.booking_id = bo.id JOIN bookingPlatform bp\r\nON bp.id = bo.booking_platform_id" +
                " JOIN passenger pas\r\nON pas.id = bo.passenger_id JOIN flight fl \r\nON fl.id = bo.flight_id  FULL JOIN " +
                "   airport dep_airport ON dep_airport.id = fl.departing_airport \r\nJOIN airport arr_airport ON arr_airport.id = fl.arriving_airport\r\nWHERE dep_airport.id  = @did AND   arr_airport.id = @aid AND ba.status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@did", departingAirport);
                        cmd.Parameters.AddWithValue("@aid", arrivingAirport);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.Weight = reader.GetDecimal(reader.GetOrdinal("weight_in_kg"));
                                booking.Gid = reader.GetInt32(reader.GetOrdinal("gid"));
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }



        public static List<dynamic> GetAllBySearch(string keyword)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = @"
    SELECT 
        bo.id,
        bo.flight_id,
        pas.first_name + ' ' + pas.last_name AS PassengerName,
        pas.email,
        pas.date_of_birth,
        bp.name AS BookingPlaformName,
        dep_airport.name AS DepartingAirport,
        arr_airport.name AS ArrivingAirport,
        fl.departure_time,
        fl.arrival_time
    FROM 
        booking bo
    LEFT JOIN 
        baggage ba ON ba.booking_id = bo.id
    JOIN 
        bookingPlatform bp ON bp.id = bo.booking_platform_id
    JOIN 
        passenger pas ON pas.id = bo.passenger_id
    JOIN 
        flight fl ON fl.id = bo.flight_id
    FULL JOIN 
        airport dep_airport ON dep_airport.id = fl.departing_airport
    JOIN 
        airport arr_airport ON arr_airport.id = fl.arriving_airport
    WHERE 
        (@searchTerm = '' 
        OR pas.first_name LIKE '%' + @searchTerm + '%' 
        OR pas.last_name LIKE '%' + @searchTerm + '%'
        OR bp.name LIKE '%' + @searchTerm + '%'
        OR CONVERT(varchar, bo.flight_id) LIKE '%' + @searchTerm + '%') AND ba.booking_id IS NULL";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", keyword);
                        

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        public static List<dynamic> GetAllBySearchUpdate(string keyword)
        {
            List<dynamic> bookingList = new List<dynamic>();
            string sql = @"
    SELECT 
        bo.id,
        bo.flight_id,
        pas.first_name + ' ' + pas.last_name AS PassengerName,
        pas.email,
        pas.date_of_birth,
        bp.name AS BookingPlaformName,
        dep_airport.name AS DepartingAirport,
        arr_airport.name AS ArrivingAirport,
        fl.departure_time,
        fl.arrival_time, ba.weight_in_kg, ba.id as gid
    FROM 
        booking bo
    RIGHT JOIN 
        baggage ba ON ba.booking_id = bo.id
    JOIN 
        bookingPlatform bp ON bp.id = bo.booking_platform_id
    JOIN 
        passenger pas ON pas.id = bo.passenger_id
    JOIN 
        flight fl ON fl.id = bo.flight_id
    FULL JOIN 
        airport dep_airport ON dep_airport.id = fl.departing_airport
    JOIN 
        airport arr_airport ON arr_airport.id = fl.arriving_airport
    WHERE 
        (@searchTerm = '' 
        OR pas.first_name LIKE '%' + @searchTerm + '%' 
        OR pas.last_name LIKE '%' + @searchTerm + '%'
        OR bp.name LIKE '%' + @searchTerm + '%'
        OR CONVERT(varchar, bo.flight_id) LIKE '%' + @searchTerm + '%') AND ba.status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", keyword);


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dynamic booking = new ExpandoObject();
                                booking.BookingId = reader.GetInt32(reader.GetOrdinal("id"));
                                booking.FlightCode = reader.GetInt32(reader.GetOrdinal("flight_id"));
                                booking.PassengerName = reader.GetString(reader.GetOrdinal("PassengerName"));
                                booking.Email = reader.GetString(reader.GetOrdinal("email"));
                                booking.Dob = reader.GetDateTime(reader.GetOrdinal("date_of_birth")).ToString("dd-MM-yyyy");
                                booking.PlatformName = reader.GetString(reader.GetOrdinal("BookingPlaformName"));
                                booking.DepartingAirport = reader.GetString(reader.GetOrdinal("DepartingAirport"));
                                booking.ArrivingAirport = reader.GetString(reader.GetOrdinal("ArrivingAirport"));
                                booking.DepartingTime = reader.GetDateTime(reader.GetOrdinal("departure_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.ArrivingTime = reader.GetDateTime(reader.GetOrdinal("arrival_time")).ToString("dd-MM-yyyy HH:mm:ss");
                                booking.Weight = reader.GetDecimal(reader.GetOrdinal("weight_in_kg"));
                                booking.Gid = reader.GetInt32(reader.GetOrdinal("gid"));
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }


        public static List<Booking> GetAllBookingHasNotBaggage2()
        {
            List<Booking> bookingList = new List<Booking>();
            string sql = "SELECT bo.* FROM baggage ba RIGHT JOIN booking bo \r\nON ba.booking_id = bo.id";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Booking booking = new Booking
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    FlightId = reader.GetInt32(reader.GetOrdinal("flight_id")),
                                    PassengerId = reader.GetInt32(reader.GetOrdinal("passenger_id")),
                                    BookingPlatformId = reader.GetInt32(reader.GetOrdinal("booking_platform_id")),
                                    BookingTime = reader.GetDateTime(reader.GetOrdinal("booking_time")),
                                    Status = reader.GetBoolean(reader.GetOrdinal("status"))
                                    // Set other properties as needed
                                };
                                bookingList.Add(booking);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
            return bookingList;
        }

        

        public static Baggage GetBaggageById(string id)
        {
            Baggage? baggage = new Baggage();
            baggage = flightManagementDbContext.Baggages.Find(id);
            return baggage;
        }

        public static void InsertBagge(Baggage baggage)
        {
            flightManagementDbContext.Baggages.Add(baggage);
            flightManagementDbContext.SaveChanges();
        }

        public static void UpdateBagge(Baggage baggage)
        {
            flightManagementDbContext.Baggages.Update(baggage);
            flightManagementDbContext.SaveChanges();
        }

        public static void DeleteBagge(string id)
        {
            string sql = "UPDATE baggage SET status = 0 WHERE id = @id ";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }

        public static int GetCurrentID()
        {
            int currentId = 0;
            string sql = "SELECT MAX(id) as currentId FROM baggage";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentId = reader.GetInt32(reader.GetOrdinal("currentId"));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                // Xử lý ngoại lệ, ví dụ: throw e; để ngoại lệ được lan truyền lên để xử lý ở mức cao hơn.
            }

            return currentId;
        }

        public static string GetConnectionString(string connectionStringName = "DefaultConnectionStrings")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            return configuration.GetConnectionString(connectionStringName);
        }


        public static List<BookingPlatform> getBookingPlatforms()
        {
            return flightManagementDbContext.BookingPlatforms.Where(status => status.Status.Equals(true)).ToList();
            //return platforms;
        }




    }
}
