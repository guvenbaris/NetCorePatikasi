using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.Application.UserOperaitons.Commands.CreateUser;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;
using BookStoreApp.TokenOperations;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Application.UserOperaitons.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenModel Model { get; set; }


        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault
            (x => x.Email == Model.Email && x.Password == Model.Password);

            if (user is not null)
            {
                var handler = new TokenHandler(_configuration);

                var token =  handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                //_context.Users.Update(user);
                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Username - Password Wrong");
        }

    }        
    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
