using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class BaseTowerAepHubheight : Audit
    {
        public int Id { get; set; }

        public BaseTower BaseTower { get; set; }

        public int BaseTowerId { get; set; }

        public decimal? AepHubHeight { get; set; }

        public decimal? AepNominationGross { get; set; }

        public decimal? AepBindingOfferNet { get; set; }

        public decimal? AepSignatureNet { get; set; }
    }
}