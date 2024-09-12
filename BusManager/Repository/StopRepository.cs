using BusManager.Data;
using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusManager.Repository
{
    public class StopRepository
    {
        public static bool AddStop(Stop stop)
        {
            using var _db = new ApplicationContext();

            _db.Stop.Add(stop);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteStop(Stop stop)
        {
            using var _db = new ApplicationContext();

            _db.Stop.Remove(stop);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool AddManyStop(List<Stop> stopList)
        {
            using var _db = new ApplicationContext();

            _db.Stop.AddRange(stopList);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static List<Stop> GetRouteLine(int lineId)
        {
            using var _db = new ApplicationContext();

            return _db.Stop.Include(x => x.Station)
                .Where(x => x.LineId == lineId)
                .OrderBy(x => x.Hour)
                .ToList();
        }

        public static Stop? GetStop(Stop stop)
        {
            using var _db = new ApplicationContext();

            return _db.Stop.FirstOrDefault(x => x.StationId == stop.StationId &&
                                  x.LineId == stop.LineId);
        }

        public static List<Stop> GetStopsByStation(Station station)
        {
            using var _db = new ApplicationContext();

            return _db.Stop.Where(x => x.StationId == station.Id).ToList();
        }

        public static Stop? GetStopByStationAndLine(int stationId, int lineId)
        {
            using var _db = new ApplicationContext();

            return _db.Stop.FirstOrDefault(x => x.StationId == stationId &&
                                            x.LineId == lineId);
        }

        public static bool DeleteStopsByLineId(int lineId)
        {
            using var _db = new ApplicationContext();

            var stopsToRemove = _db.Stop.Where(x => x.LineId == lineId).ToList();

            foreach (var stop in stopsToRemove)
            {
                _db.Stop.Remove(stop);
            }

            return true;
        }
    }
}
