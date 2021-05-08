using SGRE.TSA.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerCustomsLineItem : Audit
    {
        public int Id { get; set; }
        public CostsTowerCustomsSupplier CostsTowerCustomsSupplier { get; set; }

        [Required]
        public int CostsTowerCustomsSupplierId { get; set; }


        public CostsTowerCustomsMeta CostsTowerCustomsMeta { get; set; }

        [Required]
        public int CostsTowerCustomsMetaId { get; set; }

        public Nullable<decimal> Nomination { get; set; }

        public Nullable<decimal> Offer { get; set; }

        public Nullable<decimal> Signature { get; set; }

    }
}
