using BusManager.Model.Entities;
using BusManager.Model.Interfaces;
using BusManager.Repository;

namespace BusManager.Model.Services
{
    public class StopService : IStopService
    {
        public bool AddStop (Stop stop)
        {
            return StopRepository.AddStop(stop);
        }

        public bool DeleteStop(Stop stop)
        {
            return StopRepository.DeleteStop(stop);
        }

        public bool AddManyStop(List<Stop> stopList)
        {
            return StopRepository.AddManyStop(stopList);
        }

        public List<Stop> GetRouteLine(int lineId)
        {
            return StopRepository.GetRouteLine(lineId);
        }

        public Stop? GetStop(Stop stop)
        {
            return StopRepository.GetStop(stop);
        }

        public List<Stop>? GetStopsByStation(Station station)
        {
            return StopRepository.GetStopsByStation(station);
        }
    }
}
