using OnlineShopApp.Interfaces;
using OnlineShopApp.Models.ViewModel;

namespace OnlineShopApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepositoryM
    {
        private readonly List<UserViewModel> _users = [];

        public List<UserViewModel>? GetAll() => _users;
        
        public UserViewModel? TryGetByLogin(string login) => _users.FirstOrDefault(u => u.Login == login);

        public UserViewModel? TryGetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

        public void Add(UserViewModel newUser)
        {
            newUser.Id = Guid.NewGuid();
            newUser.CreationDateTime = DateTime.Now;

            _users.Add(newUser);
        }

        public void Delete(Guid id)
        {
            var deletedUser = TryGetById(id);

            if (deletedUser is not null)
            {
                _users.Remove(deletedUser);
            }
        }

        public void Update(UserViewModel user)
        {
            var existingUser = TryGetById(user.Id);
            
            if (existingUser is not null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Phone = user.Phone;
            }
        }

        public void ChangeRole(string login, RoleViewModel? newRole)
        {
            var existingUser = TryGetByLogin(login);
            
            if (existingUser is not null)
            {
                existingUser.Role = newRole.ToString();
            }
        }

        public void ChangePassword(string login, string newPassword)
        {
            var user = TryGetByLogin(login);
            
            if (user is not null)
            {
                user.Password = newPassword;
            }
        }
    }
}
