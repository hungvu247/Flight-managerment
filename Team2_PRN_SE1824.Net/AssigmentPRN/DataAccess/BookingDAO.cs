using DataAccess.BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class BookingDAO
    {
        public static List<Booking> GetAllBookings()
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            return db.Bookings
                .Where(b => b.Status == false || b.Status == null)
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .Include(b => b.BookingPlatform)
                .Include(b => b.Baggages)
                .OrderByDescending(b => b.BookingTime)
                .ToList();
        }

        public static List<Booking> GetAllBookingsRemoved()
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            return db.Bookings
                .Where(b => b.Status == true)
                .Include(b => b.Passenger)
                .Include(b => b.Flight)
                .Include(b => b.BookingPlatform)
                .Include(b => b.Baggages)
                .OrderByDescending(b => b.BookingTime)
                .ToList();
        }

        public static Booking? GetBookingById(int Id)
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            Booking? booking = db.Bookings
                .Where(b => b.Id == Id)
                .Include(b => b.Baggages)
                .FirstOrDefault();
            return booking;
        }

        public static void RemoveBooking(Booking booking)
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            booking.Status = true;
            UpdateBooking(booking);
            db.SaveChanges();
        }

        public static void RemoveBookingFromBin(Booking booking)
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            db.Bookings.Remove(booking);
            db.SaveChanges();
        }

        public static void UpdateBooking(Booking booking)
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            db.Bookings.Update(booking);
            db.SaveChanges();
        }

        public static void AddBooking(Booking booking)
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            db.Bookings.Add(booking);
            db.SaveChanges();
        }

        public static Booking? GetBookingWithLargestId()
        {
            FlightManagementDbContext db = new FlightManagementDbContext();
            return db.Bookings
                .OrderByDescending(b => b.Id)
                .FirstOrDefault();
        }
    }
}
