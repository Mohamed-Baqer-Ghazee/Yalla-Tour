using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Yalla_Tour.Data;
using Yalla_Tour.DTO;
using Yalla_Tour.Models;
using Microsoft.EntityFrameworkCore;

namespace Yalla_Tour.Controllers
{
    [Route("/[Controller]/[Action]")]
    public class LocationController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LocationController (ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ICollection<Location> GetAllLocations()
        {
            return _context.Location.ToList();
        }
        [HttpPost]
        public async Task<IActionResult> AddLocation( LocationDTO locationDTO)
        {
            if(locationDTO == null)
                return NoContent();

            var Location = new Location
            {
                Name = locationDTO.Name ?? "NA",
                Description = locationDTO.Description ?? "NA",
                Address = locationDTO.Address ?? "NA",
                Type = locationDTO.Type ?? "NA",
                OTime = locationDTO.OTime ?? "NA",
                CTime = locationDTO.CTime ?? "NA",
                EnteranceFee = locationDTO.EnteranceFee,
                Rules = locationDTO.Rules ?? "NA",
                RestaurantId = locationDTO.RestaurantId,
                TourGuideId = locationDTO.TourGuideId
            };

            //var Location = _mapper.Map<LocationDTO, Location>(locationDTO);

            await _context.Location.AddAsync(Location);
            await _context.SaveChangesAsync();
            return Ok(Location);

        }

        [HttpGet]
        public async Task<IActionResult> GetLocationByAddress(string? address)
        {
            if (address != null)
            {
                try
                {
                    var location = await _context.Location.Where(a => a.Address == address).FirstOrDefaultAsync();
                    if(location == null)
                        return NotFound("no location with the given address");
                    var LocationDTO = _mapper.Map<Location, LocationDTO>(location);
                    
                    return Ok(LocationDTO);

                }catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            

            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetLocationById(int? id)
        {
            if (id != null)
            {
                try
                {
                    var Location = await _context.Location.Where(a => a.Id == id).FirstOrDefaultAsync();
                    if (Location == null)
                        return NotFound("no Location with the given id");
                    var LocationDTO = _mapper.Map<Location, LocationDTO>(Location);

                    return Ok(LocationDTO);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            return NotFound();
        }
    }
}
