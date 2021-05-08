using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Permission : Audit
    {
        public int Id { get; set; }
        public Role Role { get; set; }

        [Required]
        public int RoleId { get; set; }
        public ProjectModule ProjectModule { get; set; }

        [Required]
        public int ProjectModuleId { get; set; }
        public PermissionTypes PermissionType { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Created by User is Required", MinimumLength = 3)]
        public string CreatedBy { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "Updated by User is Required", MinimumLength = 3)]
        public string UpdatedBy { get; set; }
    }
}
