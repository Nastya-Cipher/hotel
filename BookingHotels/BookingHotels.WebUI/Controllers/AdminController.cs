using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookingHotels.Domain.Abstract;
using BookingHotels.Domain.Entities;

namespace BookingHotels.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IHotelRepository repository;

        public AdminController(IHotelRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Hotels);
        }

        public ViewResult Edit(int hotelId)
        {
            Hotel hotel = repository.Hotels
                .FirstOrDefault(g => g.HotelId == hotelId);
            return View(hotel);
        }

        [HttpPost]
        public ActionResult Edit(Hotel hotel, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    hotel.ImageMimeType = image.ContentType;
                    hotel.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(hotel.ImageData, 0, image.ContentLength);
                }
                repository.SaveHotel(hotel);
                TempData["message"] = string.Format("Изменения в отеле \"{0}\" были сохранены", hotel.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(hotel);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Hotel());
        }

        [HttpPost]
        public ActionResult Delete(int hotelId)
        {
            Hotel deletedHotel = repository.DeleteHotel(hotelId);
            if (deletedHotel != null)
            {
                TempData["message"] = string.Format("Запись \"{0}\" была удалена",
                    deletedHotel.Name);
            }
            return RedirectToAction("Index");
        }

        
    }
}