using Notatnik_użytkowników.Models;
using System;
using System.Linq;

namespace Notatnik_użytkowników.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagerContext _context;
        public UserRepository(UserManagerContext context)
        {
            _context = context;
        }

        public UserModel Get(int userId)
            => _context.Users.FirstOrDefault(x => x.UserId == userId);

        public IQueryable<UserModel> GetAllActive()
            => _context.Users;

        public void Add(UserModel user)
        {
            if (user.DateOfBirth > DateTime.Today)
            {
                throw new Exception("Wrong date.");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(int userId, UserModel user)
        {
            var result = _context.Users.SingleOrDefault(x => x.UserId == userId);

            if (result != null)
            {
                result.Name = user.Name;
                result.Surname = user.Surname;
                result.DateOfBirth = user.DateOfBirth;
                result.Gender = user.Gender;
                result.AdditionalAttribute = user.AdditionalAttribute;
            }

            if (result.DateOfBirth >= DateTime.Today)
            {
                throw new Exception("Wrong date.");
            }

            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var result = _context.Users.SingleOrDefault(x => x.UserId == userId);

            if (result != null)
            {
                _context.Users.Remove(result);
                _context.SaveChanges();
            }
        }

        public void AddToRaportList(int userId)
        {
            var result = _context.Users.SingleOrDefault(x => x.UserId == userId);

            if (result != null)
            {
                result.UserRaport = result.UserRaport == true ? false : true;

                _context.SaveChanges();
            }
        }
    }
}