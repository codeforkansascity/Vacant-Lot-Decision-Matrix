using System.ComponentModel.DataAnnotations;

namespace CFKC.VPV.Entities
{
    public class MatrixAnswers
    {
        [Key]
        public int ParcelId { get; set; }
        public int? ParkWithinHalfMile { get; set; }
        public int? LargerThan1000Sqft { get; set; }
        public int? AdjacentToVacant { get; set; }
        public int? AdjacentToResidential { get; set; } //get from AddressApi
        public int? HasSoilContamination { get; set; }
        public int? InFloodplain { get; set; }
    }


}
