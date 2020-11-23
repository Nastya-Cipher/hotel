using System.Collections.Generic;
using BookingHotels.Domain.Entities;
using BookingHotels.Domain.Abstract;

namespace BookingHotels.Domain.Concrete
{
    public class EFHotelRepository : IHotelRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Hotel> Hotels
        {
            get { return context.Hotels; }
        }

        public void SaveHotel(Hotel hotel)
        {
            if (hotel.HotelId == 0)
                context.Hotels.Add(hotel);
            else
            {
                Hotel dbEntry = context.Hotels.Find(hotel.HotelId);
                if (dbEntry != null)
                {
                    dbEntry.Name = hotel.Name;
                    dbEntry.Description = hotel.Description;
                    dbEntry.Price = hotel.Price;
                    dbEntry.Location = hotel.Location;
                    dbEntry.CountApartment = hotel.CountApartment;
                    dbEntry.MaxCountClients = hotel.MaxCountClients;
                    dbEntry.ImageData = hotel.ImageData;
                    dbEntry.ImageMimeType = hotel.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Hotel DeleteHotel(int hotelId)
        {
            Hotel dbEntry = context.Hotels.Find(hotelId);
            if (dbEntry != null)
            {
                context.Hotels.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }


    }
}