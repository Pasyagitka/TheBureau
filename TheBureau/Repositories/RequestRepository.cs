using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TheBureau.Repositories
{
    public class RequestRepository
    {
        //getclientsrequest
        private Model _context = new Model();
        public IEnumerable<Request> GetAll()
        {
            return _context.Requests;
        }
        public Request Get(int id)
        {
            return _context.Requests.Find(id);
        }
        public void Add(Request request)
        {
            _context.Requests.Add(request);
        }

        public int GetRedRequestsCount()
        {
            return _context.Requests.Count(x => x.status == 1);
        }
        public IEnumerable<Request> GetRequestsByBrigadeId(int id)
        {
            return GetAll().Where(x => x.brigadeId == id);
        }

        public IEnumerable<Request> GetToDoRequestsForBrigade()
        {
            return GetAll().Where(x => x.status is 1 or 2);
        }
        
        public void Update(Request forUpdate)
        {
            _context.Entry(forUpdate).State = EntityState.Modified;
        }
        
        #region  async
        public Task<List<Request>> GetRequestsAsync()
        {
            return _context.Requests.ToListAsync();
        }
        public Task<Request> GetRequestsAsync(int Id)
        {
            return _context.Requests.FirstOrDefaultAsync(a => a.id == Id);
        }
        public async Task<Request> AddRequestsAsync(Request customer)
        {
            _context.Requests.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task DeleteRequestAsync(int customerId)
        {
            var customer = _context.Requests.FirstOrDefault(c => c.id == customerId);
            if (customer != null)
            {
                _context.Requests.Remove(customer);
            }
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}