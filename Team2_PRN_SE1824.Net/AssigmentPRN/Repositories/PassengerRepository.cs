using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        public List<Passenger> GetPassengers() => PassengerDAO.GetPassengers();
        public void InsertPassenger(Passenger passenger) => PassengerDAO.InsertPassenger(passenger);
        public void UpdatePassenger(Passenger passenger) => PassengerDAO.UpdatePassenger(passenger);
        public void DeletePassenger(Passenger passenger) => PassengerDAO.DeletePassenger(passenger);
        public Passenger? GetPassengerById(int id) => PassengerDAO.GetPassengerById(id);
    }
}
