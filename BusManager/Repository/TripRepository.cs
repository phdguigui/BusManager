using BusManager.Data;
using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusManager.Repository
{
    public class TripRepository
    {
        public static List<Trip> GetAllTrips()
        {
            using var _db = new ApplicationContext();

            return _db.Trip.Include(x => x.Bus)
                .Include(x => x.Driver)
                .Include(x => x.Line).ToList();
        }

        public static Trip? GetTripById(int id)
        {
            using var _db = new ApplicationContext();

            return _db.Trip.FirstOrDefault(x => x.Id == id);
        }

        public static bool AddTrip(Trip trip)
        {
            var _db = new ApplicationContext();

            _db.Trip.Add(trip);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool UpdateTrip(Trip trip)
        {
            var _db = new ApplicationContext();

            _db.Trip.Update(trip);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteTrip(Trip trip)
        {
            var _db = new ApplicationContext();

            _db.Trip.Remove(trip);
            var result = _db.SaveChanges();

            return result > 0;
        }
    }
}
