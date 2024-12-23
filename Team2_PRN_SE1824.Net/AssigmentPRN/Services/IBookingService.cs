using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingService
    {
        List<Booking> GetAllBookings();
        Booking? GetBookingById(int Id);
        void RemoveBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void AddBooking(Booking booking);
        Booking? GetBookingWithLargestId();
        List<Booking> GetAllBookingsRemoved();
        void RemoveBookingFromBin(Booking booking);
    }
}
