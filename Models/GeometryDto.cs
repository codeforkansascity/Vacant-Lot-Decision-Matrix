using CFKC.VPV.Entities;
using System.Collections.Generic;

namespace CFKC.VPV.Models
{
    public class GeometryDto
    {
        public int GeometryId { get; set; }

        public long? ParcelId { get; set; }

        public IEnumerable<IEnumerable<LatLngDto>> PolygonCollection { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }

        public string FormattedAddress { get; set; }

        public bool IsVacant { get; set; }

        public static GeometryDto ToGeometryDto(Geometry parcelGeometry)
        {
            
            var points = LatLngDto.ParseFromWKT(parcelGeometry.Polygon);

            return new GeometryDto
            {
                GeometryId = parcelGeometry.GeometryId,

                ParcelId = parcelGeometry.ParcelId,

                PolygonCollection = points,

                Lat = parcelGeometry.Latitude,

                Lng = parcelGeometry.Longitude,

                FormattedAddress = parcelGeometry.FormattedAddress,

                IsVacant = parcelGeometry.IsVacant > 0
            };
        }
        
    }
    
}
