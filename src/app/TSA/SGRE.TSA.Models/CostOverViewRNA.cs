namespace SGRE.TSA.Models
{
    public class CostOverViewRNA
    {
        public int PositionId { get; set; }
        public int ScenarioId { get; set; }

        public string Position { get; set; }

        public decimal Nomination { get; set; }

        public decimal Offer { get; set; }

        public decimal Signature { get; set; }

        public decimal NominationWindfarm { get; set; }

        public decimal OfferWindfarm { get; set; }

        public decimal SignatureWindfarm { get; set; }

    }
}
