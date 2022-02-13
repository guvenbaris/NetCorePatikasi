using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.DbOperations;
using BookStoreApp.TokenOperations;
using Microsoft.Extensions.Configuration;

namespace BookStoreApp.Application.UserOperaitons.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;

        public string RefreshToken { get; set; }


        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;

            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault
                (x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

            if (user is not null)
            {
                var handler = new TokenHandler(_configuration);

                var token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                //_context.Users.Update(user);
                _context.SaveChanges();
                return token;
            }
            else
                throw new InvalidOperationException("Valid bir refresh token bulunamadı!");
        }

    }
}
