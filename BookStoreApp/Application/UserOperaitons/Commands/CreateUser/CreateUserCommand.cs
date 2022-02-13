using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BookStoreApp.DbOperations;
using BookStoreApp.Entities;

namespace BookStoreApp.Application.UserOperaitons.Commands.CreateUser
{
    public class CreateUserCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserModel Model { get; set; }


        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user is not null)
            {
                throw new InvalidOperationException("User already exist");
            }

            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
