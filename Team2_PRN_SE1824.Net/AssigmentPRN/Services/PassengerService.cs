using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
using Repositories;

namespace Services
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository passengerRepository;

        public PassengerService()
        {
            passengerRepository = new PassengerRepository();
        }
        public List<Passenger> GetPassengers()
        {
            return passengerRepository.GetPassengers()
                .OrderByDescending(p => p.Id)
                .ToList();
        }
        public void InsertPassenger(Passenger passenger)
        {
            passengerRepository.InsertPassenger(passenger);
        }
        public void UpdatePassenger(Passenger passenger)
        {
            passengerRepository.UpdatePassenger(passenger);
        }
        public void DeletePassenger(Passenger passenger)
        {
            passengerRepository.DeletePassenger(passenger);
        }
        public Passenger? GetPassengerById(int id)
        {
            return passengerRepository.GetPassengerById(id);
        }
        public int GetLatestPassengerId()
        {
            var passengers = GetPassengers().ToList();
            if (passengers.Count == 0)
            {
                return 0;
            }
            return passengers.Max(p => p.Id);
        }

        public List<Passenger> SearchPassengersByNameActive(string name)
        {
            var passengers = GetPassengerActive();                                     
            return passengers
                .Where(p => p.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                            p.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public List<Passenger> SearchPassengersByNameInactive(string name)
        {
            var passengers = GetPassengerInactive();
            return passengers
                .Where(p => p.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                            p.LastName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        public List<Passenger> GetPassengerByStatus(bool status)
        {
            var passengers = GetPassengers();
            return passengers.Where(a => a.Status == status).ToList();
        }

        public List<Passenger> GetPassengerActive()
        {
            return GetPassengerByStatus(true);
        }

        public List<Passenger> GetPassengerInactive()
        {
            return GetPassengerByStatus(false);
        }
    }
}
