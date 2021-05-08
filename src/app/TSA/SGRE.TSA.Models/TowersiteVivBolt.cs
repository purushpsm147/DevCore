using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class TowersiteVivBolt : Audit
    {
        public int Id { get; set; }

        [Required]
        public Guid SstUuid { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "from_section exceeded maximum character length")]
        public string FromSection { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "to_section exceeded maximum character length")]
        public string ToSection { get; set; }

        [Required]
        public decimal TightenedBoltsPercentage { get; set; }
    }
}
