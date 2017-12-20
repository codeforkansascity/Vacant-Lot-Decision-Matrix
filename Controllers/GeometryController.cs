using CFKC.VPV.Entities;
using CFKC.VPV.Models;
using CFKC.VPV.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CFKC.VPV.Controllers
{
    [Route("api/parcels/geometries")]
    public class GeometryController : Controller
    {
        private CFKCAddressApiData _addressApiRepo;

        private readonly SqlParcelData _parcelRepo;

        public GeometryController(SqlParcelData parcelData, CFKCAddressApiData addressApiRepo)
        {
            _addressApiRepo = addressApiRepo;

            _parcelRepo = parcelData;
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var geometry = await Task.Run(() => _parcelRepo.GetGeometryById(id).ToDto()).ConfigureAwait(false);

            if(geometry == null)
            {
                return NotFound(new ResultResponse<GeometryDto>("BY_ID"));
            }

            return Ok(new ResultResponse<GeometryDto>(geometry, "BY_ID"));
        }

        [HttpGet("adjacent/id")]
        public async Task<IActionResult> GetAdjacent([FromQuery]int id)
        {

            if (id < 1) return NotFound(new ResultResponse<Geometry>("BY_ID"));

            var adjacentParcels = await _parcelRepo.GetAdjacentParcelsAsync(id).ConfigureAwait(false);
            
            var model = adjacentParcels.Select(g => g.ToDto());

            return Ok(new ResultResponse<GeometryDto>(model, "BY_ID"));

        }


    }
}
