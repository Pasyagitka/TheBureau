using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TheBureau.Repositories
{
    public class CompanyRepository
    {
        private Model _context = new Model();
        //todo single
        public Company Get()
        {
            return _context.Companies.FirstOrDefault();
        }
        public void Update()
        {
            _context.Entry(_context.Companies.FirstOrDefault()).State = EntityState.Modified;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}