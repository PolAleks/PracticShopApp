using OnlineShop.Web.ViewModels;

namespace OnlineShop.Web.Interfaces
{
    public interface IRolesRepository
    {
        List<RoleViewModel> GetAll();
        RoleViewModel? TryGetById(Guid id);
        RoleViewModel? TryGetByName(string name);
        void Add(RoleViewModel role);
        void Delete(Guid id);
    }
}
