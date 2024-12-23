using DataAccess.BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FlightService : IFlightService
    {
        IFlightRepository flightRepository;
        public FlightService() {
            flightRepository = new FlightRepository();   
        }

        public void DeleteFlight(Flight flight)
        {
           flightRepository.DeleteFlight(flight);
        }

        public Flight? GetFlightByID(int flightID)
        {
           return flightRepository.GetFlightByID(flightID);
        }

        public List<Flight> GetFlights()
        {
           return flightRepository.GetFlights();
        }

        public void InsertFlight(Flight flight)
        {
            flightRepository.InsertFlight(flight);
        }

        public void UpdateFlight(Flight flight)
        {
            flightRepository.UpdateFlight(flight);
        }
    }
}
