using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class MileStone : Audit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "MileStone Name is Required", MinimumLength = 3)]
        public string MileStoneName { get; set; }
    }
}