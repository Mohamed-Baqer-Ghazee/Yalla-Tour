using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Yalla_Tour.Data;
using Yalla_Tour.DTO;
using Yalla_Tour.Models;
using Microsoft.EntityFrameworkCore;

namespace Yalla_Tour.Controllers
{
    [Route("/[Controller]/[Action]")]
    public class UserController: Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ICollection<User> GetAllUsers()
        {
            return _context.User.ToList();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterDTO RegisterDTO)
        {
            if (RegisterDTO == null)
                return NoContent();

            var User = new User
            {
                Name = RegisterDTO.Name,
                Age = RegisterDTO.Age ,
                Gender = RegisterDTO.Gender,
                Address = RegisterDTO.Address,
                PhoneNumber = RegisterDTO.PhoneNumber,
                Email = RegisterDTO.Email,
                Password = RegisterDTO.Password,
                Role = RegisterDTO.Role
            };

            //var User = _mapper.Map<UserDTO, User>(UserDTO);

            await _context.User.AddAsync(User);
            await _context.SaveChangesAsync();
            return Ok(User);

        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int? id)
        {
            if (id != null)
            {
                try
                {
                    var User = await _context.User.Where(a => a.Id == id).FirstOrDefaultAsync();
                    if (User == null)
                        return NotFound("no User with the given id");
                    var UserDTO = _mapper.Map<User, UserDTO>(User);

                    return Ok(UserDTO);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }


            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserByPhoneNumber(string? phoneNumber)
        {
            if (phoneNumber != null)
            {
                try
                {
                    var User = await _context.User.Where(a => a.PhoneNumber == phoneNumber).FirstOrDefaultAsync();
                    if (User == null)
                        return NotFound("no Restaurant with the given address");
                    var UserDTO = _mapper.Map<User, UserDTO>(User);

                    return Ok(UserDTO);

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
