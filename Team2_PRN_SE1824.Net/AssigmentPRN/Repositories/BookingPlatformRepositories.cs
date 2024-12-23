using DataAccessLayer;
using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingPlatformRepositories : IBookingPlatformRepositories
    {
        public void Active(string id)  => BookingPlatformDAO.Active(id);

        public bool CheckExistBookingPlatformName(string id, string name) => BookingPlatformDAO.CheckExistName(id,name);

        public bool CheckExistUrl(string id, string url) => BookingPlatformDAO.CheckExistUrl(id,url);

       

        public void DeleteBookingPlatform(string id)
        {
            BookingPlatformDAO.DeleteBookingPlatform( id);
        }

        public List<BookingPlatform> GetAllBookingPlatform() =>  BookingPlatformDAO.getBookingPlatforms();

        public BookingPlatform GetBookingPlatformById(string id) => BookingPlatformDAO.GetBookingPlatformsById(id);

        public List<BookingPlatform> getBookingPlatformsDeactive()  => BookingPlatformDAO.getBookingPlatformsDeactive();

        //public PagedResult<BookingPlatform> GetBookingPlatformsPagination(int skip, int take) => BookingPlatformDAO.GetBookingPlatformsPagination(skip, take);

        public int GetCurrentId() => BookingPlatformDAO.GetCurrentID();

        public void InsertBookingPlatform(BookingPlatform bookingPlatform) => BookingPlatformDAO.insertBookingPlatforms(bookingPlatform);

        public bool IsDuplicatedBookingPlatformName(string name) => BookingPlatformDAO.IsDuplicateBookingPlatformName(name);

        public bool IsDuplicatedBookingPlatformUrl(string url) => BookingPlatformDAO.IsDuplicateBookingPlatformUrl(url);

        public void UpdateBookingPlatform(BookingPlatform bookingPlatform) => BookingPlatformDAO.updateBookingPlatforms(bookingPlatform);
    }
}
