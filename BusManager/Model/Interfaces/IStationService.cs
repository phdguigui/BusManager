using BusManager.Model.Entities;

namespace BusManager.Model.Interfaces
{
    public interface IStationService
    {
        bool AddStation(Station station);
        List<Station>? GetActiveStations();
        List<Station>? GetAllStations();
        Station? GetStationById(int id);
        bool UpdateStation(Station station);
        bool DeleteStation(Station station);
    }
}
