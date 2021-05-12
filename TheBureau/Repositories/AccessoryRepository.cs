﻿using System.Collections.Generic;
using System.Linq;

namespace TheBureau.Repositories
{
    public class AccessoryRepository
    {
        private Model _context = new Model();
        public IEnumerable<Accessory> GetAll()
        {
            return _context.Accessories;
        }
        public Accessory Get(int id)
        {
            return _context.Accessories.Find(id);
        }

        public IEnumerable<Accessory> GetByEquipmentId(string id)
        {
            return _context.Accessories.Where(x => x.Equipment.id == id);
        }
    }
}