using DataAccess;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        public void DeleteAirline(Airline airline)
        {
            AirlineDAO.DeleteAirline(airline);
        }

        public Airline? GetAirlineByID(int airlineID)
        {
            return AirlineDAO.GetAirlineByID(airlineID);
        }

        public List<Airline> GetAirlines()
        {
            return AirlineDAO.GetAirlines();
        }

        public void InsertAirline(Airline airline)
        {
            AirlineDAO.InsertAirline(airline);
        }

        public void UpdateAirline(Airline airline)
        {
            AirlineDAO.UpdateAirline(airline);
        }
    }
}
