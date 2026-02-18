using OnlineShopApp.Models;

namespace OnlineShopApp.Interfaces
{
    public interface IRolesRepository
    {
        List<Role> GetAll();
        Role? TryGetById(Guid id);
        Role? TryGetByName(string name);
        void Add(Role role);
        void Delete(Guid id);
    }
}
