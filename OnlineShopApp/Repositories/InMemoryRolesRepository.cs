using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryRolesRepository : IRolesRepository
    {
        private readonly List<Role> _roles = [];

        public List<Role> GetAll() => _roles;
        public Role? TryGetById(Guid id) => _roles.FirstOrDefault(role => role.Id.Equals(id));
        public Role? TryGetByName(string name) => _roles.FirstOrDefault(role => role.Name.Equals(name));

        public void Add(Role role)
        {
            role.Id = Guid.NewGuid();

            _roles.Add(role);
        }

        public void Delete(Guid id)
        {
            var role = _roles.FirstOrDefault(role => role.Id == id);

            if (role is not null)
            {
                _roles.Remove(role);
            }
        }
    }
}
