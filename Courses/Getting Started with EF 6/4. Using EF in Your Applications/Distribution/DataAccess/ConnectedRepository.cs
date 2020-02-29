using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDomain.DataModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Linq;
  
    using NinjaDomain.Classes;
    using System.Data.Entity;


    namespace Repository
    {
        public class ConnectedRepository
        {
            readonly NinjaContext _context = new NinjaContext();

           
            public Ninja NewNinja()
            {
                var ninja=new Ninja
                {
                    
                };
                _context.Ninjas.Add(ninja);
                return ninja;
            }

            public Ninja GetNinjaWithEquipment(int id)
            {
                return _context.Ninjas.Include(n=>n.EquipmentOwned)
                  .FirstOrDefault(n => n.Id == id);
            }

            public Ninja GetNinjaById(int id)
            {
                return _context.Ninjas.Find(id);
            }

            public List<Ninja> GetNinjas()
            {
                return _context.Ninjas.ToList();
            }

            public ObservableCollection<Ninja> NinjasInMemory()
            {
                if (_context.Ninjas.Local.Count == 0)
                {
                    GetNinjas();
                }
                return _context.Ninjas.Local;
            }

            public void Save()
            {
                RemoveEmptyNewNinjas();
                _context.SaveChanges();
            }

            private void RemoveEmptyNewNinjas()
            {
                //you can't remove from or add to a collection in a foreach loop
                for (int i = _context.Ninjas.Local.Count; i > 0; i--)
                {
                    var ninja = _context.Ninjas.Local[i - 1];
                    if (_context.Entry(ninja).State == EntityState.Added
                        && !ninja.IsDirty)
                    {
                        _context.Ninjas.Remove(ninja);
                    }
                }
            }

            public void DeleteCurrentNinja(Ninja ninja)
            {
                _context.Ninjas.Remove(ninja);
                Save();
            }
        }
    }

}
