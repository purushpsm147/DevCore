using System.Collections.Generic;

namespace SGRE.TSA.Models
{
    public class Certification
    {
        public int Id { get; set; }
        public string CertificationName { get; set; }
        public ICollection<CertificationCost> CertificationCost { get; set; }

    }
}
