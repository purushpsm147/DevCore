using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class EtlJobAudit : Audit
    {
        public int Id { get; set; }

        [Required]
        public Guid JobUuid { get; set; }

        // job_id, process_id, pipeline_id, etc
        [Required]
        [StringLength(128, ErrorMessage = "JobId exceeded maximum character length")]
        public string JobId { get; set; }

        [StringLength(255, ErrorMessage = "JobDescription exceeded maximum character length")]
        public string JobDescription { get; set; }

        // JobName - towersite_to_tosa, swat_to_tosa
        [Required]
        [StringLength(128, ErrorMessage = "JobName exceeded maximum character length")]
        public string JobName { get; set; }

        // Framework - Datafactory, Logic Apps, Event Hub, Python
        [Required]
        [StringLength(64, ErrorMessage = "Framework exceeded maximum character length")]
        public string Framework { get; set; }

        // LoadType - Batch (Init, Incremental), Stream, Event
        [Required]
        [StringLength(32, ErrorMessage = "LoadType exceeded maximum character length")]
        public string LoadType { get; set; }

        // SourceSystem - towersite, swat
        [Required]
        [StringLength(64, ErrorMessage = "SourceSystem exceeded maximum character length")]
        public string SourceSystem { get; set; }

        // DestinationSystem - ToSA DB, Datalake, Blob, etc
        [Required]
        [StringLength(64, ErrorMessage = "DestinationSystem exceeded maximum character length")]
        public string DestinationSystem { get; set; }

        // SourceType - csv, excel, txt, db
        [Required]
        [StringLength(64, ErrorMessage = "SourceType exceeded maximum character length")]
        public string SourceType { get; set; }

        // DestinationType - blob, db ,etc
        [Required]
        [StringLength(64, ErrorMessage = "DestinationType exceeded maximum character length")]
        public string DestinationType { get; set; }

        // ExecutionStatus - Running/Completed/Terminated/Failed
        [Required]
        [StringLength(32, ErrorMessage = "DestinationType exceeded maximum character length")]
        public string ExecutionStatus { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        public int ProcessCount { get; set; }

        public int RejectCount { get; set; }

        public int TargetCount { get; set; }

        [StringLength(255, ErrorMessage = "ErrorMsg exceeded maximum character length")]
        public string ErrorMsg { get; set; }

        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
    }
}

