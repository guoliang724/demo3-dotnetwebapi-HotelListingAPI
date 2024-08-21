using AutoMapper;
using HotelListingAPI.Data;
using HotelListingAPI.Model.Country;
using HotelListingAPI.Model.Hotel;

namespace HotelListingAPI.Configuration
{
    public class MapperConfig:Profile
    {
        public MapperConfig() {
          CreateMap<Country,CreateCountryDto>().ReverseMap(); 
            CreateMap<Country,GetCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Hotel, HotelDto>().ReverseMap();
        }
    }
}
