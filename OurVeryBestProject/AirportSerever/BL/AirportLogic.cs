using AirportSerever.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AirportSerever.BL
{
    public enum Direction { Landing, Departure };
    public class AirportLogic
    {
        List<Flight> _flights = new();
        private readonly RoutesBuilder _routesBuilder;
        private readonly AirportHub _airportHub;

        public AirportLogic(RoutesBuilder routesBuilder, AirportHub airportHub)
        {
            _routesBuilder = routesBuilder;
            _airportHub = airportHub;
        }
        public void AddFlight(string flightName, Direction direction)
        {
            _flights.Add(new Flight(flightName, _routesBuilder.GetRoute(direction), _airportHub));
        }

        public void RemoveFlight(string flightName)
        {
            var flight = _flights.FirstOrDefault(f => f.Name == flightName);
            _flights.Remove(flight);
        }
        public Status GetStatus()
        {
            var list = _flights.Select(f => $"{f.Name} is Attribute {f.StationId}").ToList();
            return new Status { Flights = list };
        }
        public bool InteractWithStation(int stationId, Action<IStation_Emergency> action)
        {
            return _routesBuilder.InteractWithStation(stationId, action);
        }


    }
}