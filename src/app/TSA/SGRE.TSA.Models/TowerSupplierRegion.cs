using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SGRE.TSA.Models
{
    public class TowerSupplierRegion : Audit
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Region Region { get; set; }

        [JsonIgnore]
        public int RegionId { get; set; }

        public string RegionName { get; set; }

        public ICollection<TowerSupplierSource> TowerSupplierSource { get; set; }

        [JsonIgnore]
        public InterfaceTools InterfaceTools { get; set; }
        [JsonIgnore]
        public int InterfaceToolsId { get; set; }
    }
}