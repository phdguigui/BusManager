using BusManager.Model.Entities;

namespace BusManager.Model.Interfaces
{
    public interface IBusService
    {
        public List<Bus>? GetAllBuses();
        public List<Bus>? GetActiveBuses();
        public Bus? GetBusByPlate(string plate);
        public bool AddBus(Bus bus);
        public bool DeleteBus(Bus bus);
        public bool UpdateBus(Bus bus);
    }
}
