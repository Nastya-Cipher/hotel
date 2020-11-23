using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotels.Domain.Abstract;

namespace BookingHotels.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IHotelRepository repository;

        public NavController(IHotelRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string location = null)
        {
            ViewBag.SelectedLocation = location;

            IEnumerable<string> locations = repository.Hotels
                .Select(hotel => hotel.Location)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(locations);
        }
    }
}