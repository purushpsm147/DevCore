using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class RnaCost : Audit
    {
        public int Id { get; set; }
        public Currency Currency { get; set; }
        [Required]
        public int CurrencyId { get; set; }
        public WtgCatalogue WtgCatalogue { get; set; }
        [Required]
        public int WtgCatalogueId { get; set; }
        [Required]
        public decimal Cost { get; set; }
    }
}