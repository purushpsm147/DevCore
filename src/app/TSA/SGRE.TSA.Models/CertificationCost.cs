using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CertificationCost : Audit
    {
        public int Id { get; set; }

        public Currency Currency { get; set; }

        [Required]
        public int CurrencyId { get; set; }

        public Certification Certification { get; set; }

        [Required]
        public int CertificationId { get; set; }

        [Required]
        public decimal CostKilo { get; set; }
    }
}
