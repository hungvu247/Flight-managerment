using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BussinessObjects;
using DataAccessLayer;
namespace Repositories
{
    public interface IPassengerRepository
    {
        List<Passenger> GetPassengers();
        void InsertPassenger(Passenger passenger);
        void UpdatePassenger(Passenger passenger);
        void DeletePassenger(Passenger passenger);
        Passenger? GetPassengerById(int id);
        
    }
}
