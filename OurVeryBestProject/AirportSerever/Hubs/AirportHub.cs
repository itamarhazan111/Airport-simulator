using Microsoft.AspNetCore.SignalR;

namespace AirportSerever.Hubs
{
    public class AirportHub : Hub
    {
        public async Task UpdateStation(string stationId, string plainName) => await Clients.All.SendAsync(stationId, plainName);
        public async Task UpdateStationEmergency(string stationId, bool onOff) =>
            await Clients.All.SendAsync($"{stationId}-emergency", onOff);
    }
}
