using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
using Repositories;

namespace Services
{
    public interface IPassengerService
    {
        List<Passenger> GetPassengers();
        void InsertPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        void DeletePassenger(Passenger passenger);
        Passenger? GetPassengerById(int id);
        int GetLatestPassengerId();
        List<Passenger> SearchPassengersByNameActive(string name);
        List<Passenger> SearchPassengersByNameInactive(string name);
        List<Passenger> GetPassengerActive();
        List<Passenger> GetPassengerInactive();
    }
}
