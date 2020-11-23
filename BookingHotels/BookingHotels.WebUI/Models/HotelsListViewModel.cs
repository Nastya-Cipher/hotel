using System.Collections.Generic;
using BookingHotels.Domain.Entities;

namespace BookingHotels.WebUI.Models
{
    public class HotelsListViewModel
    {
        public IEnumerable<Hotel> Hotels { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentLocation { get; set; }
    }
}