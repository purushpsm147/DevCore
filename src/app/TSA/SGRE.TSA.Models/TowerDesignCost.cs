using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class TowerDesignCost : Audit
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        [Required]
        public decimal CostKilo { get; set; }
    }
}
