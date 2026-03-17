using OnlineShop.Web.Interfaces;
using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Repositories
{
    public class InMemoryRolesRepository : IRolesRepository
    {
        private readonly List<RoleViewModel> _roles;

        public InMemoryRolesRepository()
        {
            _roles = 
                [
                    new RoleViewModel(){Id = Guid.NewGuid(), Name = "Admin"},
                    new RoleViewModel(){Id = Guid.NewGuid(), Name = "Moderator"},
                    new RoleViewModel(){Id = Guid.NewGuid(), Name = "User"},
                    new RoleViewModel(){Id = Guid.NewGuid(), Name = "Developer"},
                    new RoleViewModel(){Id = Guid.NewGuid(), Name = "Guest"}
                ];
        }

        public List<RoleViewModel> GetAll() => _roles;
        public RoleViewModel? TryGetById(Guid id) => _roles.FirstOrDefault(role => role.Id.Equals(id));
        public RoleViewModel? TryGetByName(string name) => _roles.FirstOrDefault(role => role.Name.Equals(name));

        public void Add(RoleViewModel role)
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
