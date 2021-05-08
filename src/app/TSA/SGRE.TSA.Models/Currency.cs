using System.ComponentModel.DataAnnotations;
namespace SGRE.TSA.Models
{
    public class Currency : Audit
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }
}