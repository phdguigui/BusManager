using BusManager.Data;
using BusManager.Model.Entities;
using BusManager.Model.Interfaces;
using BusManager.Repository;

namespace BusManager.Model.Services
{
    public class BusService : IBusService
    {
        public bool AddBus(Bus bus)
        {
            return BusRepository.AddBus(bus);
        }

        public List<Bus>? GetActiveBuses()
        {
            return BusRepository.GetActiveBuses();
        }

        public List<Bus>? GetAllBuses()
        {
            return BusRepository.GetAllBuses();
        }

        public Bus? GetBusByPlate(string plate)
        {
            return BusRepository.GetBusByPlate(plate);
        }

        public bool UpdateBus(Bus bus)
        {
            return BusRepository.UpdateBus(bus);
        }

        public bool DeleteBus(Bus bus)
        {
            return BusRepository.DeleteBus(bus);
        }
    }
}
