using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class MailBody
    {
        public string CustomerProject { get; set; }
        public string QuoteId { get; set; }
        public string TowerScenario { get; set; }
        public string Workflow { get; set; }
        public string Requestedby { get; set; }
        public string ToRole { get; set; }
        public string Startdate { get; set; }
        public string Responsible { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter an valid email address")]
        public string To { get; set; }
        public string Subject { get; set; }
        public string ReassignmentLink { get; set; }
        public string UILInk { get; set; }
    }
}
