using DataAccess;

using DataAccess.BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AirportService : IAirportService
    {
        IAirportRepository airportRepository;
        public AirportService()
        {
            airportRepository = new AirportRepository();
        }

        public void DeleteAirport(Airport airport)
        {
            airportRepository.DeleteAirport(airport);
        }

        public Airport? GetAirportByID(int airportID)
        {
            return airportRepository.GetAirportByID(airportID);
        }

        public List<Airport> GetAirports(string IsDelete)
        {
            return airportRepository.GetAirports(IsDelete);
        }

        public void InsertAirport(Airport airport)
        {
            airportRepository.InsertAirport(airport);
        }

        public void UpdateAirport(Airport airport)
        {
            airportRepository.UpdateAirport(airport);
        }

        public List<Airport> GetAirportByInfoCity(string airportInfo, string IsDelete) => airportRepository.GetAirportByInfoCity(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoCode(string airportInfo, string IsDelete) => airportRepository.GetAirportByInfoCode(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoCountry(string airportInfo, string IsDelete) => airportRepository.GetAirportByInfoCountry(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoName(string airportInfo, string IsDelete) => airportRepository.GetAirportByInfoName(airportInfo, IsDelete);
        public List<Airport> GetAirportByInfoState(string airportInfo, string IsDelete) => airportRepository.GetAirportByInfoState(airportInfo, IsDelete);

    }
}
