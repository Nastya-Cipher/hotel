using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingHotels.Domain.Entities;

namespace BookingHotels.Domain.Abstract
{
    public interface IHotelRepository
    {
        IEnumerable<Hotel> Hotels { get; }
        void SaveHotel(Hotel hotel);
        Hotel DeleteHotel(int hotelId);
    }
}
