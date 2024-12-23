using DataAccess;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reposiotories
{
    public class BookingRepository : IBookingRepository
    {
        public void AddBooking(Booking booking)
        {
            BookingDAO.AddBooking(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return BookingDAO.GetAllBookings();
        }

        public List<Booking> GetAllBookingsRemoved()
        {
            return BookingDAO.GetAllBookingsRemoved();
        }

        public Booking? GetBookingById(int Id)
        {
            return BookingDAO.GetBookingById(Id);
        }

        public Booking? GetBookingWithLargestId()
        {
            return BookingDAO.GetBookingWithLargestId();
        }

        public void RemoveBooking(Booking booking)
        {
            BookingDAO.RemoveBooking(booking);
        }

        public void RemoveBookingFromBin(Booking booking)
        {
            BookingDAO.RemoveBookingFromBin(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            BookingDAO.UpdateBooking(booking);
        }
    }
}
