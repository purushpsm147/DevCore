using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerLogisticsSupplier : Audit
    {
        public int Id { get; set; }
        public CostsTowerLogistics CostsTowerLogistics { get; set; }

        [Required]
        public int CostsTowerLogisticsId { get; set; }
        public ICollection<CostsTowerLogisticsLeadTimeLineitem> CostsTowerLogisticsLeadTimeLineitems { get; set; }
        public ICollection<CostsTowerLogisticsLineItem> CostsTowerLogisticsLineItems { get; set; }

    }
}
