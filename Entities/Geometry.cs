namespace CFKC.VPV.Entities
{
    public partial class Geometry
    {
        public int GeometryId { get; set; }
        public long? ParcelId { get; set; }
        public string Polygon { get; set; }
        public string FormattedAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? IsVacant { get; set; }

        public Parcel Parcel { get; set; }
    }

}
