using CFKC.VPV.Entities;
using CFKC.VPV.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CFKC.VPV.Controllers
{
    [Route("api/parcels/parks")]
    public class ParkController : Controller
    {
        private readonly SqlParcelData _parcelRepo;


        public ParkController(SqlParcelData parcelRepo)
        {
            _parcelRepo = parcelRepo;

        }

        [HttpGet("proximity/id")]
        public async Task<IActionResult> NearbyParks(int id)
        {
            
            var distanceResults = await Task.Run(() => _parcelRepo.GetNearebyParks(id)).ConfigureAwait(false);

            if (distanceResults == null)
            {
                return NotFound(new ResultResponse<ParkProximity>("BY_ID"));
            }

            return Ok(new ResultResponse<ParkProximity>(distanceResults, "BY_ID"));
            
        }
    }
}
