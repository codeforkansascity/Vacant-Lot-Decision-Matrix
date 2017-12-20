using CFKC.VPV.Entities;
using CFKC.VPV.Services;
using CFKC.VPV.Services.DecisionMatrix;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CFKC.VPV.Controllers
{
    [Route("api/parcels/matrix")]
    public class MatrixController : Controller
    {
        private readonly CFKCAddressApiData _publicRepo;

        private readonly SqlParcelData _parcelRepo;

        private readonly MatrixResolver _resolver;

        public MatrixController(SqlParcelData parcelRepository,
                                CFKCAddressApiData publicRepo,
                                MatrixResolver resolver)
        {
            _publicRepo = publicRepo;

            _parcelRepo = parcelRepository;

            _resolver = resolver;
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetFromId([FromQuery]int id)
        {
            var parcel = _parcelRepo.GetParcelById(id);

            if (parcel == null)
            {
                return NotFound();
            }


            var answers = await MergeMatrixDataAsync(parcel).ConfigureAwait(false);

            _resolver.AddMatrixData(answers);

            var matrixStats = _resolver.ComputeBestUse();

            return Ok(matrixStats);
        }

        /// <summary>
        /// Merges data needed by getting what we have from the database for this application
        /// then calling CFKCAddressApi Resful webservice to complete that data needed
        /// to answer matrix questions
        /// </summary>
        /// <param name="foundParcel"></param>
        /// <returns></returns>
        [NonAction]
        public async Task<MatrixAnswers> MergeMatrixDataAsync(Parcel foundParcel)
        {

            int parcelId = (int)foundParcel.ParcelId, geometryId = (int)foundParcel.GeometryId;

            var matrixTask = Task.Run(() => _parcelRepo.GetIncompleteMatrixAnswers(parcelId));

            var kivasTask = Task.Run(() => _parcelRepo.GetAdjacentParcelsAsync(geometryId).Result
                                                .Select(g => _parcelRepo
                                                .GetParcelById((int)g.ParcelId)
                                                .Kivapin).ToArray());

            await Task.WhenAll(matrixTask, kivasTask).ConfigureAwait(false);

            var kivas = kivasTask.Result;
            var matrix = matrixTask.Result;

            var apiParcelTasks = new Task<AddressApiParcel>[kivas.Length];

            for (var i = 0; i < apiParcelTasks.Length; i++)
            {
                object boxedKiva = kivas[i];

                apiParcelTasks[i] = Task.Factory
                                        .StartNew(o => _publicRepo.GetParcelByKiva((int)o), boxedKiva);
            }

            var apiParcels = await Task.WhenAll(apiParcelTasks).ConfigureAwait(false);

            var residential = apiParcels.Where(p => p != null).Any(p => p.IsResidential());

            matrix.AdjacentToResidential = residential ? 1 : 0;

            return matrix;

        }
    }
}
