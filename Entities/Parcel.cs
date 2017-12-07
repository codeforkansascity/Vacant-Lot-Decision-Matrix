using System;
using System.Collections.Generic;

namespace CFKC.VPV.Entities
{
    public partial class Parcel
    {
        public long ParcelId { get; set; }
        public int Kivapin { get; set; }
        public string Apn { get; set; }
        public string FormattedAddress { get; set; }
        public string Fraction { get; set; }
        public string OwnerName { get; set; }
        public string OwnerFormattedAddress { get; set; }
        public string OwnerCity { get; set; }
        public string OwnerState { get; set; }
        public string OwnerPostalCode { get; set; }
        public string Zone { get; set; }
        public double? ShapeLength { get; set; }
        public double? SquareFeet { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? GeometryId { get; set; }
        public int? IsVacant { get; set; }

        public Geometry Geometry { get; set; }
    }
}
