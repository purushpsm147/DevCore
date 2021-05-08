using SGRE.TSA.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerCustomsSupplier : Audit
    {
        public int Id { get; set; }
        public CostsTowerCustoms CostsTowerCustoms { get; set; }

        [Required]
        public int CostsTowerCustomsId { get; set; }
        public string Comments { get; set; }
        public ICollection<CostsTowerCustomsLineItem> CostsTowerCustomsLineItems { get; set; }
    }
}
