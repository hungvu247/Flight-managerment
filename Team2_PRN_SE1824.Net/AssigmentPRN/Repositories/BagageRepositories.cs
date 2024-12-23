using DataAccessLayer;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BagageRepositories : IBaggeRepositories
    {

        public void DeleteBagge(string baggage) => BaggageDAO.DeleteBagge(baggage);
            

        public List<Baggage> GetAllBagge() => BaggageDAO.GetAllBagges();

        public List<dynamic> GetAllBookingHasBaggage() => BaggageDAO.GetAllBookingHasBaggage();
        public List<dynamic> GetAllBookingHasNotBaggage() => BaggageDAO.GetAllBookingHasNotBaggage();

        public List<Booking> GetAllBookingHasNotBaggage2() => BaggageDAO.GetAllBookingHasNotBaggage2();

        public Baggage GetBaggeById(string baggage) => BaggageDAO.GetBaggageById(baggage);

        public int GetCurrentId() => BaggageDAO.GetCurrentID();

        public void InsertBagge(Baggage baggage) => BaggageDAO.InsertBagge(baggage);

        public void UpdateBagge(Baggage baggage) => BaggageDAO.UpdateBagge(baggage);

        public List<BookingPlatform> GetAllBookingPlatform() => BaggageDAO.getBookingPlatforms();

        public List<dynamic> GetAllByCbBookingPlatform(string pl) => BaggageDAO.GetAllByCbBookingPlatform(pl);

        public List<dynamic> GetAllByCbDepartingAirport(string pl) => BaggageDAO.GetAllByCbDepartingAirport(pl);

        public List<dynamic> GetAllByCbArrivingAirport(string pl) => BaggageDAO.GetAllByCbArrivingAirport(pl);

        public List<dynamic> GetAllByCbDepartingAirportAndArrivingAirport(string d, string a) => BaggageDAO.GetAllByCbDepartingAirportAndArrivingAirport(d,a);

        public List<Airport> GetALlAirports() => BaggageDAO.LoadAllAirport();

        public List<dynamic> GetAllBySearch(string pl)  => BaggageDAO.GetAllBySearch(pl);

        public List<dynamic> GetAllBySearchUpdate(string pl)  => BaggageDAO.GetAllBySearchUpdate(pl);

        public List<dynamic> GetAllByCbDepartingAirportAndArrivingAirportUpdate(string d, string a) => BaggageDAO.GetAllByCbDepartingAirportAndArrivingAirportUpdate(d,a);

        public List<dynamic> GetAllByCbDepartingAirportUpdate(string pl) => BaggageDAO.GetAllByCbDepartingAirportUpdate(pl);

        public List<dynamic> GetAllByCbArrivingAirportUpdate(string pl) => BaggageDAO.GetAllByCbArrivingAirportUpdate(pl);

        public List<dynamic> GetAllByCbBookingPlatformUpdate(string pl) => BaggageDAO.GetAllByCbBookingPlatformUpdate(pl);
    }
}
