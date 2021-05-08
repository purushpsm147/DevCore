using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ProjectModule : Audit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Project Module Name is Required", MinimumLength = 3)]
        public string ModuleName { get; set; }
    }
}
