using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerLogistics : Audit
    {
        public int Id { get; set; }
        public CostFeedback CostFeedback { get; set; }

        [Required]
        public int CostFeedbackId { get; set; }

        public string Comments { get; set; }

        [Required]
        public ScenarioProgress ScenarioProgress { get; set; }

        public bool IsPhaseComplete { get; set; }

        public ICollection<CostsTowerLogisticsSupplier> CostsTowerLogisticsSuppliers { get; set; }

    }
}
