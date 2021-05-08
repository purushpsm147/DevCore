using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class WtgThreshold
    {
        public int Id { get; set; }

        // Minimum Windfarm size in mw to create a configuration
        [Required]
        public decimal WindFarmSize { get; set; }

        // Minimum number of turbines required to create a configuration
        [Required]
        public int TurbineQuantity { get; set; }
    }
}
