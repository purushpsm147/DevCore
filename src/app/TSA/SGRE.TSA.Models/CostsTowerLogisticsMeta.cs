using SGRE.TSA.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class CostsTowerLogisticsMeta : Audit
    {
        public int Id { get; set; }

        [Required]
        public string LineItemName { get; set; }

        [Required]
        public string LineItemType { get; set; }

        public string LineItemSource { get; set; }

        [Required]
        public TypeOfTower TypeOfTower { get; set; }
        public bool IsMandatory { get; set; }
    }
}