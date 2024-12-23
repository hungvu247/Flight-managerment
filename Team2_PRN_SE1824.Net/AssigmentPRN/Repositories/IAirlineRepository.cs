using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAirlineRepository
    {
        List<Airline> GetAirlines();
        void InsertAirline(Airline airline);
        void DeleteAirline(Airline airline);
        void UpdateAirline(Airline airline);
        Airline? GetAirlineByID(int airlineID);
    }
}
