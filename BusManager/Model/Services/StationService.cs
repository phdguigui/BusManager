using BusManager.Model.Entities;
using BusManager.Model.Interfaces;
using BusManager.Repository;

namespace BusManager.Model.Services
{
    public class StationService : IStationService
    {
        public bool AddStation(Station station)
        {
            return StationRepository.AddStation(station);
        }

        public List<Station>? GetActiveStations()
        {
            return StationRepository.GetActiveStations();
        }

        public List<Station>? GetAllStations()
        {
            return StationRepository.GetAllStations();
        }

        public Station? GetStationById(int id)
        {
            return StationRepository.GetStationById(id);
        }

        public bool UpdateStation(Station station)
        {
            return StationRepository.UpdateStation(station);
        }

        public bool DeleteStation(Station station)
        {
            return StationRepository.DeleteStation(station);
        }
    }
}
