using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;
using HotelListingAPI.Repository;
using HotelListingAPI.Contracts;
using AutoMapper;
using HotelListingAPI.Model.Hotel;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {

        private readonly IHotelRespository _hotels;
        private readonly IMapper _mapper;

        public HotelsController(IHotelRespository hotels,IMapper mapper)
        {
            _hotels = hotels;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDto>>> Gethotels()
        {
            var hs  = await _hotels.GetAllAsync();
            return Ok(_mapper.Map<List<HotelDto>>(hs));
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HotelDto>> GetHotel(int id)
        {
            var hotel = await _hotels.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            var records = _mapper.Map<HotelDto>(hotel);
            return Ok(records);
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, HotelDto hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest();
            }

            var h = await _hotels.GetAsync(id);

            if (h == null) {
                return NotFound();
            }

            _mapper.Map(hotel,h);

            try
            {
                await _hotels.UpdateAsync(h);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(Hotel hotel)
        {
            
            await _hotels.AddAsync(hotel);

            return CreatedAtAction("GetHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var hotel = await _hotels.GetAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _hotels.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotels.Exists(id);
        }
    }
}
