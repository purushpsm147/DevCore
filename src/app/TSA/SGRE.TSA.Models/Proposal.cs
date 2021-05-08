using SGRE.TSA.Models.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Proposal : Audit
    {
        private string proposalId;

        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string ProposalId
        {
            get { return proposalId; }
            set { proposalId = value.Trim(); }
        }

        [Required]
        public bool IsMain { get; set; }

        public Certification Certification { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Certification Id required")]
        public int CertificationId { get; set; }

        [CertificationDate]
        public DateTimeOffset? CertificationDate { get; set; }
        public DateTimeOffset? RecordEndDateTime { get; set; }

        [StringLength(1)]
        public string ActiveRecordIndicator { get; set; }

        public ICollection<ProposalTasks> ProposalTasks { get; set; }
        public ICollection<Quote> Quotes { get; set; }
    }
}
