namespace AirportSerever.BL
{
    public class FlightRoute
    {
        public Graph StationsGraph;

        public FlightRoute(Graph routeGraph)
        {
            StationsGraph = routeGraph;
        }

        public List<Station> GetNextStations(Station? station)
        {
            return StationsGraph.GetNext(station);
        }
    }
}