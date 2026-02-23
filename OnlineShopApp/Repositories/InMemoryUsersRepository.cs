using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly List<User> _users = [];

        public List<User>? GetAll() => _users;
        
        public User? TryGetByLogin(string login) => _users.FirstOrDefault(u => u.Login == login);

        public User? TryGetById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

        public void Add(User newUser)
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

        public void Update(User user)
        {
            var existingUser = TryGetById(user.Id);
            
            if (existingUser is not null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Phone = user.Phone;
            }
        }

        public void ChangeRole(string login, Role newRole)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(string login, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}
