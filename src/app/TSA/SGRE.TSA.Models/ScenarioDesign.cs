using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ScenarioDesign
    {
        public int Id { get; set; }

        // FK to Scenario
        public Scenario Scenario { get; set; }
        [Required]
        public int ScenarioId { get; set; }

        public string Risks { get; set; }
        public bool DesignEvaluation { get; set; }

        public decimal LifeTime { get; set; }
        public decimal TowerWeight { get; set; }

        public bool StepProgress { get; set; }
        public decimal HubHeight { get; set; }
    }
}