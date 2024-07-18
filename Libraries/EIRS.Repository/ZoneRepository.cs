using EIRS.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIRS.Repository
{
    public class ZoneRepository : IZoneRepository
    {
        //EIRSContext _db;
        //public List<Zone> GetAll()
        //{
        //    using (_db = new EIRSContext())
        //    {
        //        return _db.Zone.Where(o=>o.Active==true).ToList();
        //    }
        //}

        //public Zone GetById(int id)
        //{
        //    using (_db = new EIRSContext())
        //    {
        //        return _db.Zone.FirstOrDefault(o => o.ZoneId == id);
        //    }
        //}
        //public Zone GetByZoneCode(string zoneCode)
        //{
        //    using (_db = new EIRSContext())
        //    {
        //        return _db.Zone.FirstOrDefault(o => o.ZoneCode == zoneCode);
        //    }
        //}
    }
    public interface IZoneRepository
    {
        //List<Zone> GetAll();
        //Zone GetById(int id);
        //Zone GetByZoneCode(string zoneCode);
    }
}
