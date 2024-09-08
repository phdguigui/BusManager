using BusManager.Data;
using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusManager.Repository
{
    public static class BusRepository
    {

        public static List<Bus>? GetAllBuses ()
        {
            var _db = new ApplicationContext();
            
            var busList = _db.Bus.AsNoTracking().ToList();
            return busList;
        }

        public static List<Bus>? GetActiveBuses()
        {
            var _db = new ApplicationContext();

            var busList = _db.Bus.Where(x => x.Active).AsNoTracking().ToList();
            return busList;
        }

        public static Bus? GetBusByPlate(string plate)
        {
            var _db = new ApplicationContext();

            var bus = _db.Bus.FirstOrDefault(x => x.Plate == plate);
            return bus;
        }

        public static bool AddBus (Bus bus)
        {
            var _db = new ApplicationContext();

            _db.Bus.Add(bus);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool UpdateBus(Bus bus)
        {
            var _db = new ApplicationContext();

            _db.Bus.Update(bus);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteBus(Bus bus)
        {
            var _db = new ApplicationContext();

            _db.Bus.Remove(bus);
            var result = _db.SaveChanges();

            return result > 0;
        }
    }
}
