using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CFKC.VPV.ClientApp.Models;

namespace CFKC.VPV.Entities
{
    public class SqlParcelData : IParcelRepository
    {
        private CodeForKcDbContext _context;
        private readonly string _connection;

        public SqlParcelData(CodeForKcDbContext context, IConfiguration configuration)
        {
            _context = context;

            _connection = configuration.GetConnectionString("LAN");

        }

        public SqlParcelData(CodeForKcDbContext context, string connection)
        {
            _context = context;

            _connection = connection;
        }

        public Parcel GetParcelByAddress(string address)
        {
            return _context.Parcel.FromSql("AddressSearch {0}", address)
                                  .FirstOrDefault();
        }

        public Parcel GetParcelByKiva(int kiva)
        {
            return _context.Parcel.FirstOrDefault(p => p.Kivapin == kiva);
        }

        public Parcel GetParcelById(int id)
        {
            return _context.Parcel.FirstOrDefault(p => p.ParcelId == id);
        }

        public IEnumerable<AddressAutoCompleteResult> GetSearchResults(string address)
        {
            return _context.Parcel.FromSql("AddressSearch {0}", address)
                                  .Select(x => new AddressAutoCompleteResult(x.ParcelId, x.FormattedAddress))
                                  .ToArray();
        }

        public async Task<IEnumerable<Geometry>> GetAdjacentParcelsAsync(int geometryId, int partitions = 6)
        {
            int total = _context.Geometry.Count() + 10, results = total / partitions;

            var partitionTasks = new Task<IEnumerable<Geometry>>[partitions];

            for (int i = 0; i < partitionTasks.Length; i++)
            {
                partitionTasks[i] = Task.Factory.StartNew<IEnumerable<Geometry>>
                    (
                        (dynamic o) => GetAdjacentFromPartition(o.GeometryId, o.Partition, o.Results),
                        new { GeometryId = geometryId, Partition = i + 1, Results = results }
                    );
            }

            var completedTasks = await Task.WhenAll(partitionTasks);

            return completedTasks.SelectMany(x => x);

        }

        private IEnumerable<Geometry> GetAdjacentFromPartition(int geometryId, int partition, int results)
        {
            using (var context = new CodeForKcDbContext(_connection))
            {
                var sqlStr = new RawSqlString("IntersectingParcels_Partition {0}, {1}, {2}");

                return context.Geometry.FromSql(sqlStr, partition, results, geometryId).ToList();
            }

        }

        public Geometry GetGeometryById(int id)
        {
            return _context.Geometry.FirstOrDefault(p => p.GeometryId == id);
        }

        public IEnumerable<ParkProximity> GetNearebyParks(int parcelId)
        {
            var parcel = GetParcelById(parcelId);

            if (parcel == null)
            {
                return null;
            }

            var storedProcedure = new RawSqlString("EXEC [dbo].[GetNearestParks] @lat = {0}, @lng = {1}");

            return _context.Set<ParkProximity>()
                           .FromSql(storedProcedure, parcel.Latitude, parcel.Longitude)
                           .ToList();
        }

        /// <summary>
        /// Returns incomplete matrix answers only 
        /// answers that can be answered with the data in the database
        /// you must complete all the results using another datasource like the "code for kansas-city API project"
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns>an incomplete matrix answer result</returns>
        public MatrixAnswers GetIncompleteMatrixAnswers(int parcelId)
        {
            var storedProcedure = new RawSqlString("EXEC [dbo].[GetMatrixAnswersForParcel] @parcelId = {0}");

            return _context.Set<MatrixAnswers>()
                               .FromSql(storedProcedure, parcelId)
                               .First();
        }
    }
}
