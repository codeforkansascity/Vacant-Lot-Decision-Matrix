using System.ComponentModel.DataAnnotations;

namespace CFKC.VPV.Entities
{
    public class ParkProximity
    {
        [Key]
        public int ParkId { get; set; }

        public int GeometryId { get; set; }

        public double DistanceMeters { get; set; }

        public string Name { get; set; }
    }
}
