using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SstPredesignExistingHubHeight : Audit
    {
        public int Id { get; set; }
        public SstTower SstTower { get; set; }

        [Required]
        public int SstTowerId { get; set; }

        public decimal AepNominationGross { get; set; }
        public decimal AepBindingOfferNet { get; set; }
        public decimal AepSignatureNet { get; set; }

    }
}
