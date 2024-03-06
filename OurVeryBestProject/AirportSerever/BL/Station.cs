using AirportSerever.Hubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace AirportSerever.BL
{
    public class Station : IStation_Emergency
    {
        private const int wait_for_emergency = 5000;
        public int Id { get; }
        public int TimeInStaition { get; internal set; } = 1000;

        public string? Plane = null;
        private SemaphoreSlim _sem = new(1);
        // public SemaphoreSlim Ramzor = new(2);

        private SemaphoreSlim _emergency_ShutDown = new(1);
        public bool IsEmergencyActivated { get; private set; } = false;

        public bool full = false;

        // static event Station_Released 

        public Station(int id)
        {
            Id = id;
        }
        public void ActivateEmergency()
        {
            IsEmergencyActivated = true;
            _emergency_ShutDown.Wait();
        }
        public void DeactivateEmergency()
        {
            IsEmergencyActivated = false;
            _emergency_ShutDown.Release();
        }
        internal virtual async Task<bool> Enter(string name, CancellationTokenSource cts)
        {


            try
            {

                await _sem.WaitAsync(); //.ContinueWith(t => cts.Cancel());
                var token = cts.Token;
                token.ThrowIfCancellationRequested();
                if (IsEmergencyActivated)
                {
                    Console.WriteLine($"Emergency Activated: Plane {name} is waiting outside Station {Id}");
                    await _emergency_ShutDown.WaitAsync(cts.Token);
                    _emergency_ShutDown.Release();
                }
                token.ThrowIfCancellationRequested();
                cts.Cancel();
                full = true;
                if (Plane != null)
                    Console.WriteLine("Crash !!!!!!!!");
                Plane = name;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"plane {name} Error entering Station {Id}, semaphore:\n {ex}");
                Console.WriteLine($"Catch Released at {Id}, Plane {name}");
                _sem.Release();
                _emergency_ShutDown.Release();

                return false;
            }
        }

        internal void Exit()
        {

            Plane = null;
            _sem.Release();
            //setevent(Station_Released); // the ramzor need to listen to this
            full = false;
        }


    }
}