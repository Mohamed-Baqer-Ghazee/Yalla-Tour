using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yalla_Tour.Data;
using Yalla_Tour.DTO;
using Yalla_Tour.Models;

namespace Yalla_Tour.Controllers
{
    [Route("/[Controller]/[Action]")]
    public class RestaurantController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ICollection<Restaurant> GetAllRestaurants()
        {
            return _context.Restaurant.ToList();
        }
        [HttpPost]
        public async Task<IActionResult> AddRestaurant(RestaurantDTO RestaurantDTO)
        {
            if (RestaurantDTO == null)
                return NoContent();

            var Restaurant = new Restaurant
            {
                Name = RestaurantDTO.Name ?? "NA",
                Description = RestaurantDTO.Description ?? "NA",
                Address = RestaurantDTO.Address ?? "NA",
                Type = RestaurantDTO.Type ?? "NA",
                ImagesUrl = RestaurantDTO.ImagesUrl ?? "NA",
                Menu = RestaurantDTO.Menu ?? "NA"
            };

            //var Restaurant = _mapper.Map<RestaurantDTO, Restaurant>(RestaurantDTO);

            await _context.Restaurant.AddAsync(Restaurant);
            await _context.SaveChangesAsync();
            return Ok(Restaurant);

        }

        [HttpGet]
        public async Task<IActionResult> GetRestaurantByAddress(string? address)
        {
            if (address != null)
            {
                try
                {
                    var Restaurant = await _context.Restaurant.Where(a => a.Address == address).FirstOrDefaultAsync();
                    if (Restaurant == null)
                        return NotFound("no Restaurant with the given address");
                    var RestaurantDTO = _mapper.Map<Restaurant, RestaurantDTO>(Restaurant);

                    return Ok(RestaurantDTO);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetRestaurantById(int? id)
        {
            if (id != null)
            {
                try
                {
                    var Restaurant = await _context.Restaurant.Where(a => a.Id == id).FirstOrDefaultAsync();
                    if (Restaurant == null)
                        return NotFound("no Restaurant with the given id");
                    var RestaurantDTO = _mapper.Map<Restaurant, RestaurantDTO>(Restaurant);

                    return Ok(RestaurantDTO);

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
