using DataAccess.BussinessObjects;
using Reposiotories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;
        public BookingService()
        {
            bookingRepository = new BookingRepository();  
        }

        public void AddBooking(Booking booking)
        {
            bookingRepository.AddBooking(booking);
        }

        public List<Booking> GetAllBookings()
        {
            return bookingRepository.GetAllBookings();
        }

        public List<Booking> GetAllBookingsRemoved()
        {
            return bookingRepository.GetAllBookingsRemoved();
        }

        public Booking? GetBookingById(int Id)
        {
            return bookingRepository.GetBookingById(Id);
        }

        public Booking? GetBookingWithLargestId()
        {
            return bookingRepository.GetBookingWithLargestId();
        }

        public void RemoveBooking(Booking booking)
        {
            bookingRepository.RemoveBooking(booking);
        }

        public void RemoveBookingFromBin(Booking booking)
        {
            bookingRepository.RemoveBookingFromBin(booking);
        }

        public void UpdateBooking(Booking booking)
        {
            bookingRepository.UpdateBooking(booking);
        }
    }
}
