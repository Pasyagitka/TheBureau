using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace TheBureau.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private Model _context = new Model();
        
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients;
        }

        public Client Get(int id)
        {
            return _context.Clients.Find(id);
        }

        public void Add(Client item)
        {
            _context.Clients.Add(item);
        }

        public void Update(Client forUpdate)
        {
            _context.Entry(forUpdate).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }
        }

        public IEnumerable<Client> FindClientsByCriteria(string criteria)
        {
            return GetAll().Where(x => x.firstname.ToLower().Contains(criteria.ToLower())
            || x.surname.ToLower().Contains(criteria.ToLower()) 
            || x.patronymic.ToLower().Contains(criteria.ToLower())
            || x.email.ToLower().Contains(criteria.ToLower())
            || x.contactNumber.ToString().Contains(criteria)
            || x.id.ToString().Contains(criteria));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}