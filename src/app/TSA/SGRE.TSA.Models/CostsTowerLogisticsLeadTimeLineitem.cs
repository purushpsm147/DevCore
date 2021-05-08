using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerLogisticsLeadTimeLineitem : Audit
    {
        public int Id { get; set; }
        public CostsTowerLogisticsSupplier CostsTowerLogisticsSuppliers { get; set; }

        [Required]
        public int CostsTowerLogisticsSuppliersId { get; set; }

        public CostsTowerLogisticsLeadTimeMeta CostsTowerLogisticsLeadTimeMeta { get; set; }

        [Required]
        public int CostsTowerLogisticsLeadTimeMetaId { get; set; }

        public Nullable<decimal> Nomination { get; set; }

        public Nullable<decimal> Offer { get; set; }

        public Nullable<decimal> Signature { get; set; }
    }
}