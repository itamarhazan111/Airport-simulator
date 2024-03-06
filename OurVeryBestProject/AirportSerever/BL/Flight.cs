using AirportSerever.Hubs;
using Microsoft.AspNetCore.SignalR;
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace AirportSerever.BL
{
    public class Flight
    {

        public string Name;
        public int StationId = 0;
        private FlightRoute _route;
        private readonly AirportHub _airportHub;

        public Flight(string flightName, FlightRoute route, AirportHub airportHub)
        {
            Name = flightName;
            _route = route;
            _airportHub = airportHub;
            Run();
        }

        private void Run()
        {
            Task.Run(async () =>
            {


                Station? currStation = null;
                var nextStations = _route.GetNextStations(currStation);

                while (nextStations.Count > 0)
                {
                    //if (currStation== null || (currStation.Id == 5))
                    //if (currStation != null && currStation.Id == 5)
                    //    Console.WriteLine("currStation.Id == 5, ");
                    var cts = new CancellationTokenSource();

                    var tasks = nextStations.Select(async s => await MoveToStation(s, currStation, cts)).ToArray();

                    var previousStation = currStation;

                    Task<Station> resultTask = Task.WhenAny(tasks).Result;
                    currStation = resultTask.Result;
                    if (currStation == null || currStation.Plane != Name)
                        currStation = tasks.Where(task => task.Result != null && task.Result.Plane == Name).Single().Result;


                    nextStations = _route.GetNextStations(currStation);

                }
                Console.WriteLine($"plane Finish {Name} ");
                currStation!.Exit();
                currStation.Plane = null;
                _ = _airportHub.UpdateStation(currStation.Id.ToString(), "");




            });

        }


        private async Task<Station> MoveToStation(Station nextStation, Station prevStation, CancellationTokenSource cts)
        {

            if (!await nextStation.Enter(Name, cts))
            {
                return null;
            }
            if (prevStation != null)
            {
                prevStation.Exit();
                _ = _airportHub.UpdateStation(prevStation.Id.ToString(), "");
            }
            Console.WriteLine(value: $"Flight [direction] {Name} is at {nextStation.Id}");
            _ = _airportHub.UpdateStation(nextStation.Id.ToString(), Name);
            await Task.Delay(nextStation.TimeInStaition);

            return nextStation;
        }
    }
}