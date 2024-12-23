using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
namespace DataAccessLayer
{
    public class PassengerDAO
    {
        public static List<Passenger> GetPassengers()
        {
            FlightManagementDbContext flightContext = new FlightManagementDbContext();  
            return flightContext.Passengers.ToList();
        }
        public static void InsertPassenger(Passenger passenger)
        {
            FlightManagementDbContext flightContext = new FlightManagementDbContext();
            flightContext.Passengers.Add(passenger);
            flightContext.SaveChanges();
        }
        public static void UpdatePassenger(Passenger passenger)
        {
            FlightManagementDbContext flightContext = new FlightManagementDbContext();
            flightContext.Passengers.Update(passenger);
            flightContext.SaveChanges();
        }
        public static void DeletePassenger(Passenger passenger)
        {
            FlightManagementDbContext flightContext = new FlightManagementDbContext();
            flightContext.Passengers.Remove(passenger);
            flightContext.SaveChanges();
        }
        public static Passenger? GetPassengerById(int id)
        {
            FlightManagementDbContext flightContext = new FlightManagementDbContext();
            return flightContext.Passengers.Find(id);
        }
    }
}
