using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingPlatformServices
    {
        List<BookingPlatform> GetAllBookingPlatform();
        BookingPlatform GetBookingPlatformById(string id);
        void InsertBookingPlatform(BookingPlatform bookingPlatform);
        void UpdateBookingPlatform(BookingPlatform bookingPlatform);
        void DeleteBookingPlatform(string id);

        bool IsDuplicateBookingPlatformName(string name);

        bool IsDuplicateBookingPlatformUrl(string url);

        bool CheckExistName(string id, string name);

        bool CheckExistUrl(string id, string url);

        int GetCurrentID();

        List<BookingPlatform> getBookingPlatformsDeactive();

        void Active(String id);

        //PagedResult<BookingPlatform> GetBookingPlatformsPagination(int skip, int take);
    }
}
