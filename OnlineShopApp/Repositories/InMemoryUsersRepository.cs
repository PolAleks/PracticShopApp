using OnlineShopApp.Interfaces;
using OnlineShopApp.Models;

namespace OnlineShopApp.Repositories
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private readonly List<User> _users = [];
        public void Add(User newUser)
        {
            newUser.Id = Guid.NewGuid();
            newUser.CreationDateTime = DateTime.Now;

            _users.Add(newUser);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public User? TryGetByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login == login);
        }

        public void Update(Guid id, User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateRole(Guid id, Role newRole)
        {
            throw new NotImplementedException();
        }
    }
}
