using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class GenericMarketBoundary : Audit
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        [Required]
        public int CountryId { get; set; }
        public TransportMode TransportMode { get; set; }

        [Required]
        public int TransportModeId { get; set; }

        [Required]
        public decimal MaxTowerBaseDiameterMtrs { get; set; }
        [Required]
        public decimal MaxSegmentWeightTon { get; set; }
        [Required]
        public decimal MaxSegmentLengthMtrs { get; set; }
    }
}
