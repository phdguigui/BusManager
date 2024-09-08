using BusManager.Data;
using BusManager.Model.Entities;
using BusManager.Model.Interfaces;
using BusManager.Repository;
using System.Collections.Generic;

namespace BusManager.Model.Services
{
    public class DriverService : IDriverService
    {
        public bool AddDriver(Driver driver)
        {
            return DriverRepository.AddDriver(driver);
        }

        public List<Driver>? GetActiveDrivers()
        {
            return DriverRepository.GetActiveDrivers();
        }

        public List<Driver>? GetAllDrivers()
        {
            return DriverRepository.GetAllDrivers();
        }

        public Driver? GetDriverByCPF(string cpf)
        {
            return DriverRepository.GetDriverByCPF(cpf);
        }

        public bool UpdateDriver(Driver driver)
        {
            return DriverRepository.UpdateDriver(driver);
        }

        public bool DeleteDriver(Driver driver)
        {
            return DriverRepository.DeleteDriver(driver);
        }
    }
}
