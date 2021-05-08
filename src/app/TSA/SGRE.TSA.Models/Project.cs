using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;

namespace SGRE.TSA.Models
{
    public class Project : Audit
    {
        private string opportunityId;
        private string projectName;
        private string customerName;

        public int Id { get; set; }     

        [Required]
        [StringLength(50, ErrorMessage = "Project / SaleForce Opportunity ID is Required", MinimumLength = 3)]
        public string OpportunityId
        {
            get { return opportunityId; }
            set { opportunityId = value.Trim(); }
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ProjectName
        {
            get { return projectName; }
            set { projectName = value.Trim(); }
        }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value.Trim(); }
        }

        [StringLength(20, MinimumLength = 3)]
        public string ContractStatus { get; set; }

        public Country Country { get; set; }

        [Required]
        public int CountryId { get; set; }

        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        public bool IsCurrencyLocked { get; set; }

        public string SSTRequestSimilarProjects { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Created by User is Required", MinimumLength = 3)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Updated by User is Required", MinimumLength = 3)]
        public string UpdatedBy { get; set; }
        public DateTimeOffset? RecordEndDateTime { get; set; }

        [JsonIgnore]
        public bool HasDuplicateMilestones
        {
            get
            {
                return ProjectMileStones != null && ProjectMileStones.Select(s => s.MileStoneId).Distinct().Count() != ProjectMileStones.Count();
            }
        }

        [JsonIgnore]
        public bool HasDuplicateRoles
        {
            get
            {
                return ProjectRoles != null && ProjectRoles.Select(pr => new { pr.RoleId }).Distinct().Count() != ProjectRoles.Count();
            }
        }

        [StringLength(1)]
        public string ActiveRecordIndicator { get; set; }

        public ICollection<Proposal> Proposals { get; set; }

        public ICollection<ProjectMileStones> ProjectMileStones { get; set; }

        public ICollection<ProjectRoles> ProjectRoles { get; set; }
        public ICollection<ProjectConstraint> ProjectConstraints { get; set; }

    }
}
