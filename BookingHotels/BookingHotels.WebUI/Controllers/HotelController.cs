using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotels.Domain.Abstract;
using BookingHotels.Domain.Entities;
using BookingHotels.WebUI.Models;

namespace BookingHotels.WebUI.Controllers
{
    public class HotelController : Controller
    {
        private IHotelRepository repository;
        public int pageSize = 4;

        public HotelController(IHotelRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult List_of_hotels(string location, int page = 1)
        {
            HotelsListViewModel model = new HotelsListViewModel
            {
                Hotels = repository.Hotels
                    .Where(p => location == null || p.Location == location)
                    .OrderBy(hotel => hotel.HotelId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = location == null ?
                    repository.Hotels.Count() :
                    repository.Hotels.Where(hotel => hotel.Location == location).Count()
                },
                CurrentLocation = location
            };
            return View(model);
        }

        public FileContentResult GetImage(int hotelId)
        {
            Hotel hotel = repository.Hotels
                .FirstOrDefault(g => g.HotelId == hotelId);

            if (hotel != null)
            {
                return File(hotel.ImageData, hotel.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}
