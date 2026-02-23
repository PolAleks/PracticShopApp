using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IUsersRepository
    {
        List<User>? GetAll();
        User? TryGetById(Guid id);
        User? TryGetByLogin(string login);
        void Add(User user);
        void Update(User user);
        void Delete(Guid id);
        void ChangePassword(string login, string newPassword);
        void ChangeRole(string login, Role newRole);
    }
}
