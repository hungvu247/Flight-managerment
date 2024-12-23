using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AirlineDAO
    {
        public static List<Airline> GetAirlines()
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            return flightManagementDbContext.Airlines.ToList();
        }

        public static void InsertAirline(Airline airline)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airlines.Add(airline);
            flightManagementDbContext.SaveChanges();
        }

        public static void DeleteAirline(Airline airline)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airlines.Remove(airline);
            flightManagementDbContext.SaveChanges();
        }

        public static void UpdateAirline(Airline airline)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airlines.Update(airline);
            flightManagementDbContext.SaveChanges();
        }

        public static Airline? GetAirlineByID(int airlineID)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            return flightManagementDbContext.Airlines.Find(airlineID);
        }
    }
}
