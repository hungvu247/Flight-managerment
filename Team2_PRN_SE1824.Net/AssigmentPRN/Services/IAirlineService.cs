using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAirlineService
    {
        List<Airline> GetAirlines();
        void InsertAirline(Airline airline);
        void UpdateAirline(Airline airline);
        void DeleteAirline(Airline airline);
        Airline? GetAirlineById(int id);
        int GetLatestAirlineId();
        List<Airline> SearchAirlinesByNameActive(string name);
        List<Airline> SearchAirlinesByNameInactive(string name);
        List<Airline> GetActiveAirlines();
        List<Airline> GetInactiveAirlines();
    }
}
