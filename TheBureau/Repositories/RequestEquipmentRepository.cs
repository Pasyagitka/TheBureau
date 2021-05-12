using System.Collections.Generic;
using System.Data.Entity;

namespace TheBureau.Repositories
{
    public class RequestEquipmentRepository : IRepository<RequestEquipment>
    {
        private Model _context = new Model();
        public IEnumerable<RequestEquipment> GetAll()
        {
            return _context.RequestEquipments;
        }

        public RequestEquipment Get(int id)
        {
            return _context.RequestEquipments.Find(id);
        }

        public void Add(RequestEquipment item)
        {
            _context.RequestEquipments.Add(item);
        }

        public void Update(RequestEquipment item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var requestequipment = _context.RequestEquipments.Find(id);
            if (requestequipment != null)
            {
                _context.RequestEquipments.Remove(requestequipment);
            }
        }
    }
}