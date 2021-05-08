namespace SGRE.TSA.Models
{
    public class Summary
    {
        public int SstTowerId { get; set; }
        public string QuotationId { get; set; }
        public string ClusterName { get; set; }
        public int TurbineQuantity { get; set; }
        public string WtgType { get; set; }
        public decimal? DesignLifetime { get; set; }
        public decimal sstTargetLifetime { get; set; }
        public bool isConsistent
        {
            get
            {
                if (sstTargetLifetime == DesignLifetime)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string InitialTower { get; set; }
        public string TowersiteDesignOutputFileLink { get; set; }

        public decimal FileSize { get; set; }

        public decimal RnaLoadMargin { get; set; }
        public bool IsTowerDesignStatusSuccess { get; set; }
        public string ErrorCode { get; set; }
    }
}
