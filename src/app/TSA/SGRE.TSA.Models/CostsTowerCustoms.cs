using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerCustoms : Audit
    {
        public int Id { get; set; }
        public CostFeedback CostFeedback { get; set; }

        [Required]
        public int CostFeedbackId { get; set; }

        [Required]
        public ScenarioProgress ScenarioProgress { get; set; }

        public bool IsPhaseComplete { get; set; }

        public ICollection<CostsTowerCustomsSupplier> CostsTowerCustomsSuppliers { get; set; }

    }
}
