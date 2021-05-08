using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostFeedback : Audit
    {
        public int Id { get; set; }

        public Scenario Scenario { get; set; }
        [Required]
        public int ScenarioId { get; set; }

        [Required]
        public ScenarioTypes ScenarioType { get; set; }

        public bool IsCostFeebackRequest { get; set; }

        public ICollection<CostsTowerSupplier> CostsTowerSuppliers { get; set; }
        public CostsTowerExWorks CostsTowerExWorks { get; set; }
        public ICollection<CostsTowerLogistics> CostsTowerLogistics { get; set; }
        public CostsTechnology CostsTechnologies { get; set; }
        public CostsTowerConstruction CostsTowerConstructions { get; set; }
        public ICollection<CostsTowerCustoms> CostsTowerCustoms { get; set; }

    }
}
