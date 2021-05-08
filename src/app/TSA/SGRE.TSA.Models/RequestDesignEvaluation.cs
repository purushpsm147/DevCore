using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class RequestDesignEvaluation
    {
        public string DesignAuthor { get; set; } = "TowerSelect";
        public string DesignAuthorEmail { get; set; }
        public DateTime DesignDate { get; set; } = DateTime.UtcNow;
        public string DesignDescription { get; set; } = "projectid-proposalid-qid-sid-ssttowerid";
        public int DesignVersion { get; set; }
        public string ElsaFilePath { get; set; }
        public string SafalFilePath { get; set; }
        public string Posc2Folder { get; set; } //non required can be empty value
        public string XmlFilePath { get; set; } //non required can be empty value
        public string Region { get; set; }
        public string Platform { get; set; }
        public string Tower { get; set; }
        public decimal HubHeight { get; set; }
        public decimal SectionNumber_max { get; set; } //mandatory send 200 always
        public decimal BottomDiameter_max { get; set; } //need to 
        public decimal SectionLength_max { get; set; }
        public decimal SectionWeight_max { get; set; }
        public int sstTowerId { get; set; }
        public int scenarioId { get; set; }
        public string Model { get; set; }
        public decimal LoadsetHubHeight { get; set; }
        public decimal ElevatedFoundationHeight { get; set; }
    }
    public class FrontendRequest
    {
        [Required]
        public int sstTowerId { get; set; }
        public int scenarioId { get; set; }
        public string action { get; set; }
    }
}
