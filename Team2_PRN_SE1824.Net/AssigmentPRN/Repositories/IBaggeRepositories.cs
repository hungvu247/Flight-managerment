using DataAccessLayer;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaggeRepositories
    {
        //private static readonly BaggageDAO baggageDAO = new BaggageDAO();

        void InsertBagge(Baggage baggage);

        void UpdateBagge(Baggage baggage);

        void DeleteBagge(string baggage);

        List<Baggage> GetAllBagge();

        Baggage GetBaggeById(string baggage);

        int GetCurrentId();

        List<dynamic> GetAllBookingHasNotBaggage();

       

        List<dynamic> GetAllByCbBookingPlatform(string pl);

        List<Booking> GetAllBookingHasNotBaggage2(); 

        List<BookingPlatform> GetAllBookingPlatform();

        List<dynamic> GetAllByCbDepartingAirport(string pl);
        List<dynamic> GetAllByCbArrivingAirport(string pl);

        List<dynamic> GetAllByCbDepartingAirportAndArrivingAirport(string d, string a);

        List<Airport> GetALlAirports();

        List<dynamic> GetAllBySearch(string pl);

        List<dynamic> GetAllBySearchUpdate(string pl);

        List<dynamic> GetAllByCbDepartingAirportAndArrivingAirportUpdate(string d, string a);

        List<dynamic> GetAllByCbDepartingAirportUpdate(string pl);

        List<dynamic> GetAllByCbArrivingAirportUpdate(string pl);

        List<dynamic> GetAllByCbBookingPlatformUpdate(string pl);

        List<dynamic> GetAllBookingHasBaggage();

    }
}
