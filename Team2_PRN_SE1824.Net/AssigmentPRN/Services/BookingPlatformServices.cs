using DataAccess.BussinessObjects;

using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingPlatformServices : IBookingPlatformServices
    {

        private readonly IBookingPlatformRepositories bookingPlatformRepositories;
        public BookingPlatformServices()
        {
            bookingPlatformRepositories = new BookingPlatformRepositories();
        }

        public void Active(string id)  => bookingPlatformRepositories.Active(id);

        public bool CheckExistName(string id, string name) => bookingPlatformRepositories.CheckExistBookingPlatformName(id, name);

        public bool CheckExistUrl(string id, string url) => bookingPlatformRepositories.CheckExistUrl(id, url);

        public void DeleteBookingPlatform(string id) => bookingPlatformRepositories.DeleteBookingPlatform(id);

        public List<BookingPlatform> GetAllBookingPlatform() => bookingPlatformRepositories.GetAllBookingPlatform();

        public BookingPlatform GetBookingPlatformById(string id) => bookingPlatformRepositories.GetBookingPlatformById(id);

        public List<BookingPlatform> getBookingPlatformsDeactive()  => bookingPlatformRepositories.getBookingPlatformsDeactive();

        //public PagedResult<BookingPlatform> GetBookingPlatformsPagination(int skip, int take) => bookingPlatformRepositories.GetBookingPlatformsPagination(skip, take);

        public int GetCurrentID() => bookingPlatformRepositories.GetCurrentId();

        public void InsertBookingPlatform(BookingPlatform bookingPlatform) => bookingPlatformRepositories.InsertBookingPlatform(bookingPlatform);

        public bool IsDuplicateBookingPlatformName(string name) => bookingPlatformRepositories.IsDuplicatedBookingPlatformName(name);

        public bool IsDuplicateBookingPlatformUrl(string url) => bookingPlatformRepositories.IsDuplicatedBookingPlatformUrl(url);

        public void UpdateBookingPlatform(BookingPlatform bookingPlatform) => bookingPlatformRepositories.UpdateBookingPlatform(bookingPlatform);


    }
}
