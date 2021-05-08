using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class Suppliers
    {
        public int Id { get; set; }

        public CostsTowerSupplier CostsTowerSupplier { get; set; }

        [Required]
        public int CostsTowerSupplierId { get; set; }

        public string Comments { get; set; }

        public string TowerPlanningComments { get; set; }

        public bool IsOfferCapacity { get; set; }
        public bool IsOfferCapability { get; set; }
        public bool IsOfferApproval { get; set; }
        public bool IsSignatureCapacity { get; set; }
        public bool IsSignatureCapability { get; set; }
        public bool IsSignatureApproval { get; set; }
        public bool IsApprovalsApplicable { get; set; }

        public ICollection<CostsTowerSupplierLineItem> CostsTowerSupplierLineItems { get; set; }

    }
}
