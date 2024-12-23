using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFlightRepository
    {
        List<Flight> GetFlights();
        void InsertFlight(Flight flight);
        void DeleteFlight(Flight flight);
        void UpdateFlight(Flight flight);
        Flight? GetFlightByID(int flightID);
    }
}
