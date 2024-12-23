using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
using Microsoft.EntityFrameworkCore;
namespace DataAccess
{
    public class FlightDAO
    {
        public static List<Flight> GetFlight()
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            return flightManagementDbContext.Flights.Include(f => f.Airline).Include( f => f.ArrivingAirportNavigation).Include(f => f.DepartingAirportNavigation).ToList();
        }

        public static void InsertFlight(Flight flight)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Flights.Add(flight);
            flightManagementDbContext.SaveChanges();
        }

        public static void DeleteFlight(Flight flight)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Flights.Remove(flight);
            flightManagementDbContext.SaveChanges();
        }

        public static void UpdateFlight(Flight flight)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Flights.Update(flight);
            flightManagementDbContext.SaveChanges();
        }

        public static Flight? GetFlightByID(int flightID)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            return flightManagementDbContext.Flights.Find(flightID);
        }
    }
}
