using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SSTAepLookupGross : Audit
    {
        public int Id { get; set; }

        public string estimationType { get; set; }

        public bool isProposedHubHeightFound { get; set; }

        public decimal aepNominationGross { get; set; }

        public IEnumerable<AepInputJson> aepInputFile { get; set; }

        public Guid AepLookupUuid { get; set; }

    }
}
