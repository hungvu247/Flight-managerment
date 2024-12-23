
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAirportRepository
    {
        List<Airport> GetAirports(string IsDelete);
        void InsertAirport(Airport airport);
        void DeleteAirport(Airport airport);
        void UpdateAirport(Airport airport);
        Airport? GetAirportByID(int airportID);
        List<Airport> GetAirportByInfoCode(string airportInfo, string IsDelete);
        List<Airport> GetAirportByInfoCountry(string airportInfo, string IsDelete);

        List<Airport> GetAirportByInfoCity(string airportInfo, string IsDelete);

        List<Airport> GetAirportByInfoState(string airportInfo, string IsDelete);

        List<Airport> GetAirportByInfoName(string airportInfo, string IsDelete);

    }
}
