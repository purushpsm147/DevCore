using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class PresetRoles
    {
        public int Id { get; set; }

        public Region Region { get; set; }

        public int RegionId { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public string UserName { get; set; }
    }
}
