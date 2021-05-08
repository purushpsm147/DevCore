namespace SGRE.TSA.Models
{
    public class InterfaceTools : Audit
    {
        public int Id { get; set; }
        public TowerSupplierRegion TowerSupplierRegion { get; set; }
        public int TowerSupplierRegionId { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
    }
}

