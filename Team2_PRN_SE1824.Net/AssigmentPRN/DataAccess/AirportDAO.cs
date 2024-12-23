using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AirportDAO
    {
        public static List<Airport> GetAirport(string IsDelete)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            if (IsDelete.Equals("no"))
            {
                return flightManagementDbContext.Airports
            .Where(a => a.Status == true)
            .OrderByDescending(a => a.Id) 
            .ToList();
            }
            else
            {
                return flightManagementDbContext.Airports.Where(a => a.Status == false).ToList();
            }
        }
        public static void InsertAirport(Airport airport)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airports.Add(airport);
            flightManagementDbContext.SaveChanges();
        }

        public static void DeleteAirport(Airport airport)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airports.Remove(airport);
            flightManagementDbContext.SaveChanges();
        }

        public static void UpdateAirport(Airport airport)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            flightManagementDbContext.Airports.Update(airport);
            flightManagementDbContext.SaveChanges();
        }
        public static Airport? GetAirportByID(int airportID)
        {
            FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            return flightManagementDbContext.Airports.Find(airportID);

        }

        public static List<Airport> GetAirportByInfoCode(string airportInfo, string IsDelete)
        {
            if (string.IsNullOrEmpty(airportInfo))
            {
                return GetAirport(IsDelete);
            }
            return GetAirport(IsDelete).Where(airport => airport.Code.Contains(airportInfo) && airport.Status == true).ToList();
        }

        public static List<Airport> GetAirportByInfoCountry(string airportInfo, string IsDelete)
        {
            if (string.IsNullOrEmpty(airportInfo))
            {
                return GetAirport(IsDelete);
            }
            return GetAirport(IsDelete)
        .Where(airport => airport.Country.Contains(airportInfo) && airport.Status == true)
        .ToList();
        }
        public static List<Airport> GetAirportByInfoCity(string airportInfo, string IsDelete)
        {
            if (string.IsNullOrEmpty(airportInfo))
            {
                return GetAirport(IsDelete);
            }
            return GetAirport(IsDelete).Where(airport => airport.City.Contains(airportInfo) && airport.Status == true).ToList();
        }
        public static List<Airport> GetAirportByInfoState(string airportInfo, string IsDelete)
        {
            if (string.IsNullOrEmpty(airportInfo))
            {
                return GetAirport(IsDelete);
            }
            return GetAirport(IsDelete).Where(airport => airport.State.Contains(airportInfo) && airport.Status == true).ToList();
        }
        public static List<Airport> GetAirportByInfoName(string airportInfo, string IsDelete)
        {
            if (string.IsNullOrEmpty(airportInfo))
            {
                return GetAirport(IsDelete);
            }
            return GetAirport(IsDelete).Where(airport => airport.Name.Contains(airportInfo) && airport.Status == true).ToList();
        }
    }
}
