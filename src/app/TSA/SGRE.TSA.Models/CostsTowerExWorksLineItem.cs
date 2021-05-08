using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerExWorksLineItem : Audit
    {
        public int Id { get; set; }
        public CostsTowerExWorks CostsTowerExWorks { get; set; }

        [Required]
        public int CostsTowerExWorksId { get; set; }

        public CostsTowerExWorksMeta CostsTowerExWorksMeta { get; set; }

        [Required]
        public int CostsTowerExWorksMetaId { get; set; }

        public Nullable<decimal> Nomination { get; set; }

        public Nullable<decimal> Offer { get; set; }

        public Nullable<decimal> Signature { get; set; }

    }
}
