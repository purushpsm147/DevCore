using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ProposalTasks
    {
        public int Id { get; set; }

        // Foreign key to Proposal
        public Proposal Proposal { get; set; }
        [Required]
        public int ProposalId { get; set; }

        // Foreign key to Task
        public Task Task { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public bool IsCustomer { get; set; }
    }
}