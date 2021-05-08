using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ProjectRoles
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Role Role { get; set; }

        [Required]
        public int RoleId { get; set; }

        // Contains email
        public string userId { get; set; }
    }
}
