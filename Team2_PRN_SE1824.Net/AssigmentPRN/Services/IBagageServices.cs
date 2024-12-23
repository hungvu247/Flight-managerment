using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBagageServices
    {

        void AddBaggage(Baggage baggage);
        void UpdateBaggage(Baggage baggage);

        List<Baggage> GetBaggages();
        Baggage GetBaggageById(string id);

        int GetCurrentId();
        void DeleteBaggage(string id);

        List<dynamic> GetAllBookingHasNotBaggage();

        

        List<Booking> GetAllBookingHasNotBaggage2();

        List<dynamic> GetAllByCbBookingPlatform(string pl);

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
