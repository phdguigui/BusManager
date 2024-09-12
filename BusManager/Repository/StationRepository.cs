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

        public static List<StationDTO>? GetOutdatedStations()
        {
            using var _db = new ApplicationContext();

            var result = _db.Set<StationDTO>()
                .FromSqlRaw(@"SELECT s.""Id"", s.""Address"", s.""Number"" 
                              FROM ""Stations"" s 
                              LEFT JOIN ""Stops"" s2 ON s.""Id"" = s2.""StationId"" 
                              LEFT JOIN ""Lines"" l ON l.""Id"" = s2.""LineId"" 
                              WHERE l.""Active"" = 0 OR s2.""LineId"" IS NULL
                              ORDER BY s.""Id""")
        .ToList();

            return result;
        }

        public class StationDTO
        {
            public int Id { get; set; }
            public string Address { get; set; }
            public string Number { get; set; }
        }
    }
}
