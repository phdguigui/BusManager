using BusManager.Data;
using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusManager.Repository
{
    public static class StationRepository
    {
        public static List<Station>? GetAllStations()
        {
            using var _db = new ApplicationContext();

            var stationList = _db.Station.AsNoTracking().ToList();
            return stationList;
        }

        public static List<Station>? GetActiveStations()
        {
            using var _db = new ApplicationContext();

            var stationList = _db.Station.Where(x => x.Active).AsNoTracking().ToList();
            return stationList;
        }

        public static Station? GetStationById(int id)
        {
            using var _db = new ApplicationContext();

            var station = _db.Station.FirstOrDefault(x => x.Id == id);
            return station;
        }

        public static bool AddStation(Station station)
        {
            using var _db = new ApplicationContext();

            _db.Station.Add(station);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool UpdateStation(Station station)
        {
            using var _db = new ApplicationContext();

            _db.Station.Update(station);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteStation(Station station)
        {
            using var _db = new ApplicationContext();

            _db.Station.Remove(station);
            var result = _db.SaveChanges();

            return result > 0;
        }
    }
}
