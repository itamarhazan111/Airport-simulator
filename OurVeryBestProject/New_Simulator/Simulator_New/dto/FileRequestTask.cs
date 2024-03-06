using System.ComponentModel.DataAnnotations;
namespace Simulator_New.DTO
{

    public class DataForRequestTask
    {
        [Range(500,10000)]
        public int IntervalForTask {get;set;} 

    }
}
