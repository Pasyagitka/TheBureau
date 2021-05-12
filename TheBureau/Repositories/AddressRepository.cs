using System.Collections.Generic;
using System.Data.Entity;

namespace TheBureau.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private Model _context = new Model();
        
        public IEnumerable<Address> GetAll()
        {
           return _context.Addresses;
        }

        public Address Get(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void Add(Address item)
        {
            _context.Addresses.Add(item);
        }

        public void Update(Address item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var address = _context.Addresses.Find(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
            }
        }
    }
}