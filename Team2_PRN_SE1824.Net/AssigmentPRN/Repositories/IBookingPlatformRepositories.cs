using DataAccess.BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingPlatformRepositories
    {
        List<BookingPlatform> GetAllBookingPlatform();
        BookingPlatform GetBookingPlatformById(string id);
        void InsertBookingPlatform(BookingPlatform bookingPlatform);
        void UpdateBookingPlatform(BookingPlatform bookingPlatform);
        void DeleteBookingPlatform(string id);

        bool IsDuplicatedBookingPlatformName(string name);

        bool IsDuplicatedBookingPlatformUrl(string url);

        bool CheckExistBookingPlatformName(string id, string name);

        bool CheckExistUrl(string id, string url);

        int GetCurrentId();

        List<BookingPlatform> getBookingPlatformsDeactive();

        void Active(String id);

        //PagedResult<BookingPlatform> GetBookingPlatformsPagination(int skip, int take);

    }
}
