using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookingHotels.Domain.Entities
{
    public class Hotel
    {
        [HiddenInput(DisplayValue = false)]
        public int HotelId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Пожалуйста, введите название отеля")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Пожалуйста, введите описание для отеля")]
        public string Description { get; set; }

        [Display(Name = "Расположение")]
        [Required(ErrorMessage = "Пожалуйста, укажите расположение отеля")]
        public string Location { get; set; }

        [Display(Name = "Колличество номеров")]
        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для коллличества номеров")]
        public int CountApartment { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для числа посетителей")]
        [Display(Name = "Максимальное число посетителей")]
        public int MaxCountClients { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для цены")]
        [Display(Name = "Цена (BYN)")]
        public decimal Price { get; set; }


        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
