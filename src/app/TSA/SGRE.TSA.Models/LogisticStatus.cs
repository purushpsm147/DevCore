using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class LogisticStatus
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "Status is required")]
        public string Status { get; set; }
    }
}
