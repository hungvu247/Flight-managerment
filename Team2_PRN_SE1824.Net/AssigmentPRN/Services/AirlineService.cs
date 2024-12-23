using DataAccess.BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AirlineService : IAirlineService
    {

        private readonly IAirlineRepository airlineRepository;

        public AirlineService()
        {
            airlineRepository = new AirlineRepository();
        }
        public List<Airline> GetAirlines()
        {
            return airlineRepository.GetAirlines()
                .OrderByDescending(p => p.Id)
                .ToList();
        }
        public void InsertAirline(Airline airline)
        {
            airlineRepository.InsertAirline(airline);
        }
        public void UpdateAirline(Airline airline)
        {
            airlineRepository.UpdateAirline(airline);
        }
        public void DeleteAirline(Airline airline)
        {
            airlineRepository.DeleteAirline(airline);
        }
        public Airline? GetAirlineById(int id)
        {
            return airlineRepository.GetAirlineByID(id);
        }
        public int GetLatestAirlineId()
        {
            var airlines = GetAirlines();
            if (airlines.Count == 0)
            {
                return 0;
            }
            return airlines.Max(a => a.Id);
        }
        public List<Airline> SearchAirlinesByNameActive(string name)
        {
            var airlines = GetActiveAirlines();
            return airlines.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Airline> SearchAirlinesByNameInactive(string name)
        {
            var airlines = GetInactiveAirlines();
            return airlines.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public List<Airline> GetAirlinesByStatus(bool status)
        {
            var airlines = GetAirlines();
            return airlines.Where(a => a.Status == status).ToList();
        }

        public List<Airline> GetActiveAirlines()
        {
            return GetAirlinesByStatus(true);
        }

        public List<Airline> GetInactiveAirlines()
        {
            return GetAirlinesByStatus(false);
        }

    }
}
