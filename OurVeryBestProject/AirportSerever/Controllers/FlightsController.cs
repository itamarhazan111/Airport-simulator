using AirportSerever.BL;
using AirportSerever.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using AirportSerever.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AirportSerever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly AirportLogic Airport;
        private readonly AirportHub _airportHub;

        public FlightsController(AirportLogic airportLogic, AirportHub airportHub)
        {
            Airport = airportLogic;
            _airportHub = airportHub;
        }
        [HttpGet("land")]
        public string Land()
        {
            return $"landing";
        }
        // GET api/<FlightsController>/5
        [HttpGet("land/{plane}")]
        public string Land(string plane)
        {
            if (string.IsNullOrEmpty(plane))
                return $"{plane} is not a valid value";

            var msg = $"landing {plane}";
            Console.WriteLine(msg);
            Airport.AddFlight(msg, Direction.Landing);
            return msg;
        }
        [HttpGet("departure/{plane}")]
        public string Departure(string plane)
        {
            if (string.IsNullOrEmpty(plane))
                return $"{plane} is not a valid value";

            var msg = $"Departing {plane}";
            Console.WriteLine(msg);
            Airport.AddFlight(msg, Direction.Departure);
            return msg;
        }
        [HttpGet("status")]
        public Status Status()
        {
            return Airport.GetStatus();
        }
        //// POST api/<FlightsController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<FlightsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<FlightsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        [HttpPost("ActivateEmergency")]

        public async Task<IActionResult> ActivateEmergency(DataForRequestTaskOnStation data)
        {
            Console.WriteLine("ActivateEmergency request was made");
            if (data is null)
            {
                Console.WriteLine("values not allowed for ActivateEmergency");
                return BadRequest("data not recived");
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("values not allowed for ActivateEmergency");
                return BadRequest("values not allowed");
            }
            int stationId = data.StationId;
            bool result = Airport.InteractWithStation(stationId, station => station.ActivateEmergency());
            if (!result)
            {
                Console.WriteLine($"failed to ActivateEmergency on station {stationId}");
                return BadRequest($"failed to ActivateEmergency on station {stationId}");
            }
            await _airportHub.UpdateStationEmergency(stationId.ToString(), true);
            return Ok(stationId);
        }
        [HttpPost("DeactivateEmergency")]

        public async Task<IActionResult> DeactivateEmergency(DataForRequestTaskOnStation data)
        {
            Console.WriteLine("DeactivateEmergency request was made");

            if (data is null)
            {
                Console.WriteLine("values not allowed for DeactivateEmergency");
                return BadRequest("data not recived");
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("values not allowed for DeactivateEmergency");
                return BadRequest("values not allowed");
            }
            int stationId = data.StationId;
            bool result = Airport.InteractWithStation(stationId, station => station.DeactivateEmergency());
            if (!result)
            {
                Console.WriteLine($"failed to DeactivateEmergency on station {stationId}");
                return BadRequest($"failed to DeactivateEmergency on station {stationId}");
            }
            else
            {
                await _airportHub.UpdateStationEmergency(stationId.ToString(), false);
                return Ok(stationId);

            }
        }
    }
}
