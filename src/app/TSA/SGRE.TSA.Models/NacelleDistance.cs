using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class NacelleDistance : Audit
    {
        public int Id { get; set; }

        [Required]
        public decimal DistanceBottomToCenterHub { get; set; }

        public WtgCatalogue WtgCatalogue { get; set; }
        [Required]
        public int WtgCatalogueId { get; set; }
    }
}
