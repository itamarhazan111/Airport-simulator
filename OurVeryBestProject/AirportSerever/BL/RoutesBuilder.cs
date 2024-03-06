using System.Reflection;
using System.Runtime.Intrinsics.Arm;

namespace AirportSerever.BL
{
    public class RoutesBuilder
    {

        private Station[] Stations_Arr = { new(0), new(1), new(2), new(3), new(4), new(5), new(6), new(7), new(8), new(9), new Ramzor(10)/*Ramzor!*/};
        /*
        i did not wanter to direcly change the Legacy-Code in this project since it broke so many times on me .
         for this purpuse of top 10 interaction PER DAY this code is enough.....
        */
        internal bool InteractWithStation(int stationId, Action<IStation_Emergency> action)
        {
            try
            {
                Station target = Stations_Arr[stationId];
                action(target);
                //  return new { Success = true};
                Console.WriteLine($"an {nameof(action)} was made in station {stationId}");
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not preform {nameof(action)} on station {stationId} \n exeption : {ex.Message}");
                // return new { Success = false, Error = ex };
                return false;
            }
        }

        public FlightRoute GetRoute(Direction direction)
        {
            switch (direction)
            {
                case Direction.Landing:
                    {
                        var route = new FlightRoute(LandingRoute());
                        return route;
                    }

                case Direction.Departure:
                    {
                        var route = new FlightRoute(DepartureRoute());
                        return route;
                    }

                default: throw new Exception();
            }
        }

        private Graph LandingRoute()
        {
            var g = new Graph();
            g.AddEdge(Stations_Arr[0], Stations_Arr[1]);
            g.AddEdge(Stations_Arr[1], Stations_Arr[2]);
            g.AddEdge(Stations_Arr[2], Stations_Arr[3]);
            g.AddEdge(Stations_Arr[3], Stations_Arr[4]);
            g.AddEdge(Stations_Arr[4], Stations_Arr[5]);
            g.AddEdge(Stations_Arr[5], Stations_Arr[6]);
            g.AddEdge(Stations_Arr[5], Stations_Arr[7]);
            return g;
        }

        private Graph DepartureRoute()
        {
            var g = new Graph();
            g.AddEdge(Stations_Arr[0], Stations_Arr[10]);
            g.AddEdge(Stations_Arr[10], Stations_Arr[6]);
            g.AddEdge(Stations_Arr[10], Stations_Arr[7]);
            g.AddEdge(Stations_Arr[6], Stations_Arr[8]);
            g.AddEdge(Stations_Arr[7], Stations_Arr[8]);
            g.AddEdge(Stations_Arr[8], Stations_Arr[4]);
            g.AddEdge(Stations_Arr[4], Stations_Arr[9]);
            Ramzor r = (Ramzor)Stations_Arr[10];
            r.Init(g.GetNext(r));
            return g;
        }
    }
}