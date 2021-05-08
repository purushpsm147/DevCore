namespace SGRE.TSA.Models
{
    public class TowerSupplierSource : Audit
    {
        public int Id { get; set; }

        public TowerSupplierRegion TowerSupplierRegion { get; set; }
        public int TowerSupplierRegionId { get; set; }

        public string SourcingName { get; set; }

    }
}