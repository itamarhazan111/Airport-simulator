using System.ComponentModel.DataAnnotations;
namespace AirportSerever.DTO
{

    public class DataForRequestTaskOnStation
    {
        [Range(0,int.MaxValue)]
        public int StationId {get;set;} 
    }
}