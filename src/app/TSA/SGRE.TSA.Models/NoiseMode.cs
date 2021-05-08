using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class NoiseMode : Audit
    {
        public int Id { get; set; }

        [Required]
        public string NoiseModeNo { get; set; }

        [Required]
        public decimal NoiseLevelDecibels { get; set; }

        [Required]
        public decimal NoiseModeDescription { get; set; }       

        public TowerType TowerType { get; set; }

        [Required]
        public int TowerTypeId { get; set; }
    }
}
