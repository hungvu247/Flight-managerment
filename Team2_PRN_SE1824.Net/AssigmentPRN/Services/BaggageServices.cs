using DataAccess.BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaggageServices : IBagageServices
    {
        private readonly IBaggeRepositories baggeRepositories;

        public BaggageServices()
        {
            baggeRepositories = new BagageRepositories();
        }

        public List<BookingPlatform> GetAllBookingPlatform() => baggeRepositories.GetAllBookingPlatform();
        public void AddBaggage(Baggage baggage) => baggeRepositories.InsertBagge(baggage);

        public void DeleteBaggage(string id) => baggeRepositories.DeleteBagge(id);

        public List<dynamic> GetAllBookingHasNotBaggage() => baggeRepositories.GetAllBookingHasNotBaggage();

        public Baggage GetBaggageById(string id)  => baggeRepositories.GetBaggeById(id);

        public List<Baggage> GetBaggages()  => baggeRepositories.GetAllBagge();

        public int GetCurrentId() => baggeRepositories.GetCurrentId();

        public void UpdateBaggage(Baggage baggage)  => baggeRepositories.UpdateBagge(baggage);

        public List<Booking> GetAllBookingHasNotBaggage2() => baggeRepositories.GetAllBookingHasNotBaggage2();

        public List<dynamic> GetAllByCbBookingPlatform(string pl) => baggeRepositories.GetAllByCbBookingPlatform(pl);

        public List<dynamic> GetAllByCbDepartingAirport(string pl) => baggeRepositories.GetAllByCbDepartingAirport(pl);

        public List<dynamic> GetAllByCbArrivingAirport(string pl) => baggeRepositories.GetAllByCbArrivingAirport(pl);

        public List<dynamic> GetAllByCbDepartingAirportAndArrivingAirport(string d, string a) => baggeRepositories.GetAllByCbDepartingAirportAndArrivingAirport(d,a);

        public List<Airport> GetALlAirports()  => baggeRepositories.GetALlAirports();

        public List<dynamic> GetAllBySearch(string pl)  => baggeRepositories.GetAllBySearch(pl);

        public List<dynamic> GetAllBookingHasBaggage() => baggeRepositories.GetAllBookingHasBaggage();

        public List<dynamic> GetAllBySearchUpdate(string pl)  => baggeRepositories.GetAllBySearchUpdate(pl);

        public List<dynamic> GetAllByCbDepartingAirportAndArrivingAirportUpdate(string d, string a) => baggeRepositories.GetAllByCbDepartingAirportAndArrivingAirportUpdate(d,a);

        public List<dynamic> GetAllByCbDepartingAirportUpdate(string pl) => baggeRepositories.GetAllByCbDepartingAirportUpdate(pl);

        public List<dynamic> GetAllByCbArrivingAirportUpdate(string pl) => baggeRepositories.GetAllByCbArrivingAirportUpdate(pl);

        public List<dynamic> GetAllByCbBookingPlatformUpdate(string pl) => baggeRepositories.GetAllByCbBookingPlatformUpdate(pl);
    }
    }

