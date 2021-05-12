using System.Collections.Generic;
using System.Linq;
using TheBureau.Models.DataManipulating;

namespace TheBureau.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private Model _context = new Model();
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }
        public User Get(int id)
        {
            return _context.Users.Find(id);
        }
        
        public User Login(string login, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.login == login);
            if (user == null) return null;
            return PasswordHash.ValidatePassword(password, user.password) ? user : null;
        }

        public void Add(User item)
        {
            throw new System.NotImplementedException();
        }
        
        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}