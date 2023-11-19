using Notatnik_użytkowników.Models;
using System.Linq;

namespace Notatnik_użytkowników.Repositories
{
    public interface IUserRepository
    {
        UserModel Get(int userId);        
        IQueryable<UserModel> GetAllActive();
        void Add(UserModel user);
        void Update(int userId, UserModel user);
        void Delete(int userId);
        void AddToRaportList(int userId);
    }
}