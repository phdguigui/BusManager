using BusManager.Model.Entities;
using BusManager.Repository;

namespace BusManager.Model.Services
{
    public class TripService
    {
        public List<Trip> GetAllTrips ()
        {
            return TripRepository.GetAllTrips();
        }
        public bool AddTrip(Trip trip)
        {
            return TripRepository.AddTrip(trip);
        }
        public Trip? GetTripById(int id)
        {
            return TripRepository.GetTripById(id);
        }
        public bool UpdateTrip(Trip trip)
        {
            return TripRepository.UpdateTrip(trip);
        }
        public bool DeleteTrip(Trip trip)
        {
            return TripRepository.DeleteTrip(trip);
        }
    }
}
