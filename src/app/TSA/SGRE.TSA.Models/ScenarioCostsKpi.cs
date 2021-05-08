using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ScenarioCostsKpi
    {
        public int Id { get; set; }

        public Scenario Scenario { get; set; }
        [Required]
        public int ScenarioId { get; set; }

        public decimal TotalTowerExwCostNomination { get; set; }
        public decimal TotalTowerExwCostOffer { get; set; }
        public decimal TotalTowerExwCostSignature { get; set; }


        public decimal TotalCostNomination { get; set; }
        public decimal TotalCostOffer { get; set; }
        public decimal TotalCostSignature { get; set; }

        public decimal AepP50Gross { get; set; }

        public decimal AepP50Net { get; set; }

        [Required]
        public decimal AepP50NominationGross { get; set; }

        [Required]
        public decimal AepP50BindingOfferNet { get; set; }

        [Required]
        public decimal AepP50SignatureNet { get; set; }

        public string Risks { get; set; }

        //will work as DTO
        public decimal Coe { get; set; }
        public decimal CoeDelta { get; set; }
        public decimal CapexDelta { get; set; }
        public decimal AepP50Delta { get; set; }

        public decimal TotalTowerExwCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}