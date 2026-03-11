using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Interfaces
{
    public interface IUsersRepositoryM
    {
        List<UserViewModel>? GetAll();
        UserViewModel? TryGetById(Guid id);
        UserViewModel? TryGetByLogin(string login);
        void Add(UserViewModel user);
        void Update(UserViewModel user);
        void Delete(Guid id);
        void ChangePassword(string login, string newPassword);
        void ChangeRole(string login, RoleViewModel? newRole);
    }
}
