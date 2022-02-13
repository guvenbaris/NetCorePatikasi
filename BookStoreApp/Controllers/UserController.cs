using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookStoreApp.Application.UserOperaitons.Commands.CreateToken;
using BookStoreApp.Application.UserOperaitons.Commands.CreateUser;
using BookStoreApp.Application.UserOperaitons.Commands.RefreshToken;
using BookStoreApp.DbOperations;
using BookStoreApp.TokenOperations;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            command.Handle();
            return Ok();
        }

        [HttpPost("connect/token")]
        public IActionResult CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return Ok(token);
        }

        [HttpGet("refreshToken")]
        public IActionResult CreateToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context,_configuration);
            command.RefreshToken = token;
            var resultToken = command.Handle();
            return Ok(resultToken);
        }
    }
}
