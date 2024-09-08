using BusManager.Model.Entities;

namespace BusManager.Model.Interfaces
{
    public interface IDriverService
    {
        public List<Driver>? GetAllDrivers();
        public List<Driver>? GetActiveDrivers();
        public Driver? GetDriverByCPF(string cpf);
        public bool AddDriver(Driver driver);
        public bool DeleteDriver(Driver driver);
        public bool UpdateDriver(Driver driver);
    }
}
