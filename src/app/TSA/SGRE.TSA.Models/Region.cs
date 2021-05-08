using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Region
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Region Name is Required", MinimumLength = 3)]
        public string RegionName { get; set; }
    }
}
