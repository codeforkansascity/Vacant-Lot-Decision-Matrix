using System.Threading.Tasks;
using CFKC.VPV.ClientApp.Models;
using CFKC.VPV.Entities;
using CFKC.VPV.Models;
using Microsoft.AspNetCore.Mvc;

namespace CFKC.VPV.Controllers
{
    [Route("api/parcels")]
    public class ParcelController : Controller
    {
        private readonly SqlParcelData _parcelRepo;

        public ParcelController(SqlParcelData parcel)
        {
            _parcelRepo = parcel;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetFromId([FromQuery]int id)
        {
            var parcel = await Task.Run(() => _parcelRepo.GetParcelById(id)).ConfigureAwait(false);

            if (parcel == null)
            {
                return NotFound(new ResultResponse<Parcel>(parcel, "BY_ID"));
            }

            return Ok(new ResultResponse<Parcel>(parcel, "BY_ID"));

        }

        [HttpGet("kiva")]
        public async Task<IActionResult> GetFromKiva([FromQuery]int kiva)
        {
            var parcel = await Task.Run(() => _parcelRepo.GetParcelByKiva(kiva)).ConfigureAwait(false);


            if (parcel == null)
            {
                return NotFound(new ResultResponse<Parcel>(parcel, "BY_KIVA"));
            }

            return Ok(new ResultResponse<Parcel>(parcel, "BY_KIVA"));
        }

        [HttpGet("address")]
        public async Task<IActionResult> GetFromAddress([FromQuery]string address)
        {

            var parcel = await Task.Run(() => _parcelRepo.GetParcelByAddress(address)).ConfigureAwait(false);

            if (parcel == null)
            {
                return NotFound(new ResultResponse<Parcel>(parcel, "BY_ADDRESS"));
            }

            return Ok(new ResultResponse<Parcel>(parcel, "BY_ADDRESS"));
        }


        [HttpGet("search/address")]
        public async Task<IActionResult> Search([FromQuery] string address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return Ok(new ResultResponse<AddressAutoCompleteResult>("BY_ADDRESS"));
            }

            var results = await Task.Run(() =>
            {
                var searchResults = _parcelRepo.GetSearchResults(address);

                return new ResultResponse<AddressAutoCompleteResult>(searchResults, "BY_ADDRESS");

            }).ConfigureAwait(false);

            return Ok(results);
        }
    }
}




