using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class WtgCatalogue
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Turbine Type is Required")]

        // wtg short name
        public string WtgType { get; set; }
        [Required]
        public decimal WtgSizeMW { get; set; }

        public string Platform { get; set; }

        public ICollection<TowerType> TowerTypes { get; set; }

        public ICollection<ApplicationMode> ApplicationModes { get; set; }
        public ICollection<NacelleDistance> NacelleDistances { get; set; }
        public ICollection<InitialTower> InitialTowers { get; set; }
        public ICollection<WtgCatalogueModel> WtgCatalogueModels { get; set; }
    }
}
