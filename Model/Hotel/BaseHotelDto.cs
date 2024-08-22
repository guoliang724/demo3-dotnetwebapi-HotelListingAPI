using System.ComponentModel.DataAnnotations;

namespace HotelListingAPI.Model.Hotel
{
    public abstract class BaseHotelDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string HotelName { get; set; }
        public string Address { get; set; }
        public double? Rating
        {
            get; set;
        }
        [Required]
        [Range(1,100)]
        public int CountryId { get; set; }
    }
}
