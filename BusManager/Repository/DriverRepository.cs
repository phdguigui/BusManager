using BusManager.Data;
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
    }
}
