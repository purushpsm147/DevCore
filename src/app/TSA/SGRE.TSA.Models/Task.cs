using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Task : Audit
    {
        public int Id { get; set; }

        [Required]
        public string TaskName { get; set; }
    }
}