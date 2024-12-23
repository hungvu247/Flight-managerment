using DataAccess;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FlightRepository : IFlightRepository
    {
        public void DeleteFlight(Flight flight)
        {
            FlightDAO.DeleteFlight(flight);
        }

        public Flight? GetFlightByID(int flightID)
        {
            return FlightDAO.GetFlightByID(flightID);
        }

        public List<Flight> GetFlights()
        {
            return FlightDAO.GetFlight();
        }

        public void InsertFlight(Flight flight)
        {
            FlightDAO.InsertFlight(flight);
        }

        public void UpdateFlight(Flight flight)
        {
            FlightDAO.UpdateFlight(flight);
        }
    }
}
