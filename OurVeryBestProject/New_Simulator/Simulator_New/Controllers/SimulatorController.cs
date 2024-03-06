using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Simulator_New.Modules;
using Simulator_New.DTO;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Simulator_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimulatorController : ControllerBase
    {

        private readonly TaskManager _taskManager;
        public SimulatorController(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }
        [HttpPost("CreateLanding")]
        public IActionResult CreateLanding(DataForRequestTask data)
        {
            Console.WriteLine("Request was made");
            int interval = data.IntervalForTask;
            Console.WriteLine($"a request for Landing with {interval} was made");
            if (!ModelState.IsValid)
            {
                return BadRequest("values not allowed");
            }

            _taskManager.CreateNewTask(interval, true);
            Console.WriteLine($"the Landing request was comitted succsesfuly");
            return Ok();
        }
        [HttpPost("CreateDeprature")]
        public IActionResult CreateDeprature(DataForRequestTask data)
        {
            Console.WriteLine("Request was made");
            int interval = data.IntervalForTask;
            Console.WriteLine($"a request for Deprature with {interval} was made");
            if (!ModelState.IsValid)
            {
                return BadRequest("values not allowed");
            }

            _taskManager.CreateNewTask(interval, false);
            Console.WriteLine($"the Deprature request was comitted succsesfuly");
            return Ok();
        }
        [HttpGet("All")]
        public IActionResult me()
        {

            return Ok("HAHA");
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteTask(DataForDelete data)
        {
            Console.WriteLine("delete request was made");
            if (!ModelState.IsValid)
            {
                return BadRequest("values not allowed");
            }
            bool landing = data.IsLanging;
            Console.WriteLine($"A request for Delete (Landing? : {landing}) was made ");
            if (await _taskManager.DeleteTask(landing))
            {
                Console.WriteLine($"task (for landing? : {landing}) was Canceled");
                return Ok($"deleted.");
            }

            return NotFound($"Task not found.");
        }

    }
}