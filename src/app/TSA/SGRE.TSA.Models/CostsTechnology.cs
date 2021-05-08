using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTechnology : Audit
    {
        public int Id { get; set; }
        public CostFeedback CostFeedback { get; set; }
        [Required]
        public int CostFeedbackId { get; set; }

        public decimal TowerComponentCertificateWindfarmClusterNomination { get; set; }
        public decimal TowerComponentCertificateWindfarmClusterOffer { get; set; }
        public decimal TowerComponentCertificateWindfarmClusterSignature { get; set; }


        public string AdditinalComments { get; set; }
        public CostTowerDevFeedback CostTowerDevFeedback { get; set; }
        public bool IsFeedbackRequest { get; set; }
        public bool IsLeadTimeInputRequest { get; set; }

        public decimal LeadTimeTowerNomination { get; set; }
        public decimal LeadTimeTowerOffer { get; set; }
        public decimal LeadTimeTowerSignature { get; set; }
        public EnvelopStatus EnvelopStatus { get; set; }

        [Required]
        public ScenarioProgress ScenarioProgress { get; set; }

        public bool IsPhaseComplete { get; set; }
    }
}
