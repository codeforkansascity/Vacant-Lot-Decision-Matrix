using System;

namespace CFKC.VPV.Entities
{
    public partial class Park
    {
        public int ParkId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string BookName { get; set; }
        public double? BookAcres { get; set; }
        public string BookComment { get; set; }
        public double? BookRoadway { get; set; }
        public string BenefitDistrict { get; set; }
        public string Region { get; set; }
        public string Council { get; set; }
        public int Developed { get; set; }
        public int? YearAcquired { get; set; }
        public int InKcmo { get; set; }
        public DateTime? LastUpdate { get; set; }
        public double? ShapeLength { get; set; }
        public double? SquareFeet { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? GeometryId { get; set; }
    }
}
