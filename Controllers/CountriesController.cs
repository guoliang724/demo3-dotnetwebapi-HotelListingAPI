﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListingAPI.Data;
using HotelListingAPI.Model.Country;
using AutoMapper;
using HotelListingAPI.Contracts;

namespace HotelListingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICountriesRespository _countries;

        public CountriesController(IMapper mapper,ICountriesRespository countries)
        {
            this._mapper = mapper;
            _countries = countries;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> Getcountries()
        {
            var countries =  await  _countries.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records); 

        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            var country = await _countries.GetDetails(id); 

            if (country == null)
            {
                return NotFound(); 
            }

            var contryDto = _mapper.Map<CountryDto>(country);

            return Ok(contryDto);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CountryDto country)
        {
            if (id != country.Id)
            {
                return BadRequest();
            }
            var c = await _countries.GetAsync(id);

            if (c == null) {
                return NotFound();
            }
            _mapper.Map(country, c);

            try
            {
                await _countries.UpdateAsync(c);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto cc)
        {
            
            var country = _mapper.Map<Country>(cc);

            await _countries.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var country = await _countries.GetAsync(id);
            if (country == null) { 
               return NotFound();
            }

            await _countries.DeleteAsync(id);
            
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            return await _countries.Exists(id);
        }
    }
}
