
namespace AirportSerever.BL;


public class Graph
{
    public List<Station> Nodes = new();
    public List<(Station from, Station to)> Edges = new();

    public void AddEdge(Station from, Station to)
    {
        AddNode(from);
        AddNode(to);

        Edges.Add((from, to));
    }

    public void AddNode(Station station)
    {
        if (!Nodes.Any(s => s.Id == station.Id) )
            Nodes.Add(station);
    }

    public List<Station> GetNext(Station? station)
    {
        if( station == null)
        {
            station = Nodes.Find(s => s.Id == 0);
        }
        
        var res =  Edges
            .Where(e => (e.from == null && station == null) || e.from?.Id == station?.Id)
            .Select(e=> Nodes.FirstOrDefault( n => n.Id == e.to.Id))
            .Where( s => s != null)
            .Cast<Station>()
            .ToList();

        return res;
    }
}
