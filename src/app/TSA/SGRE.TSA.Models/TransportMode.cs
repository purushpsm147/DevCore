using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class TransportMode
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Transport mode type is required")]
        public string Name { get; set; }
    }
}