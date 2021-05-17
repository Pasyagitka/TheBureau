using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace TheBureau.Repositories
{
    public class RequestRepository
    {
        private Model _context = new();
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
        public int GetYellowRequestsCount()
        {
            return _context.Requests.Count(x => x.status == 2);
        }
        public int GetGreenRequestsCount()
        {
            return _context.Requests.Count(x => x.status == 3);
        }

        public IEnumerable<Request> GetToDoRequests()
        {
            return _context.Requests.Where(x => x.status ==1 || x.status == 2);
        }
        public IEnumerable<Request> GetRequestsByBrigadeId(int id)
        {
            return _context.Requests.Where(x => x.brigadeId == id);
        }

        public IEnumerable<Request> GetToDoRequestsForBrigade()
        {
            return _context.Requests.Where(x => x.status ==1 || x.status == 2);
        }

        public void Update(Request forUpdate)
        {
            _context.Entry(forUpdate).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var request = _context.Requests.Find(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }
        }

        public IEnumerable<Request> FindByClientId(int clientId)
        {
            return _context.Requests.Where(x =>x.clientId == clientId);
        }

        public void DeleteRequestsOfClient(int clientId)
        {
            var clientRequests = FindByClientId(clientId);
            List<int> addresses = new();
            List<int> requestsid = new();
            foreach (var request in clientRequests)
            {
                addresses.Add(request.addressId);
                requestsid.Add(request.id);
                Delete(request.id);
            }

            foreach (var id in addresses)
            {
                var address = _context.Addresses.Find(id);
                if (address != null)
                {
                    _context.Addresses.Remove(address);
                }
            }

            foreach (var id in requestsid)
            {
                var equipments = _context.RequestEquipments.Where(x=>x.requestId == id);
                foreach (var e in equipments)
                {
                    _context.RequestEquipments.Remove(e);
                }
            }
            
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