using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class WtgCatalogueModel
    {
        public int Id { get; set; }

        public WtgCatalogue WtgCatalogue { get; set; }

        [Required]
        public int WtgCatalogueId { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Towermodel is Required")]
        public string Model { get; set; }
    }
}
