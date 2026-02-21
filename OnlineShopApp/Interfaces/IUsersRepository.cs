using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IUsersRepository
    {
        User? TryGetByLogin(string login);
        void Add(User user);
        void Update(Guid id, User user);
        void Delete(Guid id);
        void UpdateRole(Guid id, Role newRole);
    }
}
