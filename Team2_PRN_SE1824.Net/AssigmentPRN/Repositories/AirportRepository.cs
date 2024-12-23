using DataAccess;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public void DeleteAirport(Airport airport)
        {
            AirportDAO.DeleteAirport(airport);
        }

        public Airport? GetAirportByID(int airportID)
        {
            return AirportDAO.GetAirportByID(airportID);
        }
        public List<Airport> GetAirports(string IsDelete)
        {
            return AirportDAO.GetAirport(IsDelete);
        }

        public void InsertAirport(Airport airport)
        {
            AirportDAO.InsertAirport(airport);
        }

        public void UpdateAirport(Airport airport)
        {
            AirportDAO.UpdateAirport(airport);
        }

        public List<Airport> GetAirportByInfoCity(string airportInfo, string IsDelete) => AirportDAO.GetAirportByInfoCity(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoCode(string airportInfo, string IsDelete) => AirportDAO.GetAirportByInfoCode(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoCountry(string airportInfo, string IsDelete) => AirportDAO.GetAirportByInfoCountry(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoName(string airportInfo, string IsDelete) => AirportDAO.GetAirportByInfoName(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoState(string airportInfo, string IsDelete) => AirportDAO.GetAirportByInfoState(airportInfo, IsDelete);



    }
}
