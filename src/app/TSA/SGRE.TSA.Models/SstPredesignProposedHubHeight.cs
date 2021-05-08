using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SstPredesignProposedHubHeight : Audit
    {
        public int Id { get; set; }

        public SstTower SstTower { get; set; }
        [Required]
        public int SstTowerId { get; set; }


        [Required]
        public decimal AepNominationGross { get; set; }

        [Required]
        public decimal AepBindingOfferNet { get; set; }

        [Required]
        public decimal AepSignatureNet { get; set; }

        public bool isAepLookupDone { get; set; }
    }
}