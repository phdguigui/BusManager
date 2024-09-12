using BusManager.Data;
using BusManager.Model.DTO;
using BusManager.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusManager.Repository
{
    public static class DriverRepository
    {
        public static List<Driver>? GetAllDrivers()
        {
            var _db = new ApplicationContext();

            var driverList = _db.Driver.AsNoTracking().ToList();
            return driverList;
        }

        public static List<Driver>? GetActiveDrivers()
        {
            var _db = new ApplicationContext();

            var driverList = _db.Driver.Where(x => x.Active).AsNoTracking().ToList();
            return driverList;
        }

        public static Driver? GetDriverByCPF(string cpf)
        {
            var _db = new ApplicationContext();

            var driver = _db.Driver.FirstOrDefault(x => x.CPF == cpf);
            return driver;
        }

        public static bool AddDriver(Driver driver)
        {
            var _db = new ApplicationContext();

            _db.Driver.Add(driver);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool UpdateDriver(Driver driver)
        {
            var _db = new ApplicationContext();

            _db.Driver.Update(driver);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteDriver(Driver driver)
        {
            var _db = new ApplicationContext();

            _db.Driver.Remove(driver);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static List<DriverTripRanking> GetDriverRanking()
        {
            var _db = new ApplicationContext();

            var driversWithMostTrips = _db.Trip
                .Include(t => t.Driver)
                .GroupBy(t => new { t.Driver.Name, t.Driver.Surname, t.Driver.HireDate })
                .Select(group => new
                {
                    DriverName = group.Key.Name,
                    DriverSurname = group.Key.Surname,
                    DriverHireDate = group.Key.HireDate,
                    TripCount = group.Count()
                })
                .OrderByDescending(d => d.TripCount)
                .ToList();

            List<DriverTripRanking> result = new();
            foreach (var driver in driversWithMostTrips)
            {
                result.Add(new DriverTripRanking() 
                {
                    Name = driver.DriverName,
                    Surname = driver.DriverSurname,
                    TripCount = driver.TripCount,
                    HireDate = driver.DriverHireDate
                }
                );
            }

            return result;
        }
    }
}
