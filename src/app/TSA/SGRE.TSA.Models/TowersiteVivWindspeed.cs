using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class TowersiteVivWindspeed : Audit
    {
        public int Id { get; set; }

        [Required]
        public Guid SstUuid { get; set; }

        [Required]
        public int StageNr { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "StageDescription exceeded maximum character length")]
        public string StageDescription { get; set; }

        [StringLength(16, ErrorMessage = "Critical10minTmeanWindMs exceeded maximum character length")]
        public string Critical10minTmeanWindMs { get; set; }

        [StringLength(16, ErrorMessage = "Maxallowed10minMeanWindMs exceeded maximum character length")]
        public string Maxallowed10minMeanWindMs { get; set; }

        [StringLength(255, ErrorMessage = "PreventiveMeasure exceeded maximum character length")]
        public string PreventiveMeasure { get; set; }

        [StringLength(64, ErrorMessage = "MaxallowedVivDuration exceeded maximum character length")]
        public string MaxallowedVivDuration { get; set; }
    }
}