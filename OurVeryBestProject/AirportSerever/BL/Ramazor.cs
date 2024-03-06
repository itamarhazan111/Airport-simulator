namespace AirportSerever.BL
{
    public class Ramzor : Station
    {
        Station[] next_Stations;
        static object Locker = new object();
        public Ramzor(int id) : base(id)
        {

        }

        public void Init(List<Station> nexts)
        {
            next_Stations = nexts.ToArray();
        }
        internal override async Task<bool> Enter(string name, CancellationTokenSource cts)
        {
            lock (Locker)
            {
                while (true) //bussy wait CAN BE REPLACED WITH SEMAPHORE or event
                {
                    bool can_enter = true;
                    foreach (var s in next_Stations)
                    {
                        if (s.Plane != null)
                        {
                            if (s.Plane.Contains("Departing"))
                                can_enter = false;
                        }
                    }
                    Task.Delay(100).Wait();
                    if (can_enter) break;
                }

                base.Enter(name, cts).Wait();
            }
            return true;

        }


    }
}
